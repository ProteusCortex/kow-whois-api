﻿// <auto-generated />
using System;
using KowWhoisApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KowWhoisApi.Migrations
{
	[DbContext(typeof(WhoisContext))]
	[Migration("20191227152421_AddNameServerAddressSets")]
	partial class AddNameServerAddressSets
	{
		protected override void BuildTargetModel(ModelBuilder modelBuilder)
		{
#pragma warning disable 612, 618
			modelBuilder
				.HasAnnotation("ProductVersion", "3.1.0")
				.HasAnnotation("Relational:MaxIdentifierLength", 64);

			modelBuilder.Entity("KowWhoisApi.Models.Address", b =>
				{
					b.Property<uint>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnName("id")
						.HasColumnType("int unsigned");

					b.Property<string>("Addr")
						.IsRequired()
						.ValueGeneratedOnAddOrUpdate()
						.HasColumnName("addr")
						.HasColumnType("varchar(45)")
						.HasComputedColumnSql("INET6_NTOA(ip)")
						.HasMaxLength(45);

					b.Property<DateTime>("CreatedAt")
						.ValueGeneratedOnAdd()
						.HasColumnName("created_at")
						.HasColumnType("datetime(6)");

					b.Property<byte[]>("Ip")
						.IsRequired()
						.HasColumnName("ip")
						.HasColumnType("varbinary(16)")
						.HasMaxLength(16);

					b.Property<DateTime>("UpdatedAt")
						.ValueGeneratedOnAddOrUpdate()
						.HasColumnName("updated_at")
						.HasColumnType("datetime(6)");

					b.HasKey("Id");

					b.HasIndex("Ip")
						.IsUnique();

					b.ToTable("address");
				});

			modelBuilder.Entity("KowWhoisApi.Models.AddressSet", b =>
				{
					b.Property<uint>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnName("id")
						.HasColumnType("int unsigned");

					b.Property<DateTime>("CreatedAt")
						.ValueGeneratedOnAdd()
						.HasColumnName("created_at")
						.HasColumnType("datetime(6)");

					b.Property<uint>("NameServerId")
						.HasColumnName("nameserver_id")
						.HasColumnType("int unsigned");

					b.Property<DateTime>("UpdatedAt")
						.ValueGeneratedOnAddOrUpdate()
						.HasColumnName("updated_at")
						.HasColumnType("datetime(6)");

					b.HasKey("Id");

					b.HasIndex("NameServerId");

					b.ToTable("addressset");
				});

			modelBuilder.Entity("KowWhoisApi.Models.AddressSetAddress", b =>
				{
					b.Property<uint>("AddressSetId")
						.HasColumnName("addressset_id")
						.HasColumnType("int unsigned");

					b.Property<uint>("AddressId")
						.HasColumnName("address_id")
						.HasColumnType("int unsigned");

					b.HasKey("AddressSetId", "AddressId");

					b.HasIndex("AddressId");

					b.ToTable("rel_addressset_address");
				});

			modelBuilder.Entity("KowWhoisApi.Models.Domain", b =>
				{
					b.Property<uint>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnName("id")
						.HasColumnType("int unsigned");

					b.Property<DateTime>("CreatedAt")
						.ValueGeneratedOnAdd()
						.HasColumnName("created_at")
						.HasColumnType("datetime(6)");

					b.Property<DateTime?>("CreationDate")
						.HasColumnName("creation")
						.HasColumnType("datetime(6)");

					b.Property<DateTime?>("ExpirationDate")
						.HasColumnName("expiration")
						.HasColumnType("datetime(6)");

					b.Property<string>("Handle")
						.HasColumnName("handle")
						.HasColumnType("varchar(255)");

					b.Property<DateTime?>("LastUpdate")
						.HasColumnName("last_update")
						.HasColumnType("datetime(6)");

					b.Property<string>("Name")
						.IsRequired()
						.HasColumnName("name")
						.HasColumnType("varchar(255)");

					b.Property<string>("ProtocolNumber")
						.HasColumnName("protonum")
						.HasColumnType("varchar(128)")
						.HasMaxLength(128);

					b.Property<DateTime>("UpdatedAt")
						.ValueGeneratedOnAddOrUpdate()
						.HasColumnName("updated_at")
						.HasColumnType("datetime(6)");

					b.HasKey("Id");

					b.HasIndex("Handle")
						.IsUnique();

					b.HasIndex("Name")
						.IsUnique();

					b.ToTable("domain");
				});

			modelBuilder.Entity("KowWhoisApi.Models.NameServer", b =>
				{
					b.Property<uint>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnName("id")
						.HasColumnType("int unsigned");

					b.Property<DateTime>("CreatedAt")
						.ValueGeneratedOnAdd()
						.HasColumnName("created_at")
						.HasColumnType("datetime(6)");

					b.Property<string>("Name")
						.IsRequired()
						.HasColumnName("name")
						.HasColumnType("varchar(255)");

					b.Property<DateTime>("UpdatedAt")
						.ValueGeneratedOnAddOrUpdate()
						.HasColumnName("updated_at")
						.HasColumnType("datetime(6)");

					b.HasKey("Id");

					b.HasIndex("Name")
						.IsUnique();

					b.ToTable("nameserver");
				});

			modelBuilder.Entity("KowWhoisApi.Models.Registrar", b =>
				{
					b.Property<uint>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnName("id")
						.HasColumnType("int unsigned");

					b.Property<DateTime>("CreatedAt")
						.ValueGeneratedOnAdd()
						.HasColumnName("created_at")
						.HasColumnType("datetime(6)");

					b.Property<string>("Email")
						.HasColumnName("email")
						.HasColumnType("varchar(255)")
						.HasMaxLength(255);

					b.Property<string>("Name")
						.IsRequired()
						.HasColumnName("name")
						.HasColumnType("varchar(255)")
						.HasMaxLength(255);

					b.Property<string>("Phone")
						.HasColumnName("phone")
						.HasColumnType("varchar(20)")
						.HasMaxLength(20);

					b.Property<DateTime>("UpdatedAt")
						.ValueGeneratedOnAddOrUpdate()
						.HasColumnName("updated_at")
						.HasColumnType("datetime(6)");

					b.Property<string>("Url")
						.HasColumnName("url")
						.HasColumnType("varchar(255)")
						.HasMaxLength(255);

					b.HasKey("Id");

					b.HasIndex("Name")
						.IsUnique();

					b.ToTable("registrar");
				});

			modelBuilder.Entity("KowWhoisApi.Models.Snapshot", b =>
				{
					b.Property<uint>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnName("id")
						.HasColumnType("int unsigned");

					b.Property<DateTime>("CreatedAt")
						.ValueGeneratedOnAdd()
						.HasColumnName("created_at")
						.HasColumnType("datetime(6)");

					b.Property<uint>("DomainId")
						.HasColumnName("domain_id")
						.HasColumnType("int unsigned");

					b.Property<uint?>("RegistrarId")
						.HasColumnName("registrar_id")
						.HasColumnType("int unsigned");

					b.Property<DateTime>("UpdatedAt")
						.ValueGeneratedOnAddOrUpdate()
						.HasColumnName("updated_at")
						.HasColumnType("datetime(6)");

					b.HasKey("Id");

					b.HasIndex("DomainId");

					b.HasIndex("RegistrarId");

					b.ToTable("snapshot");
				});

			modelBuilder.Entity("KowWhoisApi.Models.SnapshotNameServer", b =>
				{
					b.Property<uint>("SnapshotId")
						.HasColumnName("snapshot_id")
						.HasColumnType("int unsigned");

					b.Property<uint>("NameServerId")
						.HasColumnName("nameserver_id")
						.HasColumnType("int unsigned");

					b.HasKey("SnapshotId", "NameServerId");

					b.HasIndex("NameServerId");

					b.ToTable("rel_snapshot_nameserver");
				});

			modelBuilder.Entity("KowWhoisApi.Models.AddressSet", b =>
				{
					b.HasOne("KowWhoisApi.Models.NameServer", null)
						.WithMany("AddressSets")
						.HasForeignKey("NameServerId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("KowWhoisApi.Models.AddressSetAddress", b =>
				{
					b.HasOne("KowWhoisApi.Models.Address", "Address")
						.WithMany()
						.HasForeignKey("AddressId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();

					b.HasOne("KowWhoisApi.Models.AddressSet", "AddressSet")
						.WithMany()
						.HasForeignKey("AddressSetId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("KowWhoisApi.Models.Snapshot", b =>
				{
					b.HasOne("KowWhoisApi.Models.Domain", "Domain")
						.WithMany("Snapshots")
						.HasForeignKey("DomainId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();

					b.HasOne("KowWhoisApi.Models.Registrar", "Registrar")
						.WithMany("Snapshots")
						.HasForeignKey("RegistrarId");
				});

			modelBuilder.Entity("KowWhoisApi.Models.SnapshotNameServer", b =>
				{
					b.HasOne("KowWhoisApi.Models.NameServer", "NameServer")
						.WithMany("SnapshotNameServers")
						.HasForeignKey("NameServerId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();

					b.HasOne("KowWhoisApi.Models.Snapshot", "Snapshot")
						.WithMany("SnapshotNameServers")
						.HasForeignKey("SnapshotId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});
#pragma warning restore 612, 618
		}
	}
}
