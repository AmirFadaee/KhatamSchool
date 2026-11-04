using KhatamSchool_Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace KhatamSchool_Data.Context
{
    public class KhatamSchool_DbContext
        : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public KhatamSchool_DbContext(DbContextOptions<KhatamSchool_DbContext> options) : base(options) { }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ExecutiveStaff> ExecutiveStaffs { get; set; }
        public DbSet<CounselingTicket> CounselingTickets { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<GalleryAlbum> GalleryAlbums { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<AchievementCategory> AchievementCategories { get; set; }
        public DbSet<AchievementItem> AchievementItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Grade>()
                .HasIndex(x => x.Title)
                .IsUnique();


            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PersonnelCode).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.PersonnelCode).IsUnique();
                entity.HasQueryFilter(e => !e.IsDeleted);

                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.SetNull)
                      .IsRequired(false);
            });

            modelBuilder.Entity<ExecutiveStaff>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PersonnelCode).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.PersonnelCode).IsUnique();
                entity.HasQueryFilter(e => !e.IsDeleted);

                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.SetNull)
                      .IsRequired(false);
            });

            modelBuilder.Entity<CounselingTicket>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TrackingCode).IsUnique();
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Slug).IsUnique();
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // AchievementCategory
            modelBuilder.Entity<AchievementCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // AchievementItem
            modelBuilder.Entity<AchievementItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasQueryFilter(e => !e.IsDeleted);
                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Items)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // GalleryAlbum
            modelBuilder.Entity<GalleryAlbum>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // GalleryItem
            modelBuilder.Entity<GalleryItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasQueryFilter(e => !e.IsDeleted);
                entity.HasOne(e => e.GalleryAlbum)
                      .WithMany(a => a.Items)
                      .HasForeignKey(e => e.GalleryAlbumId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
