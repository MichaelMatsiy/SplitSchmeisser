using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Text;

namespace SplitSchmeisser.DAL.Context
{
    public class DbConfig : DbConfiguration
    {
        public DbConfig()
        {
            SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
        }
    }

    [DbConfigurationType(typeof(DbConfig))]
    public class SchmeisserContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        public SchmeisserContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Operation>()
                .HasRequired<User>(x => x.Owner)
                .WithMany(u => u.Operations)
                .HasForeignKey<int>(x => x.OwnerId);
        }
    }
}