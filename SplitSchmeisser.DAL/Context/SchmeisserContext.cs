using Microsoft.EntityFrameworkCore;
using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitSchmeisser.DAL.Context
{
    public class SchmeisserContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        public SchmeisserContext(DbContextOptions<SchmeisserContext> options) : base(options) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>()
                .HasKey(x => new { x.UserId, x.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(x => x.Group)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.GroupId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
