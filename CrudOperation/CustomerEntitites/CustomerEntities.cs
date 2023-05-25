

using CustomerEntities.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerEntities
{
    public class CustomerEntities : DbContext
    {


        public CustomerEntities(DbContextOptions<CustomerEntities> options)
                    : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustAddress> CustAddress { get; set; }
        public virtual DbSet<CustPassword> CustPassword { get; set; }

    }
}
