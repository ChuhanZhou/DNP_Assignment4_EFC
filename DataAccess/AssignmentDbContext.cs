using System.Threading.Tasks;
using DNP_Assignment4_EFC.Models.DbUnit;
using DNP_Assignment4_EFC.Models.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DNP_Assignment4_EFC.DataAccess
{
    public class AssignmentDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DbFamily> Families { get; set; }
        public DbSet<Adult> Adults { get; set; }
        public DbSet<DbChild> Children { get; set; }
        public DbSet<Interest> Interests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source = Assignment4.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            
            //DbAdultFamily
            modelBuilder.Entity<DbAdultFamily>()
                .HasKey(af => new
                {
                    af.Address,
                    af.Id
                });
            modelBuilder.Entity<DbAdultFamily>()
                .HasOne(af => af.Family)
                .WithMany(family => family.AdultFamily)
                .HasForeignKey(af => af.Address);
            modelBuilder.Entity<DbAdultFamily>()
                .HasOne(af => af.Adult)
                .WithMany(adult => adult.AdultFamilies)
                .HasForeignKey(af => af.Id);
            
            //DbChildFamily
            modelBuilder.Entity<DbChildFamily>()
                .HasKey(af => new
                {
                    af.Address,
                    af.Id
                });
            modelBuilder.Entity<DbChildFamily>()
                .HasOne(cf => cf.Family)
                .WithMany(family => family.ChildFamily)
                .HasForeignKey(cf => cf.Address);
            modelBuilder.Entity<DbChildFamily>()
                .HasOne(cf => cf.Child)
                .WithMany(child => child.ChildFamily)
                .HasForeignKey(cf => cf.Id);
            
            //DbChildInterest
            modelBuilder.Entity<DbChildInterest>()
                .HasKey(ci => new
                {
                    ci.Id,
                    ci.Type
                });
            modelBuilder.Entity<DbChildInterest>()
                .HasOne(ci => ci.Child)
                .WithMany(child => child.ChildInterest)
                .HasForeignKey(ci => ci.Id);
            modelBuilder.Entity<DbChildInterest>()
                .HasOne(ci => ci.Interest)
                .WithMany(interest => interest.ChildInterest)
                .HasForeignKey(ci => ci.Type);
        }
    }
}