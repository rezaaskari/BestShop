using BestShop.ProductService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

using System;
using System.Linq;
using AngleSharp.Dom;

namespace BestShop.ProductService.Infrastructure.Persistence.Contexts;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
    {
        return Set<TEntity>();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Use Reflection to find all entity types
        var entityTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Entity)));

        foreach (var entityType in entityTypes)
        {
            // Define DbSet dynamically
            modelBuilder.Entity(entityType);
        }
        
       
    }



    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    //Set Schema name
    //    builder.HasDefaultSchema("BASE");

    //    builder.Entity<Category>().Property<DateTime>("UpdateAt");
    //    builder.Entity<Tag>().Property<DateTime>("UpdateAt");
    //    builder.Entity<User>().Property<DateTime>("UpdateAt");

    //    //Atomate Rgeistartion configration
    //    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    //    base.OnModelCreating(builder);
    //}

    public override int SaveChanges()
    {
        //Some change on my entities

        return base.SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //data source=k2.liara.cloud,33504;Database=shopDb;User ID=sa;Password=hfa4HxYHKfFvrf5aAuj8OKAx;encrypt=false;Trust Server Certificate=true;
        //optionsBuilder.UseSqlServer(option => option.UseInMemoryDatabase(databaseName: "ProductDb"));
    }

    //public void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    //data source=k2.liara.cloud,33504;Database=shopDb;User ID=sa;Password=hfa4HxYHKfFvrf5aAuj8OKAx;encrypt=false;Trust Server Certificate=true;
    //    //optionsBuilder.UseSqlServer(option => option.UseInMemoryDatabase(databaseName: "ProductDb"));
    //}

}
