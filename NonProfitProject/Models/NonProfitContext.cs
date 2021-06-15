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
        public DbSet<DonationRecipts> DonationRecipts { get; set; }
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
            builder.Entity<User>()
                .HasMany(u => u.payments)
                .WithOne(u => u.user)
                .HasForeignKey(p => p.UserID);
            builder.Entity<User>()
                .HasMany(u => u.donationRecipts)
                .WithOne(u => u.user)
                .HasForeignKey(DonationRecipts => DonationRecipts.UserID);
            //sets default value for new users to the current date
            builder.Entity<User>()
                .Property(u => u.UserCreationDate)
                .HasDefaultValueSql("getDate()");
            builder.Entity<User>()
                .Property(u => u.UserLastActivity)
                .HasDefaultValueSql("getDate()");

            builder.Entity<DonationRecipts>()
                .HasOne(dr => dr.membershipDues)
                .WithOne(x => x.donationRecipts)
                .HasForeignKey<DonationRecipts>(dr => dr.ReciptDonationID)
                .HasPrincipalKey<MembershipDues>(x => x.MemDuesID);
            builder.Entity<DonationRecipts>()
                .HasOne(dr => dr.donations)
                .WithOne(x => x.donationRecipts)
                .HasForeignKey<DonationRecipts>(dr => dr.ReciptDonationID)
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
                    //UserCreationDate = DateTime.Now,
                    UserActive = 'A'
                };
                var result = await userManager.CreateAsync(user, "teamProjeck275");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
