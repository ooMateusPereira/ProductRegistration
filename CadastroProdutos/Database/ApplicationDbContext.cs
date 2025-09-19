using System;
using Microsoft.EntityFrameworkCore;

namespace ProductRegistration.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
