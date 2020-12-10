using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IdentityService.Models
{
    public partial class ClaimsContext : DbContext
    {


        public ClaimsContext(DbContextOptions<ClaimsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Claims> Claims { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claims>(entity =>
            {
                entity.HasKey(e => e.ClaimId);

                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.DamagedItem)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Incidence)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });


            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Roles>().HasData(new Roles
            {
                Name = "Agent",
                RoleId = 1,
            }, new Roles
            {
                Name = "Client",
                RoleId = 2,

            });

            modelBuilder.Entity<UserInfo>().HasData(new UserInfo
            {
                Name = "landry",
                Surname = "Mukonde",
                Login = "landry",
                Password = "password",
                RoleId = 1,
                UserId = 1
            }, new UserInfo
            {
                Name = "elisa",
                Surname = "Mukonde",
                Login = "elisa",
                Password = "password",
                RoleId = 2,
                UserId = 2
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
