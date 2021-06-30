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
                EventStartDate = ,
                EventEndDate = (),
                EventDescription = ""
            },
            new Event
            {
                EventID = 2,
                EventStartDate = ,
                EventEndDate = (),
                EventDescription = ""
            },
            new Event
            {
                EventID = 3,
                EventStartDate = ,
                EventEndDate = ,
                EventDescription = ""
            }
            );
        }
    }
}