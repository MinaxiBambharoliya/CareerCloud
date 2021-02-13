using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext

    {
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }

        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }

        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }

        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }

        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }

        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }

        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }

        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }

        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }

        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }

        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }

        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }

        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }

        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }

        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }

        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }

        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }

        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }

        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected string connString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(new BaseConnection().connString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyProfilePoco>().Ignore("TimeStamp");

            modelBuilder.Entity<CompanyDescriptionPoco>().HasOne(p => p.CompanyProfile).WithMany(d=> d.CompanyDescriptions).HasForeignKey(p=> p.Id);
            modelBuilder.Entity<CompanyDescriptionPoco>().HasOne(l=> l.SystemLanguageCode).WithMany(d => d.CompanyDescriptions).HasForeignKey(l=>l.LanguageId);
            modelBuilder.Entity<CompanyDescriptionPoco>().Ignore(d => d.TimeStamp);

            modelBuilder.Entity<CompanyJobPoco>().HasOne(p => p.CompanyProfile).WithMany(j => j.CompanyJobs).HasForeignKey(f => f.Id);
            modelBuilder.Entity<CompanyJobPoco>().Ignore("TimeStamp");

            modelBuilder.Entity<CompanyJobDescriptionPoco>().HasOne(p => p.CompanyJob).WithMany(j => j.CompanyJobDescriptions).HasForeignKey(f => f.Id);
            modelBuilder.Entity<CompanyJobDescriptionPoco>().Ignore("TimeStamp");

            modelBuilder.Entity<CompanyLocationPoco>().HasOne(p => p.CompanyProfile).WithMany(d => d.CompanyLocations).HasForeignKey(p => p.Id);
            modelBuilder.Entity<CompanyLocationPoco>().HasOne(c => c.SystemCountryCode).WithMany(l=> l.CompanyLocations).HasForeignKey(f => f.CountryCode);
            modelBuilder.Entity<CompanyLocationPoco>().Ignore(d => d.TimeStamp);

            modelBuilder.Entity<CompanyJobEducationPoco>().HasOne(p => p.CompanyJob).WithMany(j => j.CompanyJobEducations).HasForeignKey(f => f.Id);
            modelBuilder.Entity<CompanyJobEducationPoco>().Ignore("TimeStamp");

            modelBuilder.Entity<CompanyJobSkillPoco>().HasOne(p => p.CompanyJob).WithMany(j => j.CompanyJobSkills).HasForeignKey(f => f.Id);
            modelBuilder.Entity<CompanyJobSkillPoco>().Ignore("TimeStamp");

            modelBuilder.Entity<SecurityLoginPoco>().Ignore("TimeStamp");

            modelBuilder.Entity<SecurityLoginsLogPoco>().HasOne(p => p.SecurityLogin).WithMany(j => j.SecurityLoginsLogs).HasForeignKey(f => f.Id);

            modelBuilder.Entity<SecurityLoginsRolePoco>().HasOne(p => p.SecurityLogin).WithMany(j => j.SecurityLoginsRoles).HasForeignKey(f => f.Id);
            modelBuilder.Entity<SecurityLoginsRolePoco>().HasOne(p => p.SecurityRole).WithMany(j => j.SecurityLoginsRoles).HasForeignKey(f => f.Id);
            modelBuilder.Entity<SecurityLoginsRolePoco>().Ignore("TimeStamp");

            modelBuilder.Entity<ApplicantProfilePoco>().HasOne(p => p.SecurityLogin).WithMany(j => j.ApplicantProfiles).HasForeignKey(f => f.Id);
            modelBuilder.Entity<ApplicantProfilePoco>().HasOne(p => p.SystemCountryCode).WithMany(j => j.ApplicantProfiles).HasForeignKey(f => f.Country);
            modelBuilder.Entity<ApplicantProfilePoco>().Ignore("TimeStamp");

            modelBuilder.Entity<ApplicantEducationPoco>().HasOne(p => p.ApplicantProfile).WithMany(j => j.ApplicantEducations).HasForeignKey(f => f.Id);
            modelBuilder.Entity<ApplicantEducationPoco>().Ignore("TimeStamp");

            modelBuilder.Entity<ApplicantJobApplicationPoco>().HasOne(p => p.ApplicantProfile).WithMany(j => j.ApplicantJobApplications).HasForeignKey(f => f.Id);
            modelBuilder.Entity<ApplicantJobApplicationPoco>().HasOne(p => p.CompanyJob).WithMany(j => j.ApplicantJobApplications).HasForeignKey(f => f.Id);
            modelBuilder.Entity<ApplicantJobApplicationPoco>().Ignore("TimeStamp");

            modelBuilder.Entity<ApplicantResumePoco>().HasOne(p => p.ApplicantProfile).WithMany(j => j.ApplicantResumes).HasForeignKey(f => f.Id);
             modelBuilder.Entity<ApplicantJobApplicationPoco>().Ignore("TimeStamp");


            modelBuilder.Entity<ApplicantSkillPoco>().HasOne(p => p.ApplicantProfile).WithMany(j => j.ApplicantSkills).HasForeignKey(f => f.Id);
            modelBuilder.Entity<ApplicantSkillPoco>().Ignore("TimeStamp");

            modelBuilder.Entity<ApplicantWorkHistoryPoco>().HasOne(p => p.ApplicantProfile).WithMany(j => j.ApplicantWorkHistorys).HasForeignKey(f => f.Id);
            modelBuilder.Entity<ApplicantWorkHistoryPoco>().HasOne(p => p.SystemCountryCode).WithMany(j => j.ApplicantWorkHistories).HasForeignKey(f => f.CountryCode);
            modelBuilder.Entity<ApplicantWorkHistoryPoco>().Ignore("TimeStamp");

            base.OnModelCreating(modelBuilder);
        }
    }
}
