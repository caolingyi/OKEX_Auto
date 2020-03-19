﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OKEX.Auto.Core.Context;

namespace OKEX.Auto.Core.ORM.Migrations
{
    [DbContext(typeof(DefaultEFDBContext))]
    [Migration("20200319060724_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("OKEX.Auto.Core.Domain.AggregatesModel.BaseCurrency", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("can_deposit")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("can_withdraw")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("currency")
                        .IsRequired()
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("min_withdrawal")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("BaseCurrency");
                });
#pragma warning restore 612, 618
        }
    }
}