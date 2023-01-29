﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ODOL.Data;

#nullable disable

namespace ODOL.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230125010104_dbAddLog")]
    partial class dbAddLog
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ODOL.Models.LogBook", b =>
                {
                    b.Property<int>("idLogBook")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idLogBook"));

                    b.Property<int?>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Departemen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("JamEnd")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("JamStart")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Kegiatan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RincianKegiatan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Tanggal")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Ulasan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("idPembimbing")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("idLogBook");

                    b.ToTable("LogBook");
                });

            modelBuilder.Entity("ODOL.Models.Mahasiswa", b =>
                {
                    b.Property<string>("NIM")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdPerusahaan")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ModifBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NamaMahasiswa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prodi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NIM");

                    b.ToTable("Mahasiswa");
                });

            modelBuilder.Entity("ODOL.Models.Pembimbing", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailPembimbing")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jabatan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModifBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("idPengguna")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("idPerusahaan")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Pembimbing");
                });

            modelBuilder.Entity("ODOL.Models.Pengguna", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pengguna");
                });

            modelBuilder.Entity("ODOL.Models.Perusahaan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AlamatPerusahaan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cabang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailPerusahaan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Group")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModifBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("idPengguna")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Perusahaan");
                });
#pragma warning restore 612, 618
        }
    }
}