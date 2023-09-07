using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace POlidvyAPI.Model
{
    public partial class PolicyMDBContext : DbContext
    {
        public PolicyMDBContext()
        {
        }

        public PolicyMDBContext(DbContextOptions<PolicyMDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerTbl> CustomerTbls { get; set; } = null!;
        public virtual DbSet<EmployerTypeTbl> EmployerTypeTbls { get; set; } = null!;
        public virtual DbSet<PolicyTbl> PolicyTbls { get; set; } = null!;
        public virtual DbSet<PolicyTypeTbl> PolicyTypeTbls { get; set; } = null!;
        public virtual DbSet<UserTbl> UserTbls { get; set; } = null!;
        public virtual DbSet<UserTypeTbl> UserTypeTbls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LTIN403505\\SQLEXPRESS;Database=PolicyMDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerTbl>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("Customer_tbl");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Address");

                entity.Property(e => e.CustomerBirthDate)
                    .HasColumnType("date")
                    .HasColumnName("Customer_Birth_Date");

                entity.Property(e => e.CustomerContactNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Contact_No");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Email");

                entity.Property(e => e.CustomerFirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Customer_First_Name");

                entity.Property(e => e.CustomerLastName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Last_Name");

                entity.Property(e => e.CustomerPanNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Pan_No");

                entity.Property(e => e.CustomerSalary)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Customer_Salary");

                entity.Property(e => e.EmployerName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Employer_Name");

                entity.Property(e => e.EmployerTypeId).HasColumnName("Employer_Type_Id");

                entity.HasOne(d => d.EmployerType)
                    .WithMany(p => p.CustomerTbls)
                    .HasForeignKey(d => d.EmployerTypeId)
                    .HasConstraintName("FK_Employer_Type_tbl");
            });

            modelBuilder.Entity<EmployerTypeTbl>(entity =>
            {
                entity.HasKey(e => e.EmployerTypeId);

                entity.ToTable("Employer_Type_tbl");

                entity.Property(e => e.EmployerTypeId).HasColumnName("Employer_Type_Id");

                entity.Property(e => e.EmployerTypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Employer_Type_Name");
            });

            modelBuilder.Entity<PolicyTbl>(entity =>
            {
                entity.HasKey(e => e.PolicyId);

                entity.ToTable("Policy_tbl");

                entity.Property(e => e.PolicyId).HasColumnName("Policy_Id");

                entity.Property(e => e.PolicyAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Policy_Amount");

                entity.Property(e => e.PolicyCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Policy_Code");

                entity.Property(e => e.PolicyCompany)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Policy_Company");

                entity.Property(e => e.PolicyDuration).HasColumnName("Policy_Duration");

                entity.Property(e => e.PolicyInitialDeposit)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Policy_Initial_Deposit");

                entity.Property(e => e.PolicyInterest)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("Policy_Interest");

                entity.Property(e => e.PolicyName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Policy_Name");

                entity.Property(e => e.PolicyStartDate)
                    .HasColumnType("date")
                    .HasColumnName("Policy_Start_Date");

                entity.Property(e => e.PolicyTermsPerYear).HasColumnName("Policy_Terms_Per_Year");

                entity.Property(e => e.PolicyTypeId).HasColumnName("Policy_Type_Id");

                entity.Property(e => e.UserTypeId).HasColumnName("User_Type_Id");

                entity.HasOne(d => d.PolicyType)
                    .WithMany(p => p.PolicyTbls)
                    .HasForeignKey(d => d.PolicyTypeId)
                    .HasConstraintName("FK_Policy_Type_tbl");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.PolicyTbls)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK_User_Type_tbl");
            });

            modelBuilder.Entity<PolicyTypeTbl>(entity =>
            {
                entity.HasKey(e => e.PolicyTypeId);

                entity.ToTable("Policy_Type_tbl");

                entity.Property(e => e.PolicyTypeId).HasColumnName("Policy_Type_Id");

                entity.Property(e => e.PolicyTypeCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Policy_Type_Code");

                entity.Property(e => e.PolicyTypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Policy_Type_Name");
            });

            modelBuilder.Entity<UserTbl>(entity =>
            {
                entity.ToTable("User_tbl");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserTypeTbl>(entity =>
            {
                entity.HasKey(e => e.UserTypeId);

                entity.ToTable("User_Type_tbl");

                entity.Property(e => e.UserTypeId).HasColumnName("User_Type_Id");

                entity.Property(e => e.UserIncomePerYear)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("User_Income_Per_Year");

                entity.Property(e => e.UserTypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("User_Type_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
