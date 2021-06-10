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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
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
            if (await userManager.FindByNameAsync("admin@cpt275.beausanders.org") == null)
            {
                User user = new User { 
                    UserName = "admin@cpt275.beausanders.org",
                    Email = "admin@cpt275.beausanders.org",
                    UserFirstName = "Beau",
                    UserLastName = "Sanders",
                    UserAddr1 = "506 S Pleasantburg Dr",
                    UserCity = "Greenville",
                    UserState = "South Carolina",
                    UserCountry = "United States Of America",
                    UserPostalCode = 29607,
                    UserCreationDate = DateTime.Now
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
