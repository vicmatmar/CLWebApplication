using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CLWebApp.Models
{
    public partial class ManufacturingStore_Context : DbContext
    {
        public ManufacturingStore_Context()
        {
        }

        public ManufacturingStore_Context(DbContextOptions<ManufacturingStore_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<JiliaHubs> JiliaHubs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //    optionsBuilder.UseSqlServer("Server=rs01;Database=ManufacturingStore_RAD2;Trusted_Connection=True;");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JiliaHubs>(entity =>
            {
                entity.Property(e => e.Activation)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.Bid)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Mac)
                    .IsRequired()
                    .HasMaxLength(17);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(36);
            });
        }
    }
}
