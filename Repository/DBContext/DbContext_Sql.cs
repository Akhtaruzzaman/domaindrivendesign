using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository.DBContext
{
    public class DbContext_Sql : DbContext
    {
        public DbContext_Sql() : base(GetOptions())
        {

        }
        private static DbContextOptions GetOptions()
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), SYS_DATA.ConnectionString).Options;
        }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasIndex(u => u.UserId).IsUnique();
        }
    }
}
