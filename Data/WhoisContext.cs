using System;
using KowWhoisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KowWhoisApi.Data
{
	public class WhoisContext : DbContext
	{
		public DbSet<Registrar> Registrars { get; set; }
		public DbSet<Domain> Domains { get; set; }
		public DbSet<NameServer> NameServers { get; set; }
		public DbSet<Snapshot> Snapshots { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<AddressSet> AddressSets { get; set; }
		public DbSet<SnapshotNameServer> NameServerSnapshots { get; set; }
		public DbSet<AddressSetAddress> AddressSetAddresses { get; set; }

		public WhoisContext(DbContextOptions<WhoisContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Domain
			modelBuilder.Entity<Domain>(entity =>
			{
				entity.HasIndex(e => e.Name).IsUnique();
				entity.HasIndex(e => e.Handle).IsUnique();
			});

			// Registrar
			modelBuilder.Entity<Registrar>().HasIndex(e => e.Name).IsUnique();

			// NameServer
			modelBuilder.Entity<NameServer>().HasIndex(e => e.Name).IsUnique();

			// Address
			modelBuilder.Entity<Address>(entity =>
			{
				entity.HasIndex(e => e.Ip).IsUnique();
				entity.Property(e => e.Addr).HasComputedColumnSql("INET6_NTOA(ip)");
			});

			// NameServer - Snapshot Relationship
			modelBuilder.Entity<SnapshotNameServer>()
				.HasKey(sns => new { sns.SnapshotId, sns.NameServerId });

			// Address - AddressSet Relationship
			modelBuilder.Entity<AddressSetAddress>()
				.HasKey(asa => new { asa.AddressSetId, asa.AddressId });
		}
	}
}
