﻿// <auto-generated />
using System;
using Com.Dotnet.Cric.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace dotnet.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230708092604_MatchPlayerMap")]
    partial class MatchPlayerMap
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Country", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.GameType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("GameTypes");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.ManOfTheSeries", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<long>("SeriesId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .HasDatabaseName("Player");

                    b.HasIndex("SeriesId")
                        .HasDatabaseName("Series");

                    b.HasIndex("SeriesId", "PlayerId")
                        .IsUnique();

                    b.ToTable("ManOfTheSeries");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long?>("BatFirstId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsOfficial")
                        .HasColumnType("bit");

                    b.Property<byte>("ResultTypeId")
                        .HasColumnType("tinyint");

                    b.Property<long>("SeriesId")
                        .HasColumnType("bigint");

                    b.Property<long>("StadiumId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("Team1Id")
                        .HasColumnType("bigint");

                    b.Property<long>("Team2Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("TossWinnerId")
                        .HasColumnType("bigint");

                    b.Property<int?>("WinMargin")
                        .HasColumnType("int");

                    b.Property<byte?>("WinMarginTypeId")
                        .HasColumnType("tinyint");

                    b.Property<long?>("WinnerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BatFirstId")
                        .HasDatabaseName("BatFirst");

                    b.HasIndex("ResultTypeId")
                        .HasDatabaseName("ResultType");

                    b.HasIndex("SeriesId")
                        .HasDatabaseName("Series");

                    b.HasIndex("StadiumId")
                        .HasDatabaseName("Stadium");

                    b.HasIndex("Team1Id")
                        .HasDatabaseName("Team1");

                    b.HasIndex("Team2Id")
                        .HasDatabaseName("Team2");

                    b.HasIndex("TossWinnerId")
                        .HasDatabaseName("TossWinner");

                    b.HasIndex("WinMarginTypeId")
                        .HasDatabaseName("WinMarginType");

                    b.HasIndex("WinnerId")
                        .HasDatabaseName("Winner");

                    b.HasIndex("StadiumId", "StartTime")
                        .IsUnique();

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.MatchPlayerMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MatchId")
                        .HasDatabaseName("Match");

                    b.HasIndex("PlayerId")
                        .HasDatabaseName("Player");

                    b.HasIndex("TeamId");

                    b.HasIndex("MatchId", "PlayerId", "TeamId")
                        .IsUnique()
                        .HasDatabaseName("UK_MPM_Match_Player_Team");

                    b.ToTable("MatchPlayerMaps");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId")
                        .HasDatabaseName("country");

                    b.HasIndex("Name", "CountryId", "DateOfBirth")
                        .IsUnique();

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.ResultType", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ResultTypes");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Series", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("GameTypeId")
                        .HasColumnType("int");

                    b.Property<long>("HomeCountryId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("TourId")
                        .HasColumnType("bigint");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameTypeId")
                        .HasDatabaseName("GameType");

                    b.HasIndex("HomeCountryId")
                        .HasDatabaseName("HomeCountry");

                    b.HasIndex("TourId")
                        .HasDatabaseName("Tour");

                    b.HasIndex("TypeId")
                        .HasDatabaseName("Type");

                    b.HasIndex("Name", "TourId", "GameTypeId")
                        .IsUnique();

                    b.ToTable("Series");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.SeriesTeamsMap", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("SeriesId")
                        .HasColumnType("bigint");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId")
                        .HasDatabaseName("Series");

                    b.HasIndex("TeamId")
                        .HasDatabaseName("Team");

                    b.HasIndex("SeriesId", "TeamId")
                        .IsUnique();

                    b.ToTable("SeriesTeamsMap");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.SeriesType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("SeriesTypes");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Stadium", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("State")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId")
                        .HasDatabaseName("country");

                    b.HasIndex("Name", "CountryId")
                        .IsUnique();

                    b.ToTable("Stadiums");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Team", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId")
                        .HasDatabaseName("country");

                    b.HasIndex("TypeId");

                    b.HasIndex("Name", "CountryId", "TypeId")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.TeamType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("TeamTypes");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Tour", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Name", "StartTime")
                        .IsUnique();

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.WinMarginType", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("WinMarginTypes");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.ManOfTheSeries", b =>
                {
                    b.HasOne("Com.Dotnet.Cric.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Com.Dotnet.Cric.Models.Series", "Series")
                        .WithMany()
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Series");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Match", b =>
                {
                    b.HasOne("Com.Dotnet.Cric.Models.Team", "BatFirst")
                        .WithMany()
                        .HasForeignKey("BatFirstId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Com.Dotnet.Cric.Models.ResultType", "ResultType")
                        .WithMany()
                        .HasForeignKey("ResultTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Com.Dotnet.Cric.Models.Series", "Series")
                        .WithMany()
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Com.Dotnet.Cric.Models.Stadium", "Stadium")
                        .WithMany()
                        .HasForeignKey("StadiumId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Com.Dotnet.Cric.Models.Team", "Team1")
                        .WithMany()
                        .HasForeignKey("Team1Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Com.Dotnet.Cric.Models.Team", "Team2")
                        .WithMany()
                        .HasForeignKey("Team2Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Com.Dotnet.Cric.Models.Team", "TossWinner")
                        .WithMany()
                        .HasForeignKey("TossWinnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Com.Dotnet.Cric.Models.WinMarginType", "WinMarginType")
                        .WithMany()
                        .HasForeignKey("WinMarginTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Com.Dotnet.Cric.Models.Team", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("BatFirst");

                    b.Navigation("ResultType");

                    b.Navigation("Series");

                    b.Navigation("Stadium");

                    b.Navigation("Team1");

                    b.Navigation("Team2");

                    b.Navigation("TossWinner");

                    b.Navigation("WinMarginType");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.MatchPlayerMap", b =>
                {
                    b.HasOne("Com.Dotnet.Cric.Models.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_mpm_match");

                    b.HasOne("Com.Dotnet.Cric.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_mpm_player");

                    b.HasOne("Com.Dotnet.Cric.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_mpm_team");

                    b.Navigation("Match");

                    b.Navigation("Player");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Player", b =>
                {
                    b.HasOne("Com.Dotnet.Cric.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Series", b =>
                {
                    b.HasOne("Com.Dotnet.Cric.Models.GameType", "GameType")
                        .WithMany()
                        .HasForeignKey("GameTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Com.Dotnet.Cric.Models.Country", "HomeCountry")
                        .WithMany()
                        .HasForeignKey("HomeCountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Com.Dotnet.Cric.Models.Tour", "Tour")
                        .WithMany()
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Com.Dotnet.Cric.Models.SeriesType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GameType");

                    b.Navigation("HomeCountry");

                    b.Navigation("Tour");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.SeriesTeamsMap", b =>
                {
                    b.HasOne("Com.Dotnet.Cric.Models.Series", "Series")
                        .WithMany()
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Com.Dotnet.Cric.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Series");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Stadium", b =>
                {
                    b.HasOne("Com.Dotnet.Cric.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Com.Dotnet.Cric.Models.Team", b =>
                {
                    b.HasOne("Com.Dotnet.Cric.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Com.Dotnet.Cric.Models.TeamType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Type");
                });
#pragma warning restore 612, 618
        }
    }
}
