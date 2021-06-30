using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Event>().HasData(
            new Event
            {
                EventID = 1,
                EventStartDate = DateTime.FromOADate(01/05/2021),
                EventEndDate = DateTime.FromOADate(01/25/2021),
                EventName = "Walk-a-thon",
                EventDescription = "Walking for a good cause."
            },
            new Event
            {
                EventID = 2,
                EventStartDate = DateTime.FromOADate(03/01/2022),
                EventEndDate = DateTime.FromOADate(03/30/2022),
                EventName = "Triathon",
                EventDescription = "Fun event coming soon!"
            },
            new Event
            {
                EventID = 3,
                EventStartDate = DateTime.FromOADate(05/01/2022),
                EventEndDate = DateTime.FromOADate(05/05/2022),
                EventName = "Five days of Help",
                EventDescription = "Fun event coming soon!"
            }
            );
        }
    }
}