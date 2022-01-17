﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NonProfitProject.Models.Settings;
using Microsoft.Extensions.Options;

namespace NonProfitProject.Models
{
    //generates the database and is used to retreive items from the database
    public class NonProfitContext : IdentityDbContext<User>
    {
        private readonly AdminAccountInformation _settings;
        public NonProfitContext(DbContextOptions<NonProfitContext> options, IOptions<AdminAccountInformation> settings) : base(options) { _settings = settings.Value; }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<CommitteeMembers> CommitteeMembers { get; set; }
        public DbSet<Committees> Committees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<SavedPaymentInformation> SavedPayments { get; set; }
        public DbSet<InvoicePayment> InvoicePayments { get; set; }
        public DbSet<Donations> Donations { get; set; }
        public DbSet<Receipts> Receipts { get; set; }
        public DbSet<InvoiceDonorInformation> InvoiceDonorInformation { get; set; }
        public DbSet<MembershipDues> MembershipDues { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }

        //inserts infoirmation into the table
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MembershipType>().HasData(
                new MembershipType
                {
                    MembershipTypeID = "fdsfdf-n53g3g-h3j9xe768w-nm4b35",
                    Name = "Basic",
                    Amount = 10.00m
                },
                new MembershipType
                {
                    MembershipTypeID = "fs8t4h-chgje6-dshuv57d8-sng94v",
                    Name = "Advanced",
                    Amount = 20.00m
                },
                new MembershipType
                {
                    MembershipTypeID = "dk5k4g-df5h7d-v5y8s2ch5t-f5h5db",
                    Name = "Premium",
                    Amount = 50.00m
                },
                new MembershipType
                {
                    MembershipTypeID = "jk5dgd-eh4d6h-f5sf4g77h5-dfs4g",
                    Name = "Paw-fect",
                    Amount = 100.00m
                }
                );

            //creates employee for user --seeded so there isnt an error in the migration files  when updating database
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            });
            var hasher = new PasswordHasher<User>();
            builder.Entity<User>().HasData(
                new User
                {
                    Id = "6b87b89f-0f9a-4e2d-b696-235e99655521",
                    UserName = "JohnJones",
                    Email = "JohnJones@gmail.com",
                    UserFirstName = "John",
                    UserLastName = "Jones",
                    UserAddr1 = "513 S Augusta St",
                    UserCity = "Greenville",
                    UserState = "South Carolina",
                    UserPostalCode = 29607,
                    UserGender = "Male",
                    UserBirthDate = new DateTime(1987, 6, 13),
                    UserActive = true,
                    ReceiveWeeklyNewsletter = false
                });
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                    UserId = "6b87b89f-0f9a-4e2d-b696-235e99655521"
                });
            //Create News
            builder.Entity<News>().HasData(
                new News
                {
                    NewsID = 1,
                    NewsTitle = "Non-PAW-Fit Raise Awareness",
                    NewsHeader = "New Members are Wecome",
                    NewsPublishDate = DateTime.UtcNow,
                    NewsLastUpdated = DateTime.UtcNow,
                    NewsBody = "The Non-PAW-Fit Animal Rescue started their non-profit organization to raise awareness of abandoned pets across the United States.  Then mission:  To Rescue Pets from unwanted homes and provide them new home where they are become part of the family.",
                    CreatedBy = "Unknown"
                },
                new News
                {
                    NewsID = 2,
                    NewsTitle = "Non-PAW-Fit Rescued Over 50",
                    NewsHeader = "50 Animals Rescued From Unwanted Homes",
                    NewsPublishDate = DateTime.UtcNow,
                    NewsLastUpdated = DateTime.UtcNow,
                    NewsBody = "Pets from cats and dogs to parrots and snakes are being rescued from unwanted homes and given a place to stay until they find their forever home.",
                    CreatedBy = "Unknown"
                });
            //creates data for employees
            builder.Entity<Employees>().HasData(
                new Employees
                {
                    EmpID = "1",
                    UserID = "6b87b89f-0f9a-4e2d-b696-235e99655521",
                    Position = "Accountant",
                    Salary = 54000,
                    HireDate = new DateTime(2020, 02, 04, 11, 14, 0),
                    ReleaseDate = null
                });
            //Creates data for committees
            builder.Entity<Committees>().HasData(
                new Committees
                {
                    CommitteesID = 1,
                    CommitteeName = "Fundrasing Committee",
                    CommitteeDescription = "Manages donations/membership dues",
                    CommitteeCreationDate = DateTime.UtcNow
                },
                new Committees
                {
                    CommitteesID = 2,
                    CommitteeName = "News Committee",
                    CommitteeDescription = "Manages news on the website",
                    CommitteeCreationDate = DateTime.UtcNow
                },
                new Committees
                {
                    CommitteesID = 3,
                    CommitteeName = "Event and Planning Committee",
                    CommitteeDescription = "Plans and organizes events",
                    CommitteeCreationDate = DateTime.UtcNow
                }
                );
            //creates data for Committee Members
            builder.Entity<CommitteeMembers>().HasData(
                new CommitteeMembers
                {
                    CommitteeMbrID = 1,
                    EmpID = "1",
                    CommitteeID = 1,
                    CommitteePosition = "Committee President"
                }
                );
            //creates data for Event table
            builder.Entity<Event>().HasData(
            new Event
            {
                EventID = 1,
                EventStartDate = new DateTime(2021, 01, 05, 12, 0, 0),
                EventEndDate = new DateTime(2021, 01, 25, 16, 0, 0),
                EventName = "Walk-a-thon",
                EventDescription = "Walking for a good cause.",
                EventAddr1 = "222 Magnolia Shaw A St",
                EventCity = "North Augusta",
                EventState = "South Carolina",
                EventPostalCode = 29841
            },
            new Event
            {
                EventID = 2,
                EventStartDate = new DateTime(2022, 03, 01, 13, 0, 0),
                EventEndDate = new DateTime(2022, 03, 01, 19, 0, 0),
                EventName = "Triathon",
                EventDescription = "Biking, Swimming, and Running",
                EventAddr1 = "881 Glenn Rd",
                EventCity = "Clover",
                EventState = "South Carolina",
                EventPostalCode = 29710
            },
            new Event
            {
                EventID = 3,
                EventStartDate = new DateTime(2022, 05, 01, 7, 0, 0),
                EventEndDate = new DateTime(2022, 05, 01, 10, 0, 0),
                EventName = "Movie Night",
                EventDescription = "Finding Nemo with yummy snacks and any drink of choice. Cost of entry is $5 for movie and snacks!",
                EventAddr1 = "739 Gaillard Rd",
                EventCity = "Moncks Corner",
                EventState = "South Carolina",
                EventPostalCode = 29461
            });

            //sets relationship between employees and Committee Memebers
            builder.Entity<Employees>()
                .HasOne(e => e.CommitteeMembers)
                .WithOne(cm => cm.employee)
                .HasForeignKey<CommitteeMembers>(cm => cm.EmpID)
                .HasPrincipalKey<Employees>(e => e.EmpID);
        }

        //creates admin user on startup if a user is doesnt exist
        public static async Task CreateAdminUser(IServiceProvider serviceProvider, IConfiguration _configuration )
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            AdminAccountInformation _settings = _configuration.GetSection("AdminAccountInformation").Get<AdminAccountInformation>();
            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            // if username doesn't exist, create it and add to role -- Creates Beau's account --
            if (await userManager.FindByNameAsync(_settings.Teacher.Username) == null)
            {
                User user = new User {
                    UserName = _settings.Teacher.Username,
                    Email = _settings.Teacher.Email,
                    UserFirstName = "Beau",
                    UserLastName = "Sanders",
                    UserAddr1 = "506 S Pleasantburg Dr",
                    UserCity = "Greenville",
                    UserState = "South Carolina",
                    UserPostalCode = 29607,
                    UserGender = "Male",
                    UserBirthDate = new DateTime(2000, 1, 1),
                    UserActive = true,
                    ReceiveWeeklyNewsletter = false
                };
                var result = await userManager.CreateAsync(user, _settings.Teacher.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("NonProfitContext")))
                    {
                        var account = await userManager.FindByNameAsync("BeauSanders");
                        sqlConnection.Open();
                        SqlCommand sqlcmd = new SqlCommand(String.Format("INSERT INTO Employees(EmpID,UserID, Position, Salary, HireDate,ReleaseDate,FinishedAccountSetup) VALUES('ZaPGaxy4Q-HSL9-LS0d-3Sjh-AIwyn3Bss2SL','{0}','IT Administrator', 123000.00, '{1}', Null, 1)", account.Id, DateTime.UtcNow), sqlConnection);
                        sqlcmd.ExecuteNonQuery();
                        await userManager.AddToRoleAsync(account, "Employee");
                    }
                }
            }
            if(await userManager.FindByNameAsync(_settings.Admin.Username) == null)
            {
                User user = new User
                {
                    UserName = _settings.Admin.Username,
                    Email = _settings.Admin.Email,
                    UserFirstName = "BankdTech",
                    UserLastName = "Solutions",
                    UserAddr1 = "506 S Pleasantburg Dr",
                    UserCity = "Greenville",
                    UserState = "South Carolina",
                    UserPostalCode = 29607,
                    UserGender = "Male",
                    UserBirthDate = new DateTime(2000, 1, 1),
                    UserActive = true,
                    ReceiveWeeklyNewsletter = false
                };
                var result = await userManager.CreateAsync(user, _settings.Admin.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            CreateRoles(serviceProvider);
            
        }
        //creates roles if they do not exist within the database
        public static async void CreateRoles(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if(await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (await roleManager.FindByNameAsync("Member") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Member"));
            }

            if (await roleManager.FindByNameAsync("Employee") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Employee"));
            }
            CreateDeafultUsers(serviceProvider);
        }
        //creates some default users in the database on startup if they do not exist
        public static async void CreateDeafultUsers(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            if (await userManager.FindByNameAsync("KarenSmith") == null)
            {
                User user = new User
                {
                    Id = "5b909dae-4d9d-4ae3-8621-6961f2ffe598",
                    UserName = "KarenSmith",
                    Email = "KarenSmith@gmail.com",
                    UserFirstName = "Karen",
                    UserLastName = "Smith",
                    UserAddr1 = "63 Long Dr",
                    UserCity = "Greenville",
                    UserState = "South Carolina",
                    UserPostalCode = 29607,
                    UserGender = "Female",
                    UserBirthDate = new DateTime(1990, 9, 24),
                    UserActive = true,
                    ReceiveWeeklyNewsletter = false
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Member");
                }
            }
            if (await userManager.FindByNameAsync("One-TimeDonation") == null)
            {
                User user = new User
                {
                    UserName = "One-TimeDonation",
                    Email = "N/A",
                    UserFirstName = "N/A",
                    UserLastName = "N/A",
                    UserAddr1 = "N/A",
                    UserCity = "N/A",
                    UserState = "N/A",
                    UserGender = "N/A",
                    UserActive = true,
                    ReceiveWeeklyNewsletter = false
                };
                //Never need access to this account because its used to store the One-Time Donations that are made from users that are not registered/signedin so I made the password as obscure as possible
                await userManager.CreateAsync(user);
            }
        }
    }
}
