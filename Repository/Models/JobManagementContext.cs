using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repository.Models
{
    public partial class JobManagementContext : DbContext
    {
        public JobManagementContext()
        {
        }

        public JobManagementContext(DbContextOptions<JobManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CandidateProfile> CandidateProfiles { get; set; }
        public virtual DbSet<Hraccount> Hraccounts { get; set; }
        public virtual DbSet<JobPosting> JobPostings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=Zero@135;database= JobManagement;TrustServerCertificate=True;");
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CandidateProfile>(entity =>
            {
                entity.HasKey(e => e.CandidateId)
                    .HasName("PK__Candidat__DF539BFCEDFF8762");

                entity.ToTable("CandidateProfile");

                entity.Property(e => e.CandidateId)
                    .HasMaxLength(20)
                    .HasColumnName("CandidateID");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PostingId)
                    .HasMaxLength(25)
                    .HasColumnName("PostingID");

                entity.Property(e => e.ProfileShortDescription).HasMaxLength(250);

                entity.Property(e => e.ProfileUrl)
                    .HasMaxLength(200)
                    .HasColumnName("ProfileURL");

                entity.HasOne(d => d.Posting)
                    .WithMany(p => p.CandidateProfiles)
                    .HasForeignKey(d => d.PostingId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Candidate__Posti__267ABA7A");
            });

            modelBuilder.Entity<Hraccount>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__HRAccoun__A9D10535F5A95C65");

                entity.ToTable("HRAccount");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<JobPosting>(entity =>
            {
                entity.HasKey(e => e.PostingId)
                    .HasName("PK__JobPosti__C31796A566CDA25A");

                entity.ToTable("JobPosting");

                entity.Property(e => e.PostingId)
                    .HasMaxLength(25)
                    .HasColumnName("PostingID");

                entity.Property(e => e.JobDescription).HasMaxLength(250);

                entity.Property(e => e.JobPostingTitle)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.PostedDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
