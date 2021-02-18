using Microsoft.EntityFrameworkCore;
using PaymentProcessor.Domain.Enitities;
using System;

namespace PaymentProcessor.Migrations
{
    public class AppDbContext: DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
