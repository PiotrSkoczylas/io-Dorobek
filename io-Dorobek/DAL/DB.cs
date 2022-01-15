using io_Dorobek.DAL.Encje;
using Microsoft.EntityFrameworkCore;

namespace io_Dorobek.DAL
{
    class DB : DbContext
    {
        public DbSet<Publication> Publications { get; set; }
        public DB(DbContextOptions<DB> opt) : base(opt)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            //Database.Migrate();
            //SaveChanges();
        }
        public DB() : base()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            //Database.Migrate();
            SaveChanges();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlServer($"Server=localhost;Database=Dorobek;Trusted_Connection=True;");//$"Data Source=baza.db");
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=baza.db");
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Publication>()
                .HasKey(x => x.Id);
            builder.Entity<Publication>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(builder);
        }
    }
}
