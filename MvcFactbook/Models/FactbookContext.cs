using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<Dockyard> Dockyard { get; set; }
        public virtual DbSet<DockyardHistory> DockyardHistory { get; set; }
        public virtual DbSet<Flag> Flag { get; set; }
        public virtual DbSet<PoliticalEntity> PoliticalEntity { get; set; }
        public virtual DbSet<PoliticalEntityDockyard> PoliticalEntityDockyard { get; set; }
        public virtual DbSet<PoliticalEntityEra> PoliticalEntityEra { get; set; }
        public virtual DbSet<PoliticalEntityFlag> PoliticalEntityFlag { get; set; }
        public virtual DbSet<PoliticalEntityType> PoliticalEntityType { get; set; }
        public virtual DbSet<PoliticalEntitySucceeding> PoliticalEntitySucceeding { get; set; }
        public virtual DbSet<Ship> Ship { get; set; }
        public virtual DbSet<Shipbuilder> Shipbuilder { get; set; }
        public virtual DbSet<ShipCategory> ShipCategory { get; set; }
        public virtual DbSet<ShipType> ShipType { get; set; }
        public virtual DbSet<ShipSubType> ShipSubType { get; set; }
        public virtual DbSet<ShipClass> ShipClass { get; set; }
        public virtual DbSet<ShipGroup> ShipGroup { get; set; }
        public virtual DbSet<ShipGroupSet> ShipGroupSet { get; set; }
        public virtual DbSet<ShipService> ShipService { get; set; }
        public virtual DbSet<SucceedingClass> SucceedingClass { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\asmith\\source\\repos\\MvcFactbook\\MvcFactbook\\Database\\MvcFactbook.mdf; Integrated Security = True;Connect Timeout=30");
                //optionsBuilder.UseSqlServer("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\AndyS\\OneDrive\\Documents\\Visual Studio 2017\\Projects\\MvcFactbook\\MvcFactbook\\Database\\MvcFactbook.mdf; Integrated Security = True;Connect Timeout=30");
                //optionsBuilder.UseSqlServer("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\dev\\MvcFactbook\\MvcFactbook\\Database\\MvcFactbook.mdf; Integrated Security = True;Connect Timeout=30");
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

                entity.Property(e => e.Complete)
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

                entity.Property(e => e.Complete)
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

            // Dockyard
            modelBuilder.Entity<Dockyard>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.Complete)
                    .IsRequired();
            });

            // Dockyard History
            modelBuilder.Entity<DockyardHistory>(entity =>
            {
                entity.Property(e => e.DockyardId)
                    .IsRequired();

                entity.Property(e => e.ShipbuilderId)
                    .IsRequired();

                entity.HasOne(x => x.Dockyard)
                    .WithMany(y => y.DockyardHistory)
                    .HasForeignKey(y => y.DockyardId)
                    .HasConstraintName("FK_DockyardHistory_DockyardId_To_Dockyard");

                entity.HasOne(x => x.Shipbuilder)
                    .WithMany(f => f.DockyardHistory)
                    .HasForeignKey(f => f.ShipbuilderId)
                    .HasConstraintName("FK_DockyardHistory_ShipbuilderId_To_Shipbuilder");
            });

            // Political Entity
            modelBuilder.Entity<PoliticalEntity>(entity =>
            {
                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.Code)
                    .IsRequired();

                entity.Property(e => e.Exists)
                    .IsRequired();

                entity.Property(e => e.HasEmblem)
                    .IsRequired();

                entity.Property(e => e.PoliticalEntityTypeId)
                    .IsRequired();

                entity.Property(e => e.Complete)
                    .IsRequired();

                entity.HasOne(x => x.PoliticalEntityType)
                    .WithMany(y => y.PoliticalEntities)
                    .HasForeignKey(y => y.PoliticalEntityTypeId)
                    .HasConstraintName("FK_PoliticalEntity_To_PoliticalEntityType");
            });

            // Political Entity Dockyard
            modelBuilder.Entity<PoliticalEntityDockyard>(entity =>
            {
                entity.Property(e => e.PoliticalEntityId)
                    .IsRequired();

                entity.Property(e => e.DockyardId)
                    .IsRequired();

                entity.HasOne(x => x.PoliticalEntity)
                    .WithMany(y => y.PoliticalEntityDockyards)
                    .HasForeignKey(y => y.PoliticalEntityId)
                    .HasConstraintName("FK_PolitcalEntityDockyard_To_PoliticalEntity");

                entity.HasOne(x => x.Dockyard)
                    .WithMany(f => f.PoliticalEntityDockyards)
                    .HasForeignKey(f => f.DockyardId)
                    .HasConstraintName("FK_PolitcalEntityDockyard_To_Dockyard");
            });

            // Political Entity Era
            modelBuilder.Entity<PoliticalEntityEra>(entity =>
            {
                entity.Property(e => e.PoliticalEntityId)
                    .IsRequired();

                entity.HasOne(x => x.PoliticalEntity)
                    .WithMany(y => y.PoliticalEntityEras)
                    .HasForeignKey(y => y.PoliticalEntityId)
                    .HasConstraintName("FK_PolitcalEntityEra_To_PoliticalEntity");
            });

            // Political Entity Flag
            modelBuilder.Entity<PoliticalEntityFlag>(entity =>
            {
                entity.Property(e => e.PoliticalEntityId)
                    .IsRequired();

                entity.Property(e => e.FlagId)
                    .IsRequired();

                entity.HasOne(x => x.PoliticalEntity)
                    .WithMany(y => y.PoliticalEntityFlags)
                    .HasForeignKey(y => y.PoliticalEntityId)
                    .HasConstraintName("FK_PolitcalEntityFlag_To_PoliticalEntity");

                entity.HasOne(x => x.Flag)
                    .WithMany(f => f.PoliticalEntityFlags)
                    .HasForeignKey(f => f.FlagId)
                    .HasConstraintName("FK_PolitcalEntityFlag_To_Flag");
            });

            // Political Entity Type
            modelBuilder.Entity<PoliticalEntityType>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired();
            });

            // Political Entity Succeeding
            modelBuilder.Entity<PoliticalEntitySucceeding>(entity =>
            {
                entity.Property(e => e.PoliticalEntityId)
                    .IsRequired();

                entity.Property(e => e.SucceedingPoliticalEntityId)
                    .IsRequired();

                entity.HasOne(x => x.PrecedingPoliticalEntity)
                    .WithMany(y => y.SucceedingEntities)
                    .HasForeignKey(y => y.PoliticalEntityId)
                    .HasConstraintName("FK_PoliticalEntitySucceeding_PoliticalEntityId_To_PoliticalEntity");

                entity.HasOne(x => x.SucceedingPoliticalEntity)
                    .WithMany(f => f.PrecedingEntities)
                    .HasForeignKey(f => f.SucceedingPoliticalEntityId)
                    .HasConstraintName("FK_PoliticalEntitySucceeding_SucceedingPoliticalEntityId_To_PoliticalEntity");
            });

            // Ship
            modelBuilder.Entity<Ship>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.Complete)
                    .IsRequired();

                entity.HasOne(x => x.Dockyard)
                    .WithMany(y => y.Ships)
                    .HasForeignKey(y => y.DockyardId)
                    .HasConstraintName("FK_Ship_To_Dockyard");
            });

            // Shipbuilder
            modelBuilder.Entity<Shipbuilder>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.Complete)
                    .IsRequired();
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

                entity.Property(e => e.Complete)
                    .IsRequired();
            });

            // Ship Group
            modelBuilder.Entity<ShipGroup>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired();
            });

            // Ship Group Set
            modelBuilder.Entity<ShipGroupSet>(entity =>
            {
                entity.Property(e => e.ShipServiceId)
                    .IsRequired();

                entity.Property(e => e.ShipGroupId)
                    .IsRequired();

                entity.HasOne(x => x.ShipService)
                    .WithMany(y => y.ShipGroupSets)
                    .HasForeignKey(y => y.ShipServiceId)
                    .HasConstraintName("FK_ShipGroupSet_To_ShipService");

                entity.HasOne(x => x.ShipGroup)
                    .WithMany(f => f.ShipGroupSets)
                    .HasForeignKey(f => f.ShipGroupId)
                    .HasConstraintName("FK_ShipGroupSet_To_ShipGroup");
            });

            // Ship Service
            modelBuilder.Entity<ShipService>(entity =>
            {
                entity.Property(e => e.Penant)
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.Active)
                    .IsRequired();

                entity.Property(e => e.Complete)
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

            // Succeeding Class
            modelBuilder.Entity<SucceedingClass>(entity =>
            {
                entity.Property(e => e.ShipClassId)
                    .IsRequired();

                entity.Property(e => e.SucceedingClassId)
                    .IsRequired();

                entity.HasOne(x => x.PrecedingShipClass)
                    .WithMany(y => y.SucceedingClasses)
                    .HasForeignKey(y => y.ShipClassId)
                    .HasConstraintName("FK_SucceedingClass_ShipClassId_To_ShipClass");

                entity.HasOne(x => x.SucceedingShipClass)
                    .WithMany(f => f.PrecedingClasses)
                    .HasForeignKey(f => f.SucceedingClassId)
                    .HasConstraintName("FK_SucceedingClass_SucceedingClassId_To_ShipClass");
            });
        }
    }
}