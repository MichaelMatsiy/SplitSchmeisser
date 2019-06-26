using Microsoft.EntityFrameworkCore;
using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text;

namespace SplitSchmeisser.DAL.Context
{
    public class SchmeisserContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        public SchmeisserContext(DbContextOptions<SchmeisserContext> options) : base(options) { }

    }
}
