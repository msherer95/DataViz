using System;
using Microsoft.EntityFrameworkCore;
using DataViz.Contracts;

namespace DataViz.Db
{
    // Creates EF's connection to the Db
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=test;Username=admin;Password=admin");
        }
    }
}
