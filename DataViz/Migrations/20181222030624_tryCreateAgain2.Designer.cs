﻿// <auto-generated />
using System;
using System.Collections.Generic;
using DataViz.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataViz.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20181222030624_tryCreateAgain2")]
    partial class tryCreateAgain2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("DataViz.Contracts.QueryCategories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<List<string>>("Columns");

                    b.Property<Dictionary<string, string>>("Conditionals");

                    b.HasKey("Id");

                    b.ToTable("QueryCategories");
                });

            modelBuilder.Entity("DataViz.Contracts.QueryPopover", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<List<string>>("Additional");

                    b.Property<bool>("Toggle");

                    b.HasKey("Id");

                    b.ToTable("QueryPopover");
                });

            modelBuilder.Entity("DataViz.Contracts.QueryRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Appearance");

                    b.Property<int?>("CategoriesId");

                    b.Property<string>("Filters");

                    b.Property<Dictionary<string, string>>("Functions");

                    b.Property<string>("GraphSubType");

                    b.Property<string>("GraphType");

                    b.Property<int?>("PopoverId");

                    b.Property<string>("TableName")
                        .IsRequired();

                    b.Property<string>("XCol");

                    b.Property<List<string>>("YCols");

                    b.HasKey("Id");

                    b.HasIndex("CategoriesId");

                    b.HasIndex("PopoverId");

                    b.ToTable("QueryRequests");
                });

            modelBuilder.Entity("DataViz.Contracts.QueryRequest", b =>
                {
                    b.HasOne("DataViz.Contracts.QueryCategories", "Categories")
                        .WithMany()
                        .HasForeignKey("CategoriesId");

                    b.HasOne("DataViz.Contracts.QueryPopover", "Popover")
                        .WithMany()
                        .HasForeignKey("PopoverId");
                });
#pragma warning restore 612, 618
        }
    }
}
