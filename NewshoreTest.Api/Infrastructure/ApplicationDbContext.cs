using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NewshoreTest.Api.Domain.Entities;

namespace NewshoreTest.Api.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Aircraft> Aircraft => Set<Aircraft>();
        public DbSet<Carrier> Carrier => Set<Carrier>();
        public DbSet<Flight> Flight => Set<Flight>();
        public DbSet<Journey> Journey => Set<Journey>();
        public DbSet<Transport> Transport => Set<Transport>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
