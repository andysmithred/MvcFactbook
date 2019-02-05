using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Models
{
    public partial class FactbookContext : DbContext
    {
        public FactbookContext() { }

        public FactbookContext(DbContextOptions<FactbookContext> options)
            : base(options) { }

        public virtual DbSet<ArmedForce> ArmedForce { get; set; }
        public virtual DbSet<ArmedForceFlag> ArmedForceFlag { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<BranchFlag> BranchFlag { get; set; }
        public virtual DbSet<BranchType> BranchType { get; set; }
        public virtual DbSet<Builder> Builder { get; set; }
        public virtual DbSet<Flag> Flag { get; set; }
        public virtual DbSet<Ship> Ship { get; set; }
        public virtual DbSet<ShipCategory> ShipCategory { get; set; }
        public virtual DbSet<ShipType> ShipType { get; set; }
        public virtual DbSet<ShipSubType> ShipSubType { get; set; }
        public virtual DbSet<ShipClass> ShipClass { get; set; }
        public virtual DbSet<ShipService> ShipService { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\AndyS\\OneDrive\\Documents\\Visual Studio 2017\\Projects\\MvcFactbook\\MvcFactbook\\Database\\Factbook.mdf; Integrated Security = True;Connect Timeout=30");
                optionsBuilder.UseSqlServer("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\dev\\MvcFactbook\\MvcFactbook\\Database\\MvcFactbook.mdf; Integrated Security = True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Armed Force
            modelBuilder.Entity<ArmedForce>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasDefaultValueSql("((X))");

                entity.Property(e => e.IsActive)
                    .IsRequired();
            });

            // Flag
            modelBuilder.Entity<Flag>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.Active)
                    .IsRequired();
            });

            // Armed Force Flag
            modelBuilder.Entity<ArmedForceFlag>(entity =>
            {
                entity.Property(e => e.ArmedForceId)
                    .IsRequired();

                entity.Property(e => e.FlagId)
                    .IsRequired();

                entity.HasOne(x => x.ArmedForce)
                    .WithMany(y => y.ArmedForceFlags)
                    .HasForeignKey(y => y.ArmedForceId)
                    .HasConstraintName("FK_ArmedForceFlag_To_ArmedForce");

                entity.HasOne(x => x.Flag)
                    .WithMany(f => f.ArmedForceFlags)
                    .HasForeignKey(f => f.FlagId)
                    .HasConstraintName("FK_ArmedForceFlag_To_Flag");
            });

            // Branch
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ArmedForceId)
                    .IsRequired();

                entity.Property(e => e.BranchTypeId)
                    .IsRequired();

                entity.Property(e => e.HasEmblem)
                    .IsRequired();

                entity.HasOne(x => x.ArmedForce)
                    .WithMany(x => x.Branches)
                    .HasForeignKey(x => x.ArmedForceId)
                    .HasConstraintName("FK_Branch_To_ArmedForce");

                entity.HasOne(x => x.BranchType)
                    .WithMany(y => y.Branches)
                    .HasForeignKey(y => y.BranchTypeId)
                    .HasConstraintName("FK_Branch_To_BranchType");
            });

            // Branch Flag
            modelBuilder.Entity<BranchFlag>(entity =>
            {
                entity.Property(e => e.BranchId)
                    .IsRequired();

                entity.Property(e => e.FlagId)
                    .IsRequired();

                entity.HasOne(x => x.Branch)
                    .WithMany(y => y.BranchFlags)
                    .HasForeignKey(y => y.BranchId)
                    .HasConstraintName("FK_BranchFlag_To_Branch");

                entity.HasOne(x => x.Flag)
                    .WithMany(f => f.BranchFlags)
                    .HasForeignKey(f => f.FlagId)
                    .HasConstraintName("FK_BranchFlag_To_Flag");
            });

            // Branch Type
            modelBuilder.Entity<BranchType>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            // Builder
            modelBuilder.Entity<Builder>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired();
            });

            // Ship
            modelBuilder.Entity<Ship>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired();

                entity.HasOne(x => x.Builder)
                    .WithMany(y => y.Ships)
                    .HasForeignKey(y => y.BuilderId)
                    .HasConstraintName("FK_Ship_To_Builder");
            });

            // Ship Category
            modelBuilder.Entity<ShipCategory>(entity =>
            {
                entity.Property(e => e.Category)
                    .IsRequired();
            });

            // Ship Type
            modelBuilder.Entity<ShipType>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(x => x.ShipCategory)
                    .WithMany(x => x.ShipTypes)
                    .HasForeignKey(x => x.ShipCategoryId)
                    .HasConstraintName("FK_ShipType_To_ShipCategory");
            });

            // Ship Sub Type
            modelBuilder.Entity<ShipSubType>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired();

                entity.HasOne(x => x.ShipType)
                    .WithMany(x => x.ShipSubTypes)
                    .HasForeignKey(x => x.ShipTypeId)
                    .HasConstraintName("FK_ShipSubType_To_ShipType");
            });

            // Ship Class
            modelBuilder.Entity<ShipClass>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired();
            });

            // Ship Service
            modelBuilder.Entity<ShipService>(entity =>
            {
                entity.Property(e => e.Penant)
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.HasOne(x => x.Ship)
                    .WithMany(x => x.ShipServices)
                    .HasForeignKey(x => x.ShipId)
                    .HasConstraintName("FK_ShipService_To_Ship");

                entity.HasOne(x => x.ShipClass)
                    .WithMany(x => x.ShipServices)
                    .HasForeignKey(x => x.ShipClassId)
                    .HasConstraintName("FK_ShipService_To_ShipClass");

                entity.HasOne(x => x.ShipSubType)
                    .WithMany(x => x.ShipServices)
                    .HasForeignKey(x => x.ShipSubTypeId)
                    .HasConstraintName("FK_ShipService_To_ShipSubType");

                entity.HasOne(x => x.Branch)
                    .WithMany(x => x.ShipServices)
                    .HasForeignKey(x => x.BranchId)
                    .HasConstraintName("FK_ShipService_To_Branch");

                entity.Property(x => x.Active)
                    .IsRequired();
            });
        }
    }
}