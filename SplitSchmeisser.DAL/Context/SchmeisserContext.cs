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

        public SchmeisserContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseLazyLoadingProxies();
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserGroup>()
        //        .HasKey(x => new { x.UserId, x.GroupId });

        //    modelBuilder.Entity<UserGroup>()
        //        .HasOne(x => x.User)
        //        .WithMany(x => x.UserGroups)
        //        .HasForeignKey(x => x.UserId);

        //    modelBuilder.Entity<UserGroup>()
        //        .HasOne(x => x.Group)
        //        .WithMany(x => x.UserGroups)
        //        .HasForeignKey(x => x.GroupId);

        //    base.OnModelCreating(modelBuilder);
        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            //modelBuilder.HasDefaultSchema("Admin");

            modelBuilder.Entity<User>().HasKey(x => x.Id);

            //modelBuilder.Entity<UseGrur>
        }
    }
}
