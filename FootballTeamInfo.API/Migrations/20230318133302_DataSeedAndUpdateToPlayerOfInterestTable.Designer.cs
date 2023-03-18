﻿// <auto-generated />
using FootballTeamInfo.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FootballTeamInfo.API.Migrations
{
    [DbContext(typeof(FootbalTeamInfoContext))]
    [Migration("20230318133302_DataSeedAndUpdateToPlayerOfInterestTable")]
    partial class DataSeedAndUpdateToPlayerOfInterestTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("FootballTeamInfo.API.Entities.FootballTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FootballTeams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Arsenal Football Club is an English professional football club based in Islington, London. Arsenal plays in the Premier League, the top flight of English football.",
                            Name = "Arsenal"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Futbol Club Barcelona, commonly referred to as Barcelona and colloquially known as Barça, is a professional football club based in Barcelona, Catalonia, Spain, that competes in La Liga, the top flight of Spanish football.",
                            Name = "Barcelona"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Real Madrid Club de Fútbol, commonly referred to as Real Madrid, is a Spanish professional football club based in Madrid. Founded in 1902 as Madrid Football Club, the club has traditionally worn a white home kit since its inception.",
                            Name = "Real Madrid"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Liverpool Football Club is a professional football club based in Liverpool, England. The club competes in the Premier League, the top tier of English football. Founded in 1892, the club joined the Football League the following year and has played its home games at Anfield since its formation.",
                            Name = "Liverpool"
                        });
                });

            modelBuilder.Entity("FootballTeamInfo.API.Entities.PlayerOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("FootballTeamId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FootballTeamId");

                    b.ToTable("PlayerOfInterests");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Record goal Scorer",
                            FootballTeamId = 1,
                            Name = "Thierry Henry"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Best football player ever",
                            FootballTeamId = 2,
                            Name = "Lionel Messi"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Best player in club history",
                            FootballTeamId = 3,
                            Name = "Cristiano Ronaldo"
                        },
                        new
                        {
                            Id = 4,
                            Description = "One of the best players in club history",
                            FootballTeamId = 4,
                            Name = "Steven Gerrard"
                        });
                });

            modelBuilder.Entity("FootballTeamInfo.API.Entities.PlayerOfInterest", b =>
                {
                    b.HasOne("FootballTeamInfo.API.Entities.FootballTeam", "FootballTeam")
                        .WithMany("PlayersOfInterest")
                        .HasForeignKey("FootballTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FootballTeam");
                });

            modelBuilder.Entity("FootballTeamInfo.API.Entities.FootballTeam", b =>
                {
                    b.Navigation("PlayersOfInterest");
                });
#pragma warning restore 612, 618
        }
    }
}