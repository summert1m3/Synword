﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Synword.Persistence.Synword;

#nullable disable

namespace Synword.Persistence.Synword.Migrations
{
    [DbContext(typeof(SynwordContext))]
    [Migration("20220714030913_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("Synword.Domain.Entities.HistoryAggregate.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("Synword.Domain.Entities.PlagiarismCheckAggregate.HighlightRange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EndIndex")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MatchedUrlId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlagiarismCheckResultId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StartIndex")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MatchedUrlId");

                    b.HasIndex("PlagiarismCheckResultId");

                    b.ToTable("HighlightRanges");
                });

            modelBuilder.Entity("Synword.Domain.Entities.PlagiarismCheckAggregate.MatchedUrl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Percent")
                        .HasColumnType("REAL");

                    b.Property<int?>("PlagiarismCheckResultId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PlagiarismCheckResultId");

                    b.ToTable("MatchedUrls");
                });

            modelBuilder.Entity("Synword.Domain.Entities.PlagiarismCheckAggregate.PlagiarismCheckResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Error")
                        .HasColumnType("TEXT");

                    b.Property<int>("HistoryId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Percent")
                        .HasColumnType("REAL");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HistoryId");

                    b.HasIndex("UserId");

                    b.ToTable("PlagiarismCheckHistories");
                });

            modelBuilder.Entity("Synword.Domain.Entities.RephraseAggregate.RephraseResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("HistoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RephrasedText")
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HistoryId");

                    b.HasIndex("UserId");

                    b.ToTable("RephraseHistories");
                });

            modelBuilder.Entity("Synword.Domain.Entities.RephraseAggregate.SourceWordSynonyms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RephraseResultId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SourceWord")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SynonymWordEndIndex")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SynonymWordStartIndex")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RephraseResultId");

                    b.ToTable("SourceWordSynonyms");
                });

            modelBuilder.Entity("Synword.Domain.Entities.RephraseAggregate.Synonym", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SourceWordSynonymsId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Synonym");

                    b.HasKey("Id");

                    b.HasIndex("SourceWordSynonymsId");

                    b.ToTable("Synonyms");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.Metadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("Locale")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Picture")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Metadata");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PurchaseToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.UsageData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastVisitDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlagiarismCheckCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RephraseCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("UsageData");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MetadataId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UsageDataId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MetadataId");

                    b.HasIndex("UsageDataId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Synword.Domain.Entities.PlagiarismCheckAggregate.HighlightRange", b =>
                {
                    b.HasOne("Synword.Domain.Entities.PlagiarismCheckAggregate.MatchedUrl", null)
                        .WithMany("Highlights")
                        .HasForeignKey("MatchedUrlId");

                    b.HasOne("Synword.Domain.Entities.PlagiarismCheckAggregate.PlagiarismCheckResult", null)
                        .WithMany("Highlights")
                        .HasForeignKey("PlagiarismCheckResultId");
                });

            modelBuilder.Entity("Synword.Domain.Entities.PlagiarismCheckAggregate.MatchedUrl", b =>
                {
                    b.HasOne("Synword.Domain.Entities.PlagiarismCheckAggregate.PlagiarismCheckResult", null)
                        .WithMany("Matches")
                        .HasForeignKey("PlagiarismCheckResultId");
                });

            modelBuilder.Entity("Synword.Domain.Entities.PlagiarismCheckAggregate.PlagiarismCheckResult", b =>
                {
                    b.HasOne("Synword.Domain.Entities.HistoryAggregate.History", "History")
                        .WithMany()
                        .HasForeignKey("HistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Synword.Domain.Entities.UserAggregate.User", null)
                        .WithMany("PlagiarismCheckHistory")
                        .HasForeignKey("UserId");

                    b.Navigation("History");
                });

            modelBuilder.Entity("Synword.Domain.Entities.RephraseAggregate.RephraseResult", b =>
                {
                    b.HasOne("Synword.Domain.Entities.HistoryAggregate.History", "History")
                        .WithMany()
                        .HasForeignKey("HistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Synword.Domain.Entities.UserAggregate.User", null)
                        .WithMany("RephraseHistory")
                        .HasForeignKey("UserId");

                    b.Navigation("History");
                });

            modelBuilder.Entity("Synword.Domain.Entities.RephraseAggregate.SourceWordSynonyms", b =>
                {
                    b.HasOne("Synword.Domain.Entities.RephraseAggregate.RephraseResult", null)
                        .WithMany("Synonyms")
                        .HasForeignKey("RephraseResultId");
                });

            modelBuilder.Entity("Synword.Domain.Entities.RephraseAggregate.Synonym", b =>
                {
                    b.HasOne("Synword.Domain.Entities.RephraseAggregate.SourceWordSynonyms", null)
                        .WithMany("Synonyms")
                        .HasForeignKey("SourceWordSynonymsId");
                });

            modelBuilder.Entity("Synword.Domain.Entities.UserAggregate.Metadata", b =>
                {
                    b.OwnsOne("Synword.Domain.Entities.UserAggregate.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<int>("MetadataId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("TEXT")
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
                                .HasColumnType("TEXT");

                            b1.Property<int>("Value")
                                .HasColumnType("INTEGER")
                                .HasColumnName("Coins");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Synword.Domain.Entities.UserAggregate.ValueObjects.Ip", "Ip", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Ip");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Coins")
                        .IsRequired();

                    b.Navigation("Ip")
                        .IsRequired();

                    b.Navigation("Metadata");

                    b.Navigation("UsageData");
                });

            modelBuilder.Entity("Synword.Domain.Entities.PlagiarismCheckAggregate.MatchedUrl", b =>
                {
                    b.Navigation("Highlights");
                });

            modelBuilder.Entity("Synword.Domain.Entities.PlagiarismCheckAggregate.PlagiarismCheckResult", b =>
                {
                    b.Navigation("Highlights");

                    b.Navigation("Matches");
                });

            modelBuilder.Entity("Synword.Domain.Entities.RephraseAggregate.RephraseResult", b =>
                {
                    b.Navigation("Synonyms");
                });

            modelBuilder.Entity("Synword.Domain.Entities.RephraseAggregate.SourceWordSynonyms", b =>
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
