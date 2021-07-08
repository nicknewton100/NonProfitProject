using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NonProfitProject.Models
{
    public class NonProfitContext : IdentityDbContext<User>
    {
        public NonProfitContext(DbContextOptions<NonProfitContext> options) : base(options) { }

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
                    UserCountry = "United States Of America",
                    UserPostalCode = 29607,
                    UserGender = "Male",
                    UserBirthDate = new DateTime(1987, 6, 13),
                    UserActive = true,
                    ReceiveWeeklyNewsletter = false,
                    PasswordHash = hasher.HashPassword(null, "JohnJones123")
                });
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                    UserId = "6b87b89f-0f9a-4e2d-b696-235e99655521"
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
                }
                );
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
                EventStartDate = new DateTime(2021, 01, 05, 0, 0, 0),
                EventEndDate = new DateTime(2021, 01, 25, 0, 0, 0),
                EventName = "Walk-a-thon",
                EventDescription = "Walking for a good cause."
            },
            new Event
            {
                EventID = 2,
                EventStartDate = new DateTime(2022, 03, 01, 0, 0, 0),
                EventEndDate = new DateTime(2022, 03, 30, 0, 0, 0),
                EventName = "Triathon",
                EventDescription = "Biking, Swimming, and Running"
            },
            new Event
            {
                EventID = 3,
                EventStartDate = new DateTime(2022, 05, 01, 0, 0, 0),
                EventEndDate = new DateTime(2022, 05, 05, 0, 0, 0),
                EventName = "Movie Night",
                EventDescription = "Finding Nemo with yummy snacks and any drink of choice. Cost of entry is $5 for movie and snacks!"
            }
            );
            //Sets relationships for User
            /*builder.Entity<User>()
                .HasMany(u => u.payments)
                .WithOne(u => u.user)
                .HasForeignKey(p => p.UserID);
            builder.Entity<User>()
                .HasMany(u => u.donationReceipts)
                .WithOne(u => u.user)
                .HasForeignKey(DonationRecipts => DonationRecipts.UserID);*/
            //sets default value for new users to the current date
            builder.Entity<User>()
                .Property(u => u.UserCreationDate)
                .HasDefaultValueSql("getUTCDate()");
            builder.Entity<User>()
                .Property(u => u.UserLastActivity)
                .HasDefaultValueSql("getUTCDate()");
            //sets default value for weekly newsletter
            builder.Entity<User>()
                .Property(u => u.ReceiveWeeklyNewsletter)
                .HasDefaultValue(false);
            //set default value for AccountDisabled
            builder.Entity<User>()
                .Property(u => u.AccountDisabled)
                .HasDefaultValue(false);
            builder.Entity<Employees>()
                .Property(e => e.HireDate)
                .HasDefaultValueSql("getUTCDate()");

            //sets relationship between employees and Committee Memebers
            builder.Entity<Employees>()
                .HasOne(e => e.CommitteeMembers)
                .WithOne(cm => cm.employee)
                .HasForeignKey<CommitteeMembers>(cm => cm.EmpID)
                .HasPrincipalKey<Employees>(e => e.EmpID);
            //need to figure out how to create a DonationID with prefix so that you can distiguish between DontationID and MemduesID
            /*builder.Entity<Donations>()
                .Property(d => d.DonationID)
                .HasComputedColumnSql("'D-' + RIGHT('00000' +CAST(DonationID AS VARCHAR(10)), 10)" );*/
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // if username doesn't exist, create it and add to role -- Creates Beau's account --
            if (await userManager.FindByNameAsync("BeauSanders") == null)
            {
                User user = new User {
                    UserName = "BeauSanders",
                    Email = "admin@cpt275.beausanders.org",
                    UserFirstName = "Beau",
                    UserLastName = "Sanders",
                    UserAddr1 = "506 S Pleasantburg Dr",
                    UserCity = "Greenville",
                    UserState = "South Carolina",
                    UserCountry = "United States Of America",
                    UserPostalCode = 29607,
                    UserGender = "Male",
                    UserBirthDate = new DateTime(2000, 1, 1),
                    UserActive = true,
                    ReceiveWeeklyNewsletter = false
                };
                var result = await userManager.CreateAsync(user, "teamProjeck275");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            if(await userManager.FindByNameAsync("BankdTechSolutionsAdmin") == null)
            {
                User user = new User
                {
                    UserName = "BankdTechSolutionsAdmin",
                    Email = "BankdTechSolutions@gmail.com",
                    UserFirstName = "BankdTech",
                    UserLastName = "Solutions",
                    UserAddr1 = "506 S Pleasantburg Dr",
                    UserCity = "Greenville",
                    UserState = "South Carolina",
                    UserCountry = "United States Of America",
                    UserPostalCode = 29607,
                    UserGender = "Male",
                    UserBirthDate = new DateTime(2000, 1, 1),
                    UserActive = true,
                    ReceiveWeeklyNewsletter = false
                };
                var result = await userManager.CreateAsync(user, "987963Gizm0");
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
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
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
        public static async void CreateDeafultUsers(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

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
                    UserCountry = "United States Of America",
                    UserPostalCode = 29607,
                    UserGender = "Female",
                    UserBirthDate = new DateTime(1990, 9, 24),
                    UserActive = true,
                    ReceiveWeeklyNewsletter = false
                };
                var result = await userManager.CreateAsync(user, "KarenSmith123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Member");
                }
            }
        }
    }
}
