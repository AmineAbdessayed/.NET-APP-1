using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Data
{
    public class ApplicationDBContext: IdentityDbContext<AppUser>  //kenet DbContext kbal mabdit fel user
    {
        public ApplicationDBContext(DbContextOptions options)
        :base(options)
        {
            
        }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Portfolio> portfolios{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x=>x.HasKey(p=> new {p.AppUserId,p.StockId}));

            builder.Entity<Portfolio>()
                .HasOne(p=>p.AppUser)
                .WithMany(p=>p.portfolios)
                .HasForeignKey(p=>p.AppUserId);

              builder.Entity<Portfolio>()
                .HasOne(u=>u.Stock)
                .WithMany(u=>u.portfolios)
                .HasForeignKey(p=>p.StockId);




            List<IdentityRole> roles=new List<IdentityRole>{
                new IdentityRole 
                {
                    Name="Admin",
                    NormalizedName= "ADMIN"
                },
                   new IdentityRole 
                {
                    Name="User",
                    NormalizedName= "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.ConfigureWarnings(warnings =>
        warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
}
        
    }
}