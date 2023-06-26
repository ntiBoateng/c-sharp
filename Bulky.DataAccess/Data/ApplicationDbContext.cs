using Bulky.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
	public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
             new Category { Id = 1, Name = "Action", DisplayOrder = 3 },
            new Category { Id = 2, Name = "Sci-Fi", DisplayOrder = 2 },
            new Category { Id = 3, Name = "Drama", DisplayOrder = 5 },
            new Category { Id = 4, Name = "Crime", DisplayOrder = 1 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id=1, Title="All that Glitters", Description="a book written by Oknko Chibodzor", ISBN="SNG234JUT34", AuthurName="Gideon", ListPrice=45, Price=34, Price100=56, Price50=89, CategoryId=2, ImageUrl="" },
                new Product { Id=2, Title="Amen to all Prayers", Description="a book written by Oknko Chibodzor", ISBN="HG985652LN", AuthurName = "Gideon", ListPrice =45, Price=34, Price100=56, Price50=89, CategoryId = 2, ImageUrl = "" },
                new Product { Id=3, Title="A charge to keep I have", Description="There is a land that is farer than the day!", ISBN="JI87541256DS", AuthurName = "Gideon", ListPrice =45, Price=34, Price100=56, Price50=89, CategoryId = 2, ImageUrl = "" },
                new Product { Id=4, Title="Redeemer Lives", Description="With God, all things are possible", ISBN="BN568452SD", AuthurName = "Gideon", ListPrice =45, Price=34, Price100=56, Price50=89, CategoryId = 2, ImageUrl = "" },
                new Product { Id=5, Title="All Oddss", Description="a book written by Oknko Chibodzor", ISBN="MN7585212SW", AuthurName = "Gideon", ListPrice = 45, Price=34, Price100=56, Price50=89, CategoryId = 2, ImageUrl = "" }
                );
        } 
    }
}
