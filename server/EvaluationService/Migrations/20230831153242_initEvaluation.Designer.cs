﻿// <auto-generated />
using System;
using EvaluationService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Xphyrus.EvaluationAPI.Migrations
{
    [DbContext(typeof(ApplicatioDbContext))]
    [Migration("20230831153242_initEvaluation")]
    partial class initEvaluation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Xphyrus.EvaluationAPI.Models.UserSubmissionandSulition", b =>
                {
                    b.Property<int>("UserSubmissionandSulitionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserSubmissionandSulitionId"));

                    b.Property<int>("AssesmentId")
                        .HasColumnType("int");

                    b.Property<int>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedON")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<int>("LanguageCode")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("UserSubmissionandSulitionId");

                    b.ToTable("userSubmissionandSulitions");
                });
#pragma warning restore 612, 618
        }
    }
}
