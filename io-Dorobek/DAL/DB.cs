using io_Dorobek.DAL.Encje;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io_Dorobek.DAL
{
    class DB : DbContext
    {
        public DbSet<Publication> Publications { get; set; }
        public DB(DbContextOptions<DB> opt) : base(opt)
        {
            Database.EnsureCreated();
            //Database.Migrate();
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            SaveChanges();
        }
        public DB() : base() { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=baza.db");
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Publication>()
                .HasKey(x => x.Id);

            base.OnModelCreating(builder);
        }
    }
}
