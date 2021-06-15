﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NonProfitProject.Models;

namespace NonProfitProject.Migrations
{
    [DbContext(typeof(NonProfitContext))]
    partial class NonProfitContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("NonProfitProject.Models.CommitteeMembers", b =>
                {
                    b.Property<int>("CommitteeMbrID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommitteeID")
                        .HasColumnType("int");

                    b.Property<string>("CommitteePosition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employeeEmpID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CommitteeMbrID");

                    b.HasIndex("CommitteeID");

                    b.HasIndex("employeeEmpID");

                    b.ToTable("CommitteeMembers");
                });

            modelBuilder.Entity("NonProfitProject.Models.Committees", b =>
                {
                    b.Property<int>("CommitteesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CommitteeCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CommitteeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommitteeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommitteesID");

                    b.ToTable("Committees");
                });

            modelBuilder.Entity("NonProfitProject.Models.DonationRecipts", b =>
                {
                    b.Property<int>("DonationRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PaymentID")
                        .HasColumnType("int");

                    b.Property<string>("ReciptDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReciptDonationID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DonationRecId");

                    b.HasIndex("PaymentID");

                    b.HasIndex("ReciptDonationID")
                        .IsUnique()
                        .HasFilter("[ReciptDonationID] IS NOT NULL");

                    b.HasIndex("UserID");

                    b.ToTable("DonationRecipts");
                });

            modelBuilder.Entity("NonProfitProject.Models.Donations", b =>
                {
                    b.Property<string>("DonationID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DonationAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DonationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("DonationID");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("NonProfitProject.Models.Employees", b =>
                {
                    b.Property<string>("EmpID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EmpID");

                    b.HasIndex("UserID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("NonProfitProject.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommitteeID")
                        .HasColumnType("int");

                    b.Property<string>("EventDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EventEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EventStartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EventID");

                    b.HasIndex("CommitteeID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("NonProfitProject.Models.MembershipDues", b =>
                {
                    b.Property<string>("MemDuesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MemActive")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<decimal>("MemDueAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("MemEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MemRenewalDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MemStartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MemDuesID");

                    b.ToTable("MembershipDues");
                });

            modelBuilder.Entity("NonProfitProject.Models.News", b =>
                {
                    b.Property<int>("NewsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommitteeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("NewsCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewsFooter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewsHeader")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NewsLastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NewsPublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewsTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NewsID");

                    b.HasIndex("CommitteeID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("NonProfitProject.Models.PaymentInformation", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CVV")
                        .HasColumnType("int");

                    b.Property<string>("CardNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardholderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PaymentID");

                    b.HasIndex("UserID");

                    b.ToTable("PaymentInformation");
                });

            modelBuilder.Entity("NonProfitProject.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserActive")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("UserAddr1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAddr2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserBirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserGender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserLastActivity")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserPostalCode")
                        .HasColumnType("int");

                    b.Property<string>("UserState")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("NonProfitProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NonProfitProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NonProfitProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("NonProfitProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NonProfitProject.Models.CommitteeMembers", b =>
                {
                    b.HasOne("NonProfitProject.Models.Committees", "committee")
                        .WithMany("committeeMembers")
                        .HasForeignKey("CommitteeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NonProfitProject.Models.Employees", "employee")
                        .WithMany("committeeMembers")
                        .HasForeignKey("employeeEmpID");

                    b.Navigation("committee");

                    b.Navigation("employee");
                });

            modelBuilder.Entity("NonProfitProject.Models.DonationRecipts", b =>
                {
                    b.HasOne("NonProfitProject.Models.PaymentInformation", "paymentInformation")
                        .WithMany("donationRecipts")
                        .HasForeignKey("PaymentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NonProfitProject.Models.Donations", "donations")
                        .WithOne("donationRecipts")
                        .HasForeignKey("NonProfitProject.Models.DonationRecipts", "ReciptDonationID");

                    b.HasOne("NonProfitProject.Models.MembershipDues", "membershipDues")
                        .WithOne("donationRecipts")
                        .HasForeignKey("NonProfitProject.Models.DonationRecipts", "ReciptDonationID");

                    b.HasOne("NonProfitProject.Models.User", "user")
                        .WithMany("donationRecipts")
                        .HasForeignKey("UserID");

                    b.Navigation("donations");

                    b.Navigation("membershipDues");

                    b.Navigation("paymentInformation");

                    b.Navigation("user");
                });

            modelBuilder.Entity("NonProfitProject.Models.Employees", b =>
                {
                    b.HasOne("NonProfitProject.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("user");
                });

            modelBuilder.Entity("NonProfitProject.Models.Event", b =>
                {
                    b.HasOne("NonProfitProject.Models.Committees", "committee")
                        .WithMany("events")
                        .HasForeignKey("CommitteeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("committee");
                });

            modelBuilder.Entity("NonProfitProject.Models.News", b =>
                {
                    b.HasOne("NonProfitProject.Models.Committees", "committee")
                        .WithMany("news")
                        .HasForeignKey("CommitteeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("committee");
                });

            modelBuilder.Entity("NonProfitProject.Models.PaymentInformation", b =>
                {
                    b.HasOne("NonProfitProject.Models.User", "user")
                        .WithMany("payments")
                        .HasForeignKey("UserID");

                    b.Navigation("user");
                });

            modelBuilder.Entity("NonProfitProject.Models.Committees", b =>
                {
                    b.Navigation("committeeMembers");

                    b.Navigation("events");

                    b.Navigation("news");
                });

            modelBuilder.Entity("NonProfitProject.Models.Donations", b =>
                {
                    b.Navigation("donationRecipts");
                });

            modelBuilder.Entity("NonProfitProject.Models.Employees", b =>
                {
                    b.Navigation("committeeMembers");
                });

            modelBuilder.Entity("NonProfitProject.Models.MembershipDues", b =>
                {
                    b.Navigation("donationRecipts");
                });

            modelBuilder.Entity("NonProfitProject.Models.PaymentInformation", b =>
                {
                    b.Navigation("donationRecipts");
                });

            modelBuilder.Entity("NonProfitProject.Models.User", b =>
                {
                    b.Navigation("donationRecipts");

                    b.Navigation("payments");
                });
#pragma warning restore 612, 618
        }
    }
}
