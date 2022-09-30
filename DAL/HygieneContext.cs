using System;
using System.Linq;
using DAL.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL
{
    public partial class HygieneContext : DbContext
    {
        public static string DatabaseServer = "";
        public HygieneContext()
        {
            this.SavingChanges += HygieneContext_SavingChanges;
            this.SavedChanges += HygieneContext_SavedChanges;
            this.SaveChangesFailed += HygieneContext_SaveChangesFailed;
        }

        private void HygieneContext_SaveChangesFailed(object sender, SaveChangesFailedEventArgs e)
        {
            //e.Exception
        }

        private void HygieneContext_SavedChanges(object sender, SavedChangesEventArgs e)
        {
            //e.EntitiesSavedCount
        }

        private void HygieneContext_SavingChanges(object sender, SavingChangesEventArgs e)
        {
            var added = this.ChangeTracker.Entries().Where(c => c.State == EntityState.Added).Select(ent => ent.OriginalValues);
            var deleted = this.ChangeTracker.Entries().Where(c => c.State == EntityState.Deleted).Select(ent => ent.OriginalValues);
            var modified = this.ChangeTracker.Entries().Where(c => c.State == EntityState.Modified).Select(ent => ent.OriginalValues);
        }
      

        public HygieneContext(DbContextOptions<HygieneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessGroup> AccessGroups { get; set; }
        public virtual DbSet<AccessGroupForm> AccessGroupForms { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Cleaning> Cleanings { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<Frequency> Frequencies { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<PlaceActivity> PlaceActivities { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<SitePlace> SitePlaces { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Hygiene;Trusted_Connection=False;User Id=sa;Password=Allahakbar;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//this is necessary for codde first

            modelBuilder.Entity<AccessGroup>(entity =>
            {
                entity.ToTable("AccessGroup");

               // entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccessGroupName).HasMaxLength(55);
            });

            modelBuilder.Entity<AccessGroupForm>(entity =>
            {
              //  entity.HasKey(e => new { e.Id, e.FormId });

                entity.ToTable("AccessGroupForm");
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity");

               // entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ActivityName).HasMaxLength(155);
            });

            modelBuilder.Entity<Cleaning>(entity =>
            {
                entity.ToTable("Cleaning");

               // entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CleanEndDate).HasColumnType("date");

                entity.Property(e => e.CleanStartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

               // entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(155);

                entity.Property(e => e.Ape)
                    .HasMaxLength(15)
                    .HasColumnName("APE");

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.Fax).HasMaxLength(55);

                entity.Property(e => e.Mail).HasMaxLength(55);

               // entity.Property(e => e.Photo).HasColumnType("image");

                entity.Property(e => e.Responsible)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.Siret)
                    .HasMaxLength(15)
                    .HasColumnName("SIRET");

                entity.Property(e => e.Tel).HasMaxLength(55);
            });

            modelBuilder.Entity<Form>(entity =>
            {
               // entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FormName).HasMaxLength(55);
            });

            modelBuilder.Entity<Frequency>(entity =>
            {
                entity.ToTable("Frequency");

               // entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FrequencyName).HasMaxLength(15);

                entity.Property(e => e.YearMonthWeek).HasMaxLength(11);
            });

            modelBuilder.Entity<Operation>(entity =>
            {
               // entity.HasKey(e => new { e.SiteId, e.PlaceId, e.ActivityId });

                entity.ToTable("Operation");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("Place");

              //  entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PlaceName)
                    .IsRequired()
                    .HasMaxLength(75);
            });

            modelBuilder.Entity<PlaceActivity>(entity =>
            {
              //  entity.HasKey(e => new { e.ActivityId, e.PlaceId })
                //    .HasName("PK_SiteActivity");

                entity.ToTable("PlaceActivity");
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.ToTable("Site");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Address).HasMaxLength(155);

                //entity.Property(e => e.ClientId)
                //    .IsRequired()
                //    .HasMaxLength(10)
                //    .IsFixedLength(true);

                entity.Property(e => e.Consultant).HasMaxLength(75);

                entity.Property(e => e.ConsultantIntern).HasMaxLength(5);

                entity.Property(e => e.ConsultantMail).HasMaxLength(55);

                entity.Property(e => e.ConsultantTel).HasMaxLength(55);

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.ContractDate).HasColumnType("date");

                entity.Property(e => e.ContractNo).HasMaxLength(15);

                entity.Property(e => e.Fax).HasMaxLength(55);

                entity.Property(e => e.Lat).HasColumnType("decimal(18, 13)");

                entity.Property(e => e.Long).HasColumnType("decimal(18, 13)");

                entity.Property(e => e.Mail).HasMaxLength(55);

                entity.Property(e => e.Port).HasMaxLength(55);

                entity.Property(e => e.SiteName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Tel).HasMaxLength(55);

                entity.Property(e => e.Version).HasMaxLength(5);
            });

            modelBuilder.Entity<SitePlace>(entity =>
            {
                //entity.HasKey(e => new { e.SiteId, e.PlaceId })
                //    .HasName("PK_ClientPlace");

                entity.ToTable("SitePlace");
            });

            modelBuilder.Entity<User>(entity =>
            {
              //  entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Pass).HasMaxLength(55);

                entity.Property(e => e.UserName).HasMaxLength(55);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
