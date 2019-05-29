using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Chaka.Database.Context.Models
{
    public partial class ChakaContext : DbContext
    {
        public ChakaContext()
        {
        }

        public ChakaContext(DbContextOptions<ChakaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivityHistory> ActivityHistory { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<BookCategory> BookCategory { get; set; }
        public virtual DbSet<BusinessFieldCategory> BusinessFieldCategory { get; set; }
        public virtual DbSet<BusinessFieldClassification> BusinessFieldClassification { get; set; }
        public virtual DbSet<BusinessFieldRegulation> BusinessFieldRegulation { get; set; }
        public virtual DbSet<BusinessStatusOwnership> BusinessStatusOwnership { get; set; }
        public virtual DbSet<Calendar> Calendar { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<CityDeprecated> CityDeprecated { get; set; }
        public virtual DbSet<CompanyBrand> CompanyBrand { get; set; }
        public virtual DbSet<Configuration> Configuration { get; set; }
        public virtual DbSet<CoreDefinition> CoreDefinition { get; set; }
        public virtual DbSet<CoreFilter> CoreFilter { get; set; }
        public virtual DbSet<CostCenter> CostCenter { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<CountryDeprecated> CountryDeprecated { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<EducationFaculty> EducationFaculty { get; set; }
        public virtual DbSet<EducationMajor> EducationMajor { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeBiodata> EmployeeBiodata { get; set; }
        public virtual DbSet<EmployeeInfoRestrictionGroup> EmployeeInfoRestrictionGroup { get; set; }
        public virtual DbSet<EmployeeListFilter> EmployeeListFilter { get; set; }
        public virtual DbSet<EmployeeListFilterGrade> EmployeeListFilterGrade { get; set; }
        public virtual DbSet<EmployeeListFilterLevel> EmployeeListFilterLevel { get; set; }
        public virtual DbSet<EmployeeListFilterLocation> EmployeeListFilterLocation { get; set; }
        public virtual DbSet<EmployeeListFilterOrgUnit> EmployeeListFilterOrgUnit { get; set; }
        public virtual DbSet<EmployeeListFilterPosition> EmployeeListFilterPosition { get; set; }
        public virtual DbSet<EmployeeListFilterStatus> EmployeeListFilterStatus { get; set; }
        public virtual DbSet<EmployeeStatus> EmployeeStatus { get; set; }
        public virtual DbSet<Function> Function { get; set; }
        public virtual DbSet<Funding> Funding { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<GradeGroup> GradeGroup { get; set; }
        public virtual DbSet<JobDescription> JobDescription { get; set; }
        public virtual DbSet<JobFamily> JobFamily { get; set; }
        public virtual DbSet<JobFamilyMajor> JobFamilyMajor { get; set; }
        public virtual DbSet<JobFunction> JobFunction { get; set; }
        public virtual DbSet<JobMaster> JobMaster { get; set; }
        public virtual DbSet<JobStatus> JobStatus { get; set; }
        public virtual DbSet<JobTitle> JobTitle { get; set; }
        public virtual DbSet<JobtitleRequirement> JobtitleRequirement { get; set; }
        public virtual DbSet<JobtitleSpecification> JobtitleSpecification { get; set; }
        public virtual DbSet<JobtitleSpecificationMajor> JobtitleSpecificationMajor { get; set; }
        public virtual DbSet<KppBranch> KppBranch { get; set; }
        public virtual DbSet<LegalEntityInformation> LegalEntityInformation { get; set; }
        public virtual DbSet<Level> Level { get; set; }
        public virtual DbSet<LevelCategory> LevelCategory { get; set; }
        public virtual DbSet<LevelEdu> LevelEdu { get; set; }
        public virtual DbSet<LineOfBusiness> LineOfBusiness { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<LocationClassification> LocationClassification { get; set; }
        public virtual DbSet<LocationClassification1> LocationClassification1 { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuDetail> MenuDetail { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<OauthClient> OauthClient { get; set; }
        public virtual DbSet<OrgUnit> OrgUnit { get; set; }
        public virtual DbSet<OrgUnitChangeStatus> OrgUnitChangeStatus { get; set; }
        public virtual DbSet<OrgUnitTransDetJobTitle> OrgUnitTransDetJobTitle { get; set; }
        public virtual DbSet<OrgUnitTransaction> OrgUnitTransaction { get; set; }
        public virtual DbSet<OrgUnitTransactionDetail> OrgUnitTransactionDetail { get; set; }
        public virtual DbSet<OrganizationLevel> OrganizationLevel { get; set; }
        public virtual DbSet<OrganizationListJobtitle> OrganizationListJobtitle { get; set; }
        public virtual DbSet<OrganizationListLocation> OrganizationListLocation { get; set; }
        public virtual DbSet<OrganizationUnitCategory> OrganizationUnitCategory { get; set; }
        public virtual DbSet<OrganizationUnitType> OrganizationUnitType { get; set; }
        public virtual DbSet<PasswordAuditTrail> PasswordAuditTrail { get; set; }
        public virtual DbSet<PreferenceAuthority> PreferenceAuthority { get; set; }
        public virtual DbSet<PreferenceGroup> PreferenceGroup { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<ProvinceDeprecated> ProvinceDeprecated { get; set; }
        public virtual DbSet<ReminderNotification> ReminderNotification { get; set; }
        public virtual DbSet<RespGroupDetail> RespGroupDetail { get; set; }
        public virtual DbSet<Responsibility> Responsibility { get; set; }
        public virtual DbSet<ResponsibilityGroup> ResponsibilityGroup { get; set; }
        public virtual DbSet<Siupclass> Siupclass { get; set; }
        public virtual DbSet<Siupclass1> Siupclass1 { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<TaxDuty> TaxDuty { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserForgotPassword> UserForgotPassword { get; set; }
        public virtual DbSet<UserFunction> UserFunction { get; set; }
        public virtual DbSet<UserFunctionAuditTrail> UserFunctionAuditTrail { get; set; }
        public virtual DbSet<UserResponsibility> UserResponsibility { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
        public virtual DbSet<UsersImei> UsersImei { get; set; }
        public virtual DbSet<UsersImeitransaction> UsersImeitransaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=172.16.31.52;Database=Chaka;UID=sa;password=Medion.2019;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<ActivityHistory>(entity =>
            {
                entity.ToTable("ActivityHistory", "Preference");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ActionDate)
                    .HasColumnName("Action_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ActivityInstanceDestinationId).HasColumnName("Activity_Instance_Destination_ID");

                entity.Property(e => e.ActivityName)
                    .IsRequired()
                    .HasColumnName("Activity_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ParticipantLoginName)
                    .HasColumnName("Participant_Login_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessInstanceId).HasColumnName("Process_Instance_ID");

                entity.Property(e => e.ReferenceId).HasColumnName("Reference_ID");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.Property(e => e.WorkflowName)
                    .HasColumnName("Workflow_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachment", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AttachmentFile)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.TableOriginId).HasColumnName("TableOriginID");

                entity.Property(e => e.TableOriginName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.ToTable("BookCategory", "SysAdmin");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BusinessFieldCategory>(entity =>
            {
                entity.ToTable("BusinessFieldCategory", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessFieldRegulationId).HasColumnName("BusinessFieldRegulationID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.BusinessFieldRegulation)
                    .WithMany(p => p.BusinessFieldCategory)
                    .HasForeignKey(d => d.BusinessFieldRegulationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BusinessFieldCategory_BusinessFieldRegulation");
            });

            modelBuilder.Entity<BusinessFieldClassification>(entity =>
            {
                entity.ToTable("BusinessFieldClassification", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessFieldCategoryId).HasColumnName("BusinessFieldCategoryID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.GroupCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MainSectionCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.MainSectionName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SectionCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubSectionCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SubSectionName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.BusinessFieldCategory)
                    .WithMany(p => p.BusinessFieldClassification)
                    .HasForeignKey(d => d.BusinessFieldCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BusinessFieldClassification_BusinessFieldCategory");
            });

            modelBuilder.Entity<BusinessFieldRegulation>(entity =>
            {
                entity.ToTable("BusinessFieldRegulation", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppointedDate).HasColumnType("date");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BusinessStatusOwnership>(entity =>
            {
                entity.ToTable("BusinessStatusOwnership", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.ToTable("Calendar", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City", "Preference");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_City_Province");
            });

            modelBuilder.Entity<CityDeprecated>(entity =>
            {
                entity.ToTable("City_Deprecated", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.MinimumSalary).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.CityDeprecated)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_State");
            });

            modelBuilder.Entity<CompanyBrand>(entity =>
            {
                entity.ToTable("CompanyBrand", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("Configuration", "SysAdmin");

                entity.Property(e => e.Key)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.HelpText)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreDefinition>(entity =>
            {
                entity.ToTable("CoreDefinition", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CoreFilter>(entity =>
            {
                entity.ToTable("CoreFilter", "SysAdmin");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CoreId).HasColumnName("CoreID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DetailId).HasColumnName("DetailID");

                entity.Property(e => e.EmployeeListFilterId).HasColumnName("EmployeeListFilterID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.CoreFilter)
                    .HasForeignKey<CoreFilter>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoreFilter_CoreDefinition");

                entity.HasOne(d => d.Id1)
                    .WithOne(p => p.CoreFilter)
                    .HasForeignKey<CoreFilter>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoreFilter_EmployeeListFilter");
            });

            modelBuilder.Entity<CostCenter>(entity =>
            {
                entity.ToTable("CostCenter", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "Preference");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CountryDeprecated>(entity =>
            {
                entity.ToTable("Country_Deprecated", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<EducationFaculty>(entity =>
            {
                entity.ToTable("EducationFaculty", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<EducationMajor>(entity =>
            {
                entity.ToTable("EducationMajor", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EducationMajorFacId).HasColumnName("EducationMajorFacID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.EducationMajorFac)
                    .WithMany(p => p.EducationMajor)
                    .HasForeignKey(d => d.EducationMajorFacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EducationMajor_EducationMajorFaculty");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", "People");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.FinalProcessDate).HasColumnType("date");

                entity.Property(e => e.FingerPrintCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.HiredDate).HasColumnType("date");

                entity.Property(e => e.LastWorkingDate).HasColumnType("date");

                entity.Property(e => e.Nik)
                    .IsRequired()
                    .HasColumnName("NIK")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousEmployeeId).HasColumnName("PreviousEmployeeID");

                entity.Property(e => e.ProbationPeriodEnd).HasColumnType("date");

                entity.Property(e => e.ProbationPeriodStart).HasColumnType("date");

                entity.Property(e => e.TerminationDate).HasColumnType("date");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.PreviousEmployee)
                    .WithMany(p => p.InversePreviousEmployee)
                    .HasForeignKey(d => d.PreviousEmployeeId)
                    .HasConstraintName("FK_Employee_OldEmployee");
            });

            modelBuilder.Entity<EmployeeBiodata>(entity =>
            {
                entity.ToTable("EmployeeBiodata", "People");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BeginEff).HasColumnType("date");

                entity.Property(e => e.BirthCityId).HasColumnName("BirthCityID");

                entity.Property(e => e.BirthCountryId).HasColumnName("BirthCountryID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.BirthStateId).HasColumnName("BirthStateID");

                entity.Property(e => e.BloodTypeId).HasColumnName("BloodTypeID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.EthnicOriginId).HasColumnName("EthnicOriginID");

                entity.Property(e => e.FamilyCardNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IdentityNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IdentityTypeId).HasColumnName("IdentityTypeID");

                entity.Property(e => e.LastEff).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NationalityId).HasColumnName("NationalityID");

                entity.Property(e => e.NickName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonalEmailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhysicalCondition)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ReligionId).HasColumnName("ReligionID");

                entity.Property(e => e.StatusInFamilyCard)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TitleId).HasColumnName("TitleID");

                entity.Property(e => e.UniformSizeId).HasColumnName("UniformSizeID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.WorkEmailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeInfoRestrictionGroup>(entity =>
            {
                entity.ToTable("EmployeeInfoRestrictionGroup", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmployeeListFilter>(entity =>
            {
                entity.ToTable("EmployeeListFilter", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmployeeListFilterGrade>(entity =>
            {
                entity.ToTable("EmployeeListFilterGrade", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeListFilterId).HasColumnName("EmployeeListFilterID");

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeListFilter)
                    .WithMany(p => p.EmployeeListFilterGrade)
                    .HasForeignKey(d => d.EmployeeListFilterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeListFilterGrade_EmployeeListFilter");
            });

            modelBuilder.Entity<EmployeeListFilterLevel>(entity =>
            {
                entity.ToTable("EmployeeListFilterLevel", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeListFilterId).HasColumnName("EmployeeListFilterID");

                entity.Property(e => e.LevelId).HasColumnName("LevelID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeListFilter)
                    .WithMany(p => p.EmployeeListFilterLevel)
                    .HasForeignKey(d => d.EmployeeListFilterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeListFilterLevel_EmployeeListFilter");
            });

            modelBuilder.Entity<EmployeeListFilterLocation>(entity =>
            {
                entity.ToTable("EmployeeListFilterLocation", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeListFilterId).HasColumnName("EmployeeListFilterID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeListFilter)
                    .WithMany(p => p.EmployeeListFilterLocation)
                    .HasForeignKey(d => d.EmployeeListFilterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeListFilterLocation_EmployeeListFilter");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.EmployeeListFilterLocation)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeListFilterLocation_Location");
            });

            modelBuilder.Entity<EmployeeListFilterOrgUnit>(entity =>
            {
                entity.ToTable("EmployeeListFilterOrgUnit", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeListFilterId).HasColumnName("EmployeeListFilterID");

                entity.Property(e => e.OrgUnitId).HasColumnName("OrgUnitID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeListFilter)
                    .WithMany(p => p.EmployeeListFilterOrgUnit)
                    .HasForeignKey(d => d.EmployeeListFilterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeListFilterOrgUnit_EmployeeListFilter");
            });

            modelBuilder.Entity<EmployeeListFilterPosition>(entity =>
            {
                entity.ToTable("EmployeeListFilterPosition", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeListFilterId).HasColumnName("EmployeeListFilterID");

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeListFilter)
                    .WithMany(p => p.EmployeeListFilterPosition)
                    .HasForeignKey(d => d.EmployeeListFilterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeListFilterPosition_EmployeeListFilter");
            });

            modelBuilder.Entity<EmployeeListFilterStatus>(entity =>
            {
                entity.ToTable("EmployeeListFilterStatus", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeListFilterId).HasColumnName("EmployeeListFilterID");

                entity.Property(e => e.EmploymentStatusId).HasColumnName("EmploymentStatusID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeListFilter)
                    .WithMany(p => p.EmployeeListFilterStatus)
                    .HasForeignKey(d => d.EmployeeListFilterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeListFilterStatus_EmployeeListFilter");

                entity.HasOne(d => d.EmploymentStatus)
                    .WithMany(p => p.EmployeeListFilterStatus)
                    .HasForeignKey(d => d.EmploymentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeListFilterStatus_EmploymentStatus");
            });

            modelBuilder.Entity<EmployeeStatus>(entity =>
            {
                entity.ToTable("EmployeeStatus", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Function>(entity =>
            {
                entity.ToTable("Function", "SysAdmin");

                entity.HasIndex(e => new { e.Name, e.DelDate })
                    .HasName("UQ1_Function")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessTemplateId).HasColumnName("BusinessTemplateID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.DescriptionEng)
                    .HasColumnName("DescriptionENG")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DescriptionIna)
                    .HasColumnName("DescriptionINA")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FunctionView)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IconUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Funding>(entity =>
            {
                entity.ToTable("Funding", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.GroupGradeId).HasColumnName("GroupGradeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.GroupGrade)
                    .WithMany(p => p.Grade)
                    .HasForeignKey(d => d.GroupGradeId)
                    .HasConstraintName("FK_Grade_GradeGroup");
            });

            modelBuilder.Entity<GradeGroup>(entity =>
            {
                entity.ToTable("GradeGroup", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<JobDescription>(entity =>
            {
                entity.ToTable("JobDescription", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.JobMasterId).HasColumnName("JobMasterID");

                entity.Property(e => e.JobtitleId).HasColumnName("JobtitleID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Jobtitle)
                    .WithMany(p => p.JobDescription)
                    .HasForeignKey(d => d.JobtitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobDescription_Jobtitle");
            });

            modelBuilder.Entity<JobFamily>(entity =>
            {
                entity.ToTable("JobFamily", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<JobFamilyMajor>(entity =>
            {
                entity.ToTable("JobFamilyMajor", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.JobFamilyId).HasColumnName("JobFamilyID");

                entity.Property(e => e.MajorId).HasColumnName("MajorID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.JobFamily)
                    .WithMany(p => p.JobFamilyMajor)
                    .HasForeignKey(d => d.JobFamilyId)
                    .HasConstraintName("FK_JobFamilyMajor_JobFamily");
            });

            modelBuilder.Entity<JobFunction>(entity =>
            {
                entity.ToTable("JobFunction", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<JobMaster>(entity =>
            {
                entity.ToTable("JobMaster", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<JobStatus>(entity =>
            {
                entity.ToTable("JobStatus", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.ToTable("JobTitle", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BeginEff).HasColumnType("date");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.JobFamilyId).HasColumnName("JobFamilyID");

                entity.Property(e => e.JobLevelCategoryId).HasColumnName("JobLevelCategoryID");

                entity.Property(e => e.JobLevelId).HasColumnName("JobLevelID");

                entity.Property(e => e.JobStatusId).HasColumnName("JobStatusID");

                entity.Property(e => e.LastEff).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.JobStatus)
                    .WithMany(p => p.JobTitle)
                    .HasForeignKey(d => d.JobStatusId)
                    .HasConstraintName("FK_JobTitle_JobStatus");
            });

            modelBuilder.Entity<JobtitleRequirement>(entity =>
            {
                entity.ToTable("JobtitleRequirement", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.JobtitleId).HasColumnName("JobtitleID");

                entity.Property(e => e.MajorId).HasColumnName("MajorID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Jobtitle)
                    .WithMany(p => p.JobtitleRequirement)
                    .HasForeignKey(d => d.JobtitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobtitleRequirement_Jobtitle");
            });

            modelBuilder.Entity<JobtitleSpecification>(entity =>
            {
                entity.ToTable("JobtitleSpecification", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Descriptions)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.JobFunctionId).HasColumnName("JobFunctionID");

                entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");

                entity.Property(e => e.LevelEduId).HasColumnName("LevelEduID");

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.JobFunction)
                    .WithMany(p => p.JobtitleSpecification)
                    .HasForeignKey(d => d.JobFunctionId)
                    .HasConstraintName("FK_JobtitleSpecification_JobFunction");

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.JobtitleSpecification)
                    .HasForeignKey(d => d.JobTitleId)
                    .HasConstraintName("FK_JobtitleSpecification_JobTitle");

                entity.HasOne(d => d.Line)
                    .WithMany(p => p.JobtitleSpecification)
                    .HasForeignKey(d => d.LineId)
                    .HasConstraintName("FK_JobtitleSpecification_LineOfBusiness");
            });

            modelBuilder.Entity<JobtitleSpecificationMajor>(entity =>
            {
                entity.ToTable("JobtitleSpecificationMajor", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.JobtitleSpecificationId).HasColumnName("JobtitleSpecificationID");

                entity.Property(e => e.MajorId).HasColumnName("MajorID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.JobtitleSpecification)
                    .WithMany(p => p.JobtitleSpecificationMajor)
                    .HasForeignKey(d => d.JobtitleSpecificationId)
                    .HasConstraintName("FK_JobtitleSpecificationMajor_JobtitleSpecification");
            });

            modelBuilder.Entity<KppBranch>(entity =>
            {
                entity.ToTable("KppBranch", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.KppBranch)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KppBranch_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.KppBranch)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KppBranch_Country");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.KppBranch)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KppBranch_State");
            });

            modelBuilder.Entity<LegalEntityInformation>(entity =>
            {
                entity.ToTable("LegalEntityInformation", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.BusinessFieldCategoryId).HasColumnName("BusinessFieldCategoryID");

                entity.Property(e => e.BusinessFieldClassificationId).HasColumnName("BusinessFieldClassificationID");

                entity.Property(e => e.BusinessFieldRegulationId).HasColumnName("BusinessFieldRegulationID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.BusinessStatusOwnershipId).HasColumnName("BusinessStatusOwnershipID");

                entity.Property(e => e.CompanyBrandId).HasColumnName("CompanyBrandID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.FaxNumber1)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FaxNumber2)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FundingId).HasColumnName("FundingID");

                entity.Property(e => e.KppbranchId).HasColumnName("KPPBranchID");

                entity.Property(e => e.ManagingDirectorId).HasColumnName("ManagingDirectorID");

                entity.Property(e => e.PhoneNumber1)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber2)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SitubeginEff)
                    .HasColumnName("SITUBeginEff")
                    .HasColumnType("date");

                entity.Property(e => e.SitubusinessArea)
                    .HasColumnName("SITUBusinessArea")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SitulastEff)
                    .HasColumnName("SITULastEff")
                    .HasColumnType("date");

                entity.Property(e => e.Situnumber)
                    .HasColumnName("SITUNumber")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiupbeginEff)
                    .HasColumnName("SIUPBeginEff")
                    .HasColumnType("date");

                entity.Property(e => e.SiupbusinessInstitutional)
                    .HasColumnName("SIUPBusinessInstitutional")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiupclassId).HasColumnName("SIUPClassID");

                entity.Property(e => e.SiupgoodsOrService)
                    .HasColumnName("SIUPGoodsOrService")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiupinitialAsset)
                    .HasColumnName("SIUPInitialAsset")
                    .HasColumnType("money");

                entity.Property(e => e.SiuplastEff)
                    .HasColumnName("SIUPLastEff")
                    .HasColumnType("date");

                entity.Property(e => e.Siupnumber)
                    .HasColumnName("SIUPNumber")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sppkpnumber)
                    .IsRequired()
                    .HasColumnName("SPPKPNumber")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SppkpreleaseDate)
                    .HasColumnName("SPPKPReleaseDate")
                    .HasColumnType("date");

                entity.Property(e => e.TaxDutyId).HasColumnName("TaxDutyID");

                entity.Property(e => e.TaxFileNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TaxInvoiceSerialCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TdpbeginEff)
                    .HasColumnName("TDPBeginEff")
                    .HasColumnType("date");

                entity.Property(e => e.TdplastEff)
                    .HasColumnName("TDPLastEff")
                    .HasColumnType("date");

                entity.Property(e => e.Tdpnumber)
                    .IsRequired()
                    .HasColumnName("TDPNumber")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TfnregisterDate)
                    .HasColumnName("TFNRegisterDate")
                    .HasColumnType("date");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.AttachmentId)
                    .HasConstraintName("FK_LegalEntityInformation_Attachment");

                entity.HasOne(d => d.BusinessFieldCategory)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.BusinessFieldCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalEntityInformation_BusinessFieldCategory");

                entity.HasOne(d => d.BusinessFieldClassification)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.BusinessFieldClassificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalEntityInformation_BusinessFieldClassification");

                entity.HasOne(d => d.BusinessFieldRegulation)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.BusinessFieldRegulationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalEntityInformation_BusinessFieldRegulation");

                entity.HasOne(d => d.BusinessStatusOwnership)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.BusinessStatusOwnershipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalEntityInformation_BusinessStatusOwnership");

                entity.HasOne(d => d.CompanyBrand)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.CompanyBrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalEntityInformation_CompanyBrand");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalEntityInformation_Currency");

                entity.HasOne(d => d.Funding)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.FundingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalEntityInformation_Funding");

                entity.HasOne(d => d.Kppbranch)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.KppbranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalEntityInformation_KppBranch");

                entity.HasOne(d => d.ManagingDirector)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.ManagingDirectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalEntityInformation_Employee");

                entity.HasOne(d => d.Siupclass)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.SiupclassId)
                    .HasConstraintName("FK_LegalEntityInformation_SIUPClass");

                entity.HasOne(d => d.TaxDuty)
                    .WithMany(p => p.LegalEntityInformation)
                    .HasForeignKey(d => d.TaxDutyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalEntityInformation_TaxDuty");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.ToTable("Level", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LevelCategoryId).HasColumnName("LevelCategoryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.LevelCategory)
                    .WithMany(p => p.Level)
                    .HasForeignKey(d => d.LevelCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Level_LevelCategory");
            });

            modelBuilder.Entity<LevelCategory>(entity =>
            {
                entity.ToTable("LevelCategory", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LevelEdu>(entity =>
            {
                entity.ToTable("LevelEdu", "Preference");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LineOfBusiness>(entity =>
            {
                entity.ToTable("LineOfBusiness", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BeginEff).HasColumnType("date");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.ClassificationId).HasColumnName("ClassificationID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastEff).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Postalcode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_City");

                entity.HasOne(d => d.Classification)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.ClassificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_LocationClassification");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_Country");
            });

            modelBuilder.Entity<LocationClassification>(entity =>
            {
                entity.ToTable("LocationClassification", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LocationClassification1>(entity =>
            {
                entity.ToTable("LocationClassification", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NameEng)
                    .IsRequired()
                    .HasColumnName("NameENG")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameIna)
                    .IsRequired()
                    .HasColumnName("NameINA")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MenuDetail>(entity =>
            {
                entity.ToTable("MenuDetail", "SysAdmin");

                entity.HasIndex(e => new { e.MenuId, e.FunctionId, e.DelDate })
                    .HasName("UQ1_MenuDetail")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.FunctionId).HasColumnName("FunctionID");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.MenuDetail)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuDetail_Function");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuDetail)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_MenuDetail_Menu");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("Notifications", "SysAdmin");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReadDate).HasColumnType("datetime");

                entity.Property(e => e.RejectedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequesterId).HasColumnName("RequesterID");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.HasOne(d => d.Requester)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.RequesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifications_User");
            });

            modelBuilder.Entity<OauthClient>(entity =>
            {
                entity.ToTable("OAuthClient", "SysAdmin");

                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.AllowedOrigin).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<OrgUnit>(entity =>
            {
                entity.ToTable("OrgUnit", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BeginEff).HasColumnType("date");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CostCenterId).HasColumnName("CostCenterID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LastEff).HasColumnType("date");

                entity.Property(e => e.LegalEntityInformationId).HasColumnName("LegalEntityInformationID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrganizationlevelId).HasColumnName("OrganizationlevelID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.SuperiorId).HasColumnName("SuperiorID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.OrgUnit)
                    .HasForeignKey(d => d.CostCenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgUnit_CostCenter");

                entity.HasOne(d => d.LegalEntityInformation)
                    .WithMany(p => p.OrgUnit)
                    .HasForeignKey(d => d.LegalEntityInformationId)
                    .HasConstraintName("FK_OrgUnit_LegalEntityInformation");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_OrgUnit_ParentOrgUnit");

                entity.HasOne(d => d.Superior)
                    .WithMany(p => p.OrgUnit)
                    .HasForeignKey(d => d.SuperiorId)
                    .HasConstraintName("FK__OrgUnit__Employee");
            });

            modelBuilder.Entity<OrgUnitChangeStatus>(entity =>
            {
                entity.ToTable("OrgUnitChangeStatus", "General");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrgUnitTransDetJobTitle>(entity =>
            {
                entity.ToTable("OrgUnitTransDetJobTitle", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BeginEff).HasColumnType("date");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.JobtitleId)
                    .IsRequired()
                    .HasColumnName("JobtitleID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastEff).HasColumnType("date");

                entity.Property(e => e.OrgUnitTransactionDetId).HasColumnName("OrgUnitTransactionDetID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.OrgUnitTransactionDet)
                    .WithMany(p => p.OrgUnitTransDetJobTitle)
                    .HasForeignKey(d => d.OrgUnitTransactionDetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgUnitTransDetJobTitle_OrgUnitTransactionDetail");
            });

            modelBuilder.Entity<OrgUnitTransaction>(entity =>
            {
                entity.ToTable("OrgUnitTransaction", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.BeginEff).HasColumnType("date");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DecisionNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Folio)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastEff).HasColumnType("date");

                entity.Property(e => e.OrgUnitId).HasColumnName("OrgUnitID");

                entity.Property(e => e.ProcessInstance)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestorId)
                    .HasColumnName("RequestorID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionStatusId).HasColumnName("TransactionStatusID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrgUnitTransactionDetail>(entity =>
            {
                entity.ToTable("OrgUnitTransactionDetail", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BeginEff).HasColumnType("date");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CostCenterId).HasColumnName("CostCenterID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LastEff).HasColumnType("date");

                entity.Property(e => e.LegalEntityInformationId).HasColumnName("LegalEntityInformationID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrgUnitId).HasColumnName("OrgUnitID");

                entity.Property(e => e.OrgUnitTransactionId).HasColumnName("OrgUnitTransactionID");

                entity.Property(e => e.OrganizationleveId).HasColumnName("OrganizationleveID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SuperiorId).HasColumnName("SuperiorID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.OrgUnitTransactionDetail)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgUnitTransactionDetail_OrganizationUnitCategory");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.OrgUnitTransactionDetail)
                    .HasForeignKey(d => d.CostCenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgUnitTransactionDetail_CostCenter");

                entity.HasOne(d => d.LegalEntityInformation)
                    .WithMany(p => p.OrgUnitTransactionDetail)
                    .HasForeignKey(d => d.LegalEntityInformationId)
                    .HasConstraintName("FK_OrgUnitTransactionDetail_LegalEntityInformation");

                entity.HasOne(d => d.OrgUnitTransaction)
                    .WithMany(p => p.OrgUnitTransactionDetail)
                    .HasForeignKey(d => d.OrgUnitTransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgUnitTransactionDetail_OrgUnitTransaction");

                entity.HasOne(d => d.Organizationleve)
                    .WithMany(p => p.OrgUnitTransactionDetail)
                    .HasForeignKey(d => d.OrganizationleveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgUnitTransactionDetail_OrganizationLevel");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.OrgUnitTransactionDetail)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgUnitTransactionDetail_OrgUnitChangeStatus");

                entity.HasOne(d => d.Superior)
                    .WithMany(p => p.OrgUnitTransactionDetail)
                    .HasForeignKey(d => d.SuperiorId)
                    .HasConstraintName("FK_OrgUnitTransactionDetail_Employee");
            });

            modelBuilder.Entity<OrganizationLevel>(entity =>
            {
                entity.ToTable("OrganizationLevel", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrganizationListJobtitle>(entity =>
            {
                entity.ToTable("OrganizationListJobtitle", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BeginEff).HasColumnType("date");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");

                entity.Property(e => e.LastEff).HasColumnType("date");

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.OrganizationListJobtitle)
                    .HasForeignKey(d => d.JobTitleId)
                    .HasConstraintName("FK_OrganizationListJobtitle_JobTitle");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.OrganizationListJobtitle)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_OrganizationListJobtitle_OrgUnit");
            });

            modelBuilder.Entity<OrganizationListLocation>(entity =>
            {
                entity.ToTable("OrganizationListLocation", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BeginEff).HasColumnType("date");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.LastEff).HasColumnType("date");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.OrganizationListLocation)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_OrganizationListLocation_Location");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.OrganizationListLocation)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_OrganizationListLocation_OrgUnit");
            });

            modelBuilder.Entity<OrganizationUnitCategory>(entity =>
            {
                entity.ToTable("OrganizationUnitCategory", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrganizationUnitType>(entity =>
            {
                entity.ToTable("OrganizationUnitType", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PasswordAuditTrail>(entity =>
            {
                entity.ToTable("PasswordAuditTrail", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PasswordAuditTrail)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PasswordAuditTrail_User");
            });

            modelBuilder.Entity<PreferenceAuthority>(entity =>
            {
                entity.ToTable("PreferenceAuthority", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.FunctionPreferenceId).HasColumnName("FunctionPreferenceID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.FunctionPreference)
                    .WithMany(p => p.PreferenceAuthority)
                    .HasForeignKey(d => d.FunctionPreferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreferenceAuthority_Function");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PreferenceAuthority)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreferenceAuthority_User");
            });

            modelBuilder.Entity<PreferenceGroup>(entity =>
            {
                entity.ToTable("PreferenceGroup", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province", "Preference");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Province)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Province___Count__7A8729A3");
            });

            modelBuilder.Entity<ProvinceDeprecated>(entity =>
            {
                entity.ToTable("Province_Deprecated", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ProvinceDeprecated)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Province_Country");
            });

            modelBuilder.Entity<ReminderNotification>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("ReminderNotification", "SysAdmin");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationDate).HasColumnType("datetime");

                entity.Property(e => e.OriginatorEmployeeId).HasColumnName("OriginatorEmployeeID");

                entity.Property(e => e.ReadDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiverUserId).HasColumnName("ReceiverUserID");

                entity.Property(e => e.RequestType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.HasOne(d => d.ReceiverUser)
                    .WithMany(p => p.ReminderNotification)
                    .HasForeignKey(d => d.ReceiverUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReminderNotification_User");
            });

            modelBuilder.Entity<RespGroupDetail>(entity =>
            {
                entity.ToTable("RespGroupDetail", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.ResponsibilityGroupId).HasColumnName("ResponsibilityGroupID");

                entity.Property(e => e.ResponsibilityId).HasColumnName("ResponsibilityID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ResponsibilityGroup)
                    .WithMany(p => p.RespGroupDetail)
                    .HasForeignKey(d => d.ResponsibilityGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RespGroupDetail_ResponsibilityGroup");

                entity.HasOne(d => d.Responsibility)
                    .WithMany(p => p.RespGroupDetail)
                    .HasForeignKey(d => d.ResponsibilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RespGroupDetail_Responsibility");
            });

            modelBuilder.Entity<Responsibility>(entity =>
            {
                entity.ToTable("Responsibility", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DefaultBusinessGroupId).HasColumnName("DefaultBusinessGroupID");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Responsibility)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Responsibility_Menu");
            });

            modelBuilder.Entity<ResponsibilityGroup>(entity =>
            {
                entity.ToTable("ResponsibilityGroup", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Siupclass>(entity =>
            {
                entity.ToTable("SIUPClass", "Preference");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AssetFrom).HasColumnType("money");

                entity.Property(e => e.AssetTo).HasColumnType("money");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Siupclass1>(entity =>
            {
                entity.ToTable("SIUPClass", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AssetFrom).HasColumnType("money");

                entity.Property(e => e.AssetTo).HasColumnType("money");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State", "General");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.MinimumSalary).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_State_Country");
            });

            modelBuilder.Entity<TaxDuty>(entity =>
            {
                entity.ToTable("TaxDuty", "Core");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "SysAdmin");

                entity.HasIndex(e => new { e.UserName, e.DelDate })
                    .HasName("UQ1_User")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentBusinessGroupId).HasColumnName("CurrentBusinessGroupID");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.EmployeeListFilterId).HasColumnName("EmployeeListFilterID");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordExpiration).HasColumnType("datetime");

                entity.Property(e => e.PreferenceGroupId).HasColumnName("PreferenceGroupID");

                entity.Property(e => e.ResponsibilityGroupId).HasColumnName("ResponsibilityGroupID");

                entity.Property(e => e.RestrictionGroupId).HasColumnName("RestrictionGroupID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserStatusId).HasColumnName("UserStatusID");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserStatus");
            });

            modelBuilder.Entity<UserForgotPassword>(entity =>
            {
                entity.ToTable("UserForgotPassword", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActiveFrom).HasColumnType("datetime");

                entity.Property(e => e.ActiveTo).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserFunction>(entity =>
            {
                entity.ToTable("UserFunction", "SysAdmin");

                entity.HasIndex(e => new { e.UserId, e.FunctionId, e.DelDate })
                    .HasName("UQ1_UserFunction")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.ExcludedField)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FunctionId).HasColumnName("FunctionID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.UserFunction)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFunction_Function");
            });

            modelBuilder.Entity<UserFunctionAuditTrail>(entity =>
            {
                entity.ToTable("UserFunctionAuditTrail", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.FunctionId).HasColumnName("FunctionID");

                entity.Property(e => e.LastVisit).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.UserFunctionAuditTrail)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FunctionAuditTrail_Function");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFunctionAuditTrail)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FunctionAuditTrail_User");
            });

            modelBuilder.Entity<UserResponsibility>(entity =>
            {
                entity.ToTable("UserResponsibility", "SysAdmin");

                entity.HasIndex(e => new { e.UserId, e.ResponsibilityId, e.DelDate })
                    .HasName("UQ1_UserResponsibility")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.ResponsibilityId).HasColumnName("ResponsibilityID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Responsibility)
                    .WithMany(p => p.UserResponsibility)
                    .HasForeignKey(d => d.ResponsibilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserResponsibiltiy_Responsibility");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserResponsibility)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserResponsibility_User");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("UserStatus", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsersImei>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UsersIMEI", "SysAdmin");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BusinessGroupId).HasColumnName("BusinessGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DelDate).HasColumnType("datetime");

                entity.Property(e => e.Imei)
                    .IsRequired()
                    .HasColumnName("IMEI")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UsersImei)
                    .HasForeignKey<UsersImei>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersIMEI_User");
            });

            modelBuilder.Entity<UsersImeitransaction>(entity =>
            {
                entity.ToTable("UsersIMEITransaction", "SysAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Imei)
                    .IsRequired()
                    .HasColumnName("IMEI")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.UsersImeitransactionDeletedByNavigation)
                    .HasForeignKey(d => d.DeletedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersIMEITransaction_User");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersImeitransactionUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersIMEITransaction_Users");
            });
        }
    }
}
