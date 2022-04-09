﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Synword.Infrastructure.Data;

#nullable disable

namespace Synword.Infrastructure.Data.Migrations
{
    [DbContext(typeof(UserDataContext))]
    [Migration("20220306220749_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.HighlightRange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EndIndex")
                        .HasColumnType("int");

                    b.Property<int?>("MatchedUrlId")
                        .HasColumnType("int");

                    b.Property<int?>("PlagiarismCheckHistoryId")
                        .HasColumnType("int");

                    b.Property<int>("StartIndex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MatchedUrlId");

                    b.HasIndex("PlagiarismCheckHistoryId");

                    b.ToTable("HighlightRanges");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.MatchedUrl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("Percent")
                        .HasColumnType("real");

                    b.Property<int?>("PlagiarismCheckHistoryId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlagiarismCheckHistoryId");

                    b.ToTable("MatchedUrls");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.Metadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Metadata");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PurchaseToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.PlagiarismCheckHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Percent")
                        .HasColumnType("real");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PlagiarismCheckHistories");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.RephraseHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("RephrasedText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RephraseHistories");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.Synonym", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("RephraseHistoryId")
                        .HasColumnType("int");

                    b.Property<int>("SynonymWordEndIndex")
                        .HasColumnType("int");

                    b.Property<int>("SynonymWordStartIndex")
                        .HasColumnType("int");

                    b.Property<string>("Synonyms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RephraseHistoryId");

                    b.ToTable("Synonyms");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.UsageData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastVisitDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlagiarismCheckCount")
                        .HasColumnType("int");

                    b.Property<int>("RephraseCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UsageData");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("MetadataId")
                        .HasColumnType("int");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsageDataId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MetadataId");

                    b.HasIndex("UsageDataId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.HighlightRange", b =>
                {
                    b.HasOne("Synword.Domain.Entities.UserAggregate.MatchedUrl", null)
                        .WithMany("Highlights")
                        .HasForeignKey("MatchedUrlId");

                    b.HasOne("Synword.Domain.Entities.UserAggregate.PlagiarismCheckHistory", null)
                        .WithMany("Highlight")
                        .HasForeignKey("PlagiarismCheckHistoryId");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.MatchedUrl", b =>
                {
                    b.HasOne("Synword.Domain.Entities.UserAggregate.PlagiarismCheckHistory", null)
                        .WithMany("Matches")
                        .HasForeignKey("PlagiarismCheckHistoryId");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.Metadata", b =>
                {
                    b.OwnsOne("Synword.Domain.Entities.UserAggregate.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<int>("MetadataId")
                                .HasColumnType("int");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Email");

                            b1.HasKey("MetadataId");

                            b1.ToTable("Metadata");

                            b1.WithOwner()
                                .HasForeignKey("MetadataId");
                        });

                    b.Navigation("Email");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.Order", b =>
                {
                    b.HasOne("Synword.Domain.Entities.UserAggregate.User", null)
                        .WithMany("Orders")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.PlagiarismCheckHistory", b =>
                {
                    b.HasOne("Synword.Domain.Entities.UserAggregate.User", "User")
                        .WithMany("PlagiarismCheckHistory")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.RephraseHistory", b =>
                {
                    b.HasOne("Synword.Domain.Entities.UserAggregate.User", "User")
                        .WithMany("RephraseHistory")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.Synonym", b =>
                {
                    b.HasOne("Synword.Domain.Entities.UserAggregate.RephraseHistory", null)
                        .WithMany("Synonyms")
                        .HasForeignKey("RephraseHistoryId");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.User", b =>
                {
                    b.HasOne("Synword.Domain.Entities.UserAggregate.Metadata", "Metadata")
                        .WithMany()
                        .HasForeignKey("MetadataId");

                    b.HasOne("Synword.Domain.Entities.UserAggregate.UsageData", "UsageData")
                        .WithMany()
                        .HasForeignKey("UsageDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Synword.Domain.Entities.UserAggregate.ValueObjects.Coins", "Coins", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("Coins");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Synword.Domain.Entities.UserAggregate.ValueObjects.ExternalSignIn", "ExternalSignIn", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Id")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.HasIndex("Id")
                                .IsUnique()
                                .HasFilter("[ExternalSignIn_Id] IS NOT NULL");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Synword.Domain.Entities.UserAggregate.ValueObjects.Ip", "Ip", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Ip");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Coins")
                        .IsRequired();

                    b.Navigation("ExternalSignIn");

                    b.Navigation("Ip")
                        .IsRequired();

                    b.Navigation("Metadata");

                    b.Navigation("UsageData");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.MatchedUrl", b =>
                {
                    b.Navigation("Highlights");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.PlagiarismCheckHistory", b =>
                {
                    b.Navigation("Highlight");

                    b.Navigation("Matches");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.RephraseHistory", b =>
                {
                    b.Navigation("Synonyms");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("PlagiarismCheckHistory");

                    b.Navigation("RephraseHistory");
                });
#pragma warning restore 612, 618
        }
    }
}