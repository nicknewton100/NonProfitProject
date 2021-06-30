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

        public DbSet<PaymentInformation> PaymentInformation { get; set; }
        public DbSet<DonationReceipts> DonationReceipts { get; set; }
        public DbSet<MembershipDues> MembershipDues { get; set; }
        public DbSet<Donations> Donations { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<CommitteeMembers> CommitteeMembers { get; set; }
        public DbSet<Committees> Committees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
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
                EventDescription = "Fun event coming soon!"
            },
            new Event
            {
                EventID = 3,
                EventStartDate = new DateTime(2022, 05, 01, 0, 0, 0),
                EventEndDate = new DateTime(2022, 05, 05, 0, 0, 0),
                EventName = "Five days of Help",
                EventDescription = "Fun event coming soon!"
            }
            );
            //Sets relationships for User
            builder.Entity<User>()
                .HasMany(u => u.payments)
                .WithOne(u => u.user)
                .HasForeignKey(p => p.UserID);
            builder.Entity<User>()
                .HasMany(u => u.donationReceipts)
                .WithOne(u => u.user)
                .HasForeignKey(DonationRecipts => DonationRecipts.UserID);
            //sets default value for new users to the current date
            builder.Entity<User>()
                .Property(u => u.UserCreationDate)
                .HasDefaultValueSql("getDate()");
            builder.Entity<User>()
                .Property(u => u.UserLastActivity)
                .HasDefaultValueSql("getDate()");
            //sets default value for weekly newsletter
            builder.Entity<User>()
                .Property(u => u.ReceiveWeeklyNewsletter)
                .HasDefaultValue(false);
            //sets relationships between Donation receipts and Membership dues
            builder.Entity<DonationReceipts>()
                .HasOne(dr => dr.membershipDues)
                .WithOne(x => x.DonationReceipts)
                .HasForeignKey<DonationReceipts>(dr => dr.ReceiptDonationID)
                .HasPrincipalKey<MembershipDues>(x => x.MemDuesID);
            //sets relationship between Donation and dontationreceipts
            builder.Entity<DonationReceipts>()
                .HasOne(dr => dr.donations)
                .WithOne(x => x.donationReceipts)
                .HasForeignKey<DonationReceipts>(dr => dr.ReceiptDonationID)
                .HasPrincipalKey<Donations>(d => d.DonationID);
            builder.Entity<PaymentInformation>()
                .HasMany(dr => dr.donationRecipts)
                .WithOne(pi => pi.paymentInformation)
                .HasForeignKey(DonationRecipts => DonationRecipts.PaymentID);
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
        }
    }
}
