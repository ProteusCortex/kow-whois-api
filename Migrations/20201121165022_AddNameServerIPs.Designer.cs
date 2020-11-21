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
    [Migration("20201121165022_AddNameServerIPs")]
    partial class AddNameServerIPs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("KowWhoisApi.Models.Address", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Ip")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("longtext")
                        .HasColumnName("ip")
                        .HasComputedColumnSql("INET6_NTOA(ip_raw)");

                    b.Property<byte[]>("IpRaw")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varbinary(16)")
                        .HasColumnName("ip_raw");

                    b.Property<uint>("SnapshotNameServerNameServerId")
                        .HasColumnType("int unsigned")
                        .HasColumnName("nameserver_id");

                    b.Property<uint>("SnapshotNameServerSnapshotId")
                        .HasColumnType("int unsigned")
                        .HasColumnName("snapshot_id");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("IpRaw")
                        .IsUnique();

                    b.HasIndex("SnapshotNameServerSnapshotId", "SnapshotNameServerNameServerId");

                    b.ToTable("address");
                });

            modelBuilder.Entity("KowWhoisApi.Models.Domain", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("creation");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("expiration");

                    b.Property<string>("Handle")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("handle");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_update");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("ProtocolNumber")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("protonum");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

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
                        .HasColumnType("int unsigned")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("nameserver");
                });

            modelBuilder.Entity("KowWhoisApi.Models.Registrar", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<string>("Url")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("registrar");
                });

            modelBuilder.Entity("KowWhoisApi.Models.Snapshot", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<uint>("DomainId")
                        .HasColumnType("int unsigned")
                        .HasColumnName("domain_id");

                    b.Property<uint?>("RegistrarId")
                        .HasColumnType("int unsigned")
                        .HasColumnName("registrar_id");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.HasIndex("RegistrarId");

                    b.ToTable("snapshot");
                });

            modelBuilder.Entity("KowWhoisApi.Models.SnapshotNameServer", b =>
                {
                    b.Property<uint>("SnapshotId")
                        .HasColumnType("int unsigned")
                        .HasColumnName("snapshot_id");

                    b.Property<uint>("NameServerId")
                        .HasColumnType("int unsigned")
                        .HasColumnName("nameserver_id");

                    b.HasKey("SnapshotId", "NameServerId");

                    b.HasIndex("NameServerId");

                    b.ToTable("rel_snapshot_nameserver");
                });

            modelBuilder.Entity("KowWhoisApi.Models.Address", b =>
                {
                    b.HasOne("KowWhoisApi.Models.SnapshotNameServer", "SnapshotNameServer")
                        .WithMany("Addresses")
                        .HasForeignKey("SnapshotNameServerSnapshotId", "SnapshotNameServerNameServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SnapshotNameServer");
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

                    b.Navigation("Domain");

                    b.Navigation("Registrar");
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

                    b.Navigation("NameServer");

                    b.Navigation("Snapshot");
                });

            modelBuilder.Entity("KowWhoisApi.Models.Domain", b =>
                {
                    b.Navigation("Snapshots");
                });

            modelBuilder.Entity("KowWhoisApi.Models.NameServer", b =>
                {
                    b.Navigation("SnapshotNameServers");
                });

            modelBuilder.Entity("KowWhoisApi.Models.Registrar", b =>
                {
                    b.Navigation("Snapshots");
                });

            modelBuilder.Entity("KowWhoisApi.Models.Snapshot", b =>
                {
                    b.Navigation("SnapshotNameServers");
                });

            modelBuilder.Entity("KowWhoisApi.Models.SnapshotNameServer", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
