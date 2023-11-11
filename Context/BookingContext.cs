
using Microsoft.EntityFrameworkCore;
using Booking.API.Models;
using System.Linq;

namespace Booking.API.Context
{
    public partial class BookingContext : DbContext
    {
        public BookingContext()
        {
        }

        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {
        }
        
         public virtual DbSet<Customer> Customer { get; set; }
         public virtual DbSet<Booking> Booking {get;set;}
         public virtual DbSet<CountryType> CountryType {get;set;}
         public virtual DbSet<Class> Class {get;set;}
         public virtual DbSet<Customer> Customer {get;set;}
         public virtual DbSet<Packages> Packages {get;set;}
         public virtual DbSet<WaitList> WaitList {get;set;}

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }                

           
            modelBuilder.Entity<Customer>()
            .HasKey(k => new { k.CustomerId});
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
