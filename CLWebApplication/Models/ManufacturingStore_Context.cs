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

        public virtual DbSet<Board> Board { get; set; }
        public virtual DbSet<BoardRevision> BoardRevision { get; set; }
        public virtual DbSet<ChipType> ChipType { get; set; }
        public virtual DbSet<Eeprom> Eeprom { get; set; }
        public virtual DbSet<EuiList> EuiList { get; set; }
        public virtual DbSet<Firmware> Firmware { get; set; }
        public virtual DbSet<ImageType> ImageType { get; set; }
        public virtual DbSet<InsightAdapter> InsightAdapter { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductionSite> ProductionSite { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<SerialNumber> SerialNumber { get; set; }
        public virtual DbSet<TargetDevice> TargetDevice { get; set; }
        public virtual DbSet<Tester> Tester { get; set; }
        public virtual DbSet<TestSession> TestSession { get; set; }
        public virtual DbSet<UserSession> UserSession { get; set; }


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

            modelBuilder.Entity<Board>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_Board")
                    .IsUnique();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ChipType)
                    .WithMany(p => p.Board)
                    .HasForeignKey(d => d.ChipTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Board_ChipType");
            });

            modelBuilder.Entity<BoardRevision>(entity =>
            {
                entity.HasIndex(e => new { e.BoardId, e.Revision })
                    .HasName("IX_BoardRevsion")
                    .IsUnique();

                entity.HasIndex(e => new { e.Revision, e.BoardId, e.Bomrevision })
                    .HasName("uc_BomRevision")
                    .IsUnique();

                entity.Property(e => e.AltRx).HasColumnName("ALT_RX");

                entity.Property(e => e.AltTx).HasColumnName("ALT_TX");

                entity.Property(e => e.Bomrevision)
                    .HasColumnName("BOMRevision")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BoostTxpower).HasColumnName("BoostTXPower");

                entity.Property(e => e.Eepromid).HasColumnName("EEPROMId");

                entity.Property(e => e.ExternalPaalt).HasColumnName("ExternalPAAlt");

                entity.Property(e => e.ExternalPabidirectional).HasColumnName("ExternalPABidirectional");

                entity.Property(e => e.HasPa)
                    .IsRequired()
                    .HasColumnName("HasPA")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MfgTokens)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Port).HasMaxLength(1);

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.BoardRevision)
                    .HasForeignKey(d => d.BoardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BoardRevsion_Board");

                entity.HasOne(d => d.Eeprom)
                    .WithMany(p => p.BoardRevision)
                    .HasForeignKey(d => d.Eepromid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BoardRevision_EEPROM");
            });

            modelBuilder.Entity<ChipType>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.Manufacturer })
                    .HasName("IX_ChipType_UniqueNameMfg")
                    .IsUnique();

                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Eeprom>(entity =>
            {
                entity.ToTable("EEPROM");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_EEPROM")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EuiList>(entity =>
            {
                entity.HasIndex(e => e.Eui)
                    .HasName("IX_EuiList")
                    .IsUnique();

                entity.Property(e => e.Eui)
                    .IsRequired()
                    .HasColumnName("EUI")
                    .HasMaxLength(16);

                entity.Property(e => e.VendorEui).HasMaxLength(16);

                entity.HasOne(d => d.ProductionSite)
                    .WithMany(p => p.EuiList)
                    .HasForeignKey(d => d.ProductionSiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EuiList_ProductionSite");
            });

            modelBuilder.Entity<Firmware>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.FileVersion })
                    .HasName("IX_Firmware_UniqueNameVersion")
                    .IsUnique();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ChipType)
                    .WithMany(p => p.Firmware)
                    .HasForeignKey(d => d.ChipTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Firmware_ChipTypes");

                entity.HasOne(d => d.ImageType)
                    .WithMany(p => p.Firmware)
                    .HasForeignKey(d => d.ImageTypeId)
                    .HasConstraintName("FK_Firmware_ImageTypes");
            });

            modelBuilder.Entity<ImageType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_ImageTypes")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<InsightAdapter>(entity =>
            {
                entity.HasIndex(e => e.IpAddress)
                    .HasName("IX_InsightAdapter_1")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("IX_InsightAdapter")
                    .IsUnique();

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_Product_Name")
                    .IsUnique();

                entity.Property(e => e.BoxLabelName).HasDefaultValueSql("('')");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ModelEncodingNumber).HasDefaultValueSql("((-1))");

                entity.Property(e => e.ModelString)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Released).HasDefaultValueSql("((0))");

                entity.Property(e => e.SerialNumberCode)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.Sku)
                    .IsRequired()
                    .HasColumnName("SKU")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ZigbeeModelString)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ModelString')");

                entity.Property(e => e.ZplFile).HasMaxLength(255);

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.BoardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Board");
            });

            modelBuilder.Entity<ProductionSite>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_ProductionSite")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RunIct)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasIndex(e => e.Text)
                    .HasName("UQ_Result_Text")
                    .IsUnique();

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SerialNumber>(entity =>
            {
                entity.HasIndex(e => new { e.ProductId, e.SerialNumber1 })
                    .HasName("IX_SerialNumber")
                    .IsUnique();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SerialNumber1).HasColumnName("SerialNumber");

                entity.Property(e => e.TesterId).HasDefaultValueSql("((5))");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Eui)
                    .WithMany(p => p.SerialNumber)
                    .HasForeignKey(d => d.EuiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SerialNumber_EuiList");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SerialNumber)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SerialNumber_Product");

                entity.HasOne(d => d.Tester)
                    .WithMany(p => p.SerialNumber)
                    .HasForeignKey(d => d.TesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SerialNumber_Tester");
            });

            modelBuilder.Entity<TargetDevice>(entity =>
            {
                entity.HasIndex(e => new { e.IsaId, e.TestSessionId, e.EuiId, e.Id })
                    .HasName("_dta_index_TargetDevice_29_167671645__K2_K4_K3_K1");

                entity.HasOne(d => d.Eui)
                    .WithMany(p => p.TargetDevice)
                    .HasForeignKey(d => d.EuiId)
                    .HasConstraintName("FK_TargetDevice_EuiList");

                entity.HasOne(d => d.Isa)
                    .WithMany(p => p.TargetDevice)
                    .HasForeignKey(d => d.IsaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TargetDevice_InsightAdapter");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.TargetDevice)
                    .HasForeignKey(d => d.ResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TargetDevice_Result");

                entity.HasOne(d => d.TestSession)
                    .WithMany(p => p.TargetDevice)
                    .HasForeignKey(d => d.TestSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TargetDevice_TestSession");
            });

            modelBuilder.Entity<Tester>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<TestSession>(entity =>
            {
                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.BoardRevision)
                    .WithMany(p => p.TestSession)
                    .HasForeignKey(d => d.BoardRevisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestSession_BoardRevision");

                entity.HasOne(d => d.Firmware)
                    .WithMany(p => p.TestSession)
                    .HasForeignKey(d => d.FirmwareId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestSession_Firmware");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TestSession)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestSession_Product");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.TestSession)
                    .HasForeignKey(d => d.ResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestSession_Result");

                entity.HasOne(d => d.UserSession)
                    .WithMany(p => p.TestSession)
                    .HasForeignKey(d => d.UserSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestSession_UserSession");
            });

            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Unknown')");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Tester)
                    .WithMany(p => p.UserSession)
                    .HasForeignKey(d => d.TesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_Tester");
            });
        }


    }
}
