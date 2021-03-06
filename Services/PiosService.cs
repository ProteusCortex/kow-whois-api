using System;
using System.Collections.Generic;
using System.Globalization;
using HtmlAgilityPack;
using KowWhoisApi.Models;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using KowWhoisApi.Utilities;
using Microsoft.Extensions.Options;

namespace KowWhoisApi.Services
{
	public class PiosService : IPiosService
	{
		private readonly PiosServiceOptions _options;

		private readonly int _cache_ttl;
		private readonly ILogger _logger;
		private readonly IRedisCacheClient _redis;
		private UriBuilder _builder;


		public PiosService(IOptions<PiosServiceOptions> options, ILogger<PiosService> logger, IRedisCacheClient redis)
		{
			// Initialize the options.
			_options = options.Value;

			// Initialize injected stuff.
			_logger = logger;
			_redis = redis;

			// Save the cache TTL.
			_cache_ttl = _options.CacheTtl;

			// Determine the scheme to be used.
			var scheme = _options.Secure ? "https" : "http";

			// Initialize the builder.
			_builder = new UriBuilder(scheme, _options.Host, _options.Port, _options.Path);
		}

		public IPiosResult AskPios(string domain, bool fresh = false)
		{
			// Log stuff.
			_logger.LogInformation($"Got request for {domain}");

			// Extract the base domain.
			var baseDomain = new BaseDomain(domain);

			// Check if a valid domain could be found, otherwise return an empty result.
			if (!baseDomain.IsValid)
			{
				_logger.LogInformation($"\"{domain}\" is an invalid .gr/.ελ domain.");
				return new PiosResult();
			}

			// Check for cached results, unless otherwise requested.
			if (!fresh)
			{
				var cached = RecallResult(baseDomain.Value);
				if (cached != null)
				{
					return cached;
				}
			}

			// At this point it's clear we need to query The Registry.
			_logger.LogInformation($"No cached results found or fresh results were requested for {baseDomain}. Fetching some fresh information.");

			// Fill in the domain query.
			_builder.Query = $"domainName={baseDomain}";

			// Fetch what needs to be fetched.
			HtmlWeb web = new HtmlWeb();
			var htmlDoc = web.Load(_builder.Uri);

			// Isolate the response body.
			var node = htmlDoc.DocumentNode.SelectSingleNode("//body");

			// Parse the result.
			var result = ParsePios(baseDomain.Value, node.InnerText);

			// Cache the result.
			CacheResult(result);

			// We are done here.
			return result;
		}

		/// <summary>
		/// Writes results to the cache.
		/// </summary>
		/// <param name="result">The result to be cached.</param>
		private void CacheResult(PiosResult result)
		{
			// Cache the result.
			var writeCache = _redis.Db0.AddAsync<PiosResult>(result.Domain.Name, result, TimeSpan.FromSeconds(_cache_ttl));

			// Wait for it to finish.
			writeCache.Wait();
		}

		/// <summary>
		/// Tries to recall the results for a given domain from the cache.
		/// </summary>
		/// <param name="domain">The domain for which to check.</param>
		/// <returns>The <see cref="PiosResult"> for the domain (marked as "cached") or null.</returns>
		private PiosResult RecallResult(string domain)
		{
			// Query the cache for the given domain.
			var queryCache = _redis.Db0.GetAsync<PiosResult>(domain);
			queryCache.Wait();
			var cached = queryCache.Result;

			// If the result was found in the cache, mark it accordingly
			// before returning it.
			if (cached != null)
			{
				_logger.LogInformation($"Found {domain} in cache.");
				cached.IsCached = true;
			}

			// Return whatever the cache served.
			return cached;
		}

		/// <summary>
		/// Parse the results returned from the Registry.
		/// </summary>
		/// <param name="domain">The domain to which the results refer.</param>
		/// <param name="result">The raw registry query results.</param>
		/// <returns></returns>
		private PiosResult ParsePios(string domain, string result)
		{
			// Split the result into fields.
			var fields = result.Split('\n');

			// Fill in the basics.
			var parsed = new PiosResult(domain, fields.Length > 2);

			// If it is not registered, just return what we got so far.
			if (!parsed.IsRegistered) return parsed;

			// OK, we got so far, so this domain is registered. Gather
			// all the key-value pairs into a List.
			var pairs = new List<string[]>();

			foreach (var field in fields)
			{
				// Split the pair.
				var pair = field.Split(':');

				// Get the key.
				var key = pair[0];

				// Initialize the value.
				var value = String.Empty;

				// The URL field gets split more than once, thus we need to
				// take extra care of the value field.
				if (pair.Length == 2)
				{
					value = pair[1];
				}
				else if (pair.Length == 3)
				{
					value = $"{pair[1]}:{pair[2]}";
				}

				// Check if there is anything in there.
				if (key.Length == 0) continue;

				// Add it to the pile of pairs.
				pairs.Add(new string[2] { key, value });
			}

			// For this first version we assume that the order of fields
			// is always the same in the response.
			var cult = new CultureInfo("el-GR");

			/* Domain */
			parsed.Domain.Handle = pairs[1][1];
			parsed.Domain.ProtocolNumber = pairs[2][1];
			parsed.Domain.CreationDate = DateTime.Parse(pairs[3][1], cult);
			parsed.Domain.ExpirationDate = DateTime.Parse(pairs[4][1], cult);
			parsed.Domain.LastUpdate = DateTime.Parse(pairs[5][1], cult);

			/* Current Registrar */
			parsed.Registrar.Name = pairs[6][1];
			parsed.Registrar.Url = pairs[7][1];
			parsed.Registrar.Email = pairs[8][1];
			parsed.Registrar.Phone = pairs[9][1];

			/* Name Servers */
			for (var i = 12; i < pairs.Count; i++)
			{
				// Create the name server.
				var ns = new NameServer();

				// Fill it.
				ns.Name = pairs[i][1];

				// Add it to the list.
				parsed.NameServers.Add(ns);
			}

			// We are done here.
			return parsed;
		}
	}
}
