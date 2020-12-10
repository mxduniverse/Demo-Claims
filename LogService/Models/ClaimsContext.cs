using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LogService.Models
{
    public partial class ClaimsContext : DbContext
    {


        public ClaimsContext(DbContextOptions<ClaimsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Logs> Logs { get; set; }


 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.HasKey(e => e.LogId);


                entity.Property(e => e.ActionPerformed)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");


         

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });





            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
