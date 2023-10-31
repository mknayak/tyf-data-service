using Microsoft.EntityFrameworkCore;

namespace tyf.data.service.DbModels;

public partial class TyfDataContext : DbContext
{
    public TyfDataContext()
    {
    }

    public TyfDataContext(DbContextOptions<TyfDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessAccount> AccessAccounts { get; set; }

    public virtual DbSet<AccessAccountRole> AccessAccountRoles { get; set; }

    public virtual DbSet<AccessGroup> AccessGroups { get; set; }

    public virtual DbSet<AccessGroupAccount> AccessGroupAccounts { get; set; }

    public virtual DbSet<AccessGroupRole> AccessGroupRoles { get; set; }

    public virtual DbSet<AccessRole> AccessRoles { get; set; }

    public virtual DbSet<AccessToken> AccessTokens { get; set; }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<DataSchema> DataSchemas { get; set; }

    public virtual DbSet<SchemaDatum> SchemaData { get; set; }

    public virtual DbSet<SchemaField> SchemaFields { get; set; }

    public virtual DbSet<SchemaFieldType> SchemaFieldTypes { get; set; }

    public virtual DbSet<SchemaInstance> SchemaInstances { get; set; }

    public virtual DbSet<SettingItem> SettingItems { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessAccount>(entity =>
        {
            entity.HasKey(e => e.AccessAccountId).HasName("AccessAccounts_pkey");

            entity.ToTable("AccessAccounts", "master");

            entity.Property(e => e.AccessAccountId).ValueGeneratedNever();
            entity.Property(e => e.AccessAccountEmail).HasMaxLength(150);
            entity.Property(e => e.AccessAccountName).HasMaxLength(150);
            entity.Property(e => e.AccessAccountPasswordhash).HasMaxLength(100);
            entity.Property(e => e.AccessAccountSalt).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<AccessAccountRole>(entity =>
        {
            entity.HasKey(e => e.AccessAccountRoleId).HasName("AccessAccountRoles_pkey");

            entity.ToTable("AccessAccountRoles", "master");

            entity.Property(e => e.AccessAccountRoleId).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.AccessAccount).WithMany(p => p.AccessAccountRoles)
                .HasForeignKey(d => d.AccessAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_accessaccountrole_accessaccount");

            entity.HasOne(d => d.AccessRole).WithMany(p => p.AccessAccountRoles)
                .HasForeignKey(d => d.AccessRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_accessaccountrole_accessrole");
        });

        modelBuilder.Entity<AccessGroup>(entity =>
        {
            entity.HasKey(e => e.AccessGroupId).HasName("AccessGroup_pkey");

            entity.ToTable("AccessGroup", "master");

            entity.HasIndex(e => new { e.AccessGroupName, e.AccessGroupNamespace }, "uk_accessgroup").IsUnique();

            entity.Property(e => e.AccessGroupId).ValueGeneratedNever();
            entity.Property(e => e.AccessGroupName).HasMaxLength(100);
            entity.Property(e => e.AccessGroupNamespace).HasMaxLength(250);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<AccessGroupAccount>(entity =>
        {
            entity.HasKey(e => e.AccessGroupAccountId).HasName("AccessGroupAccount_pkey");

            entity.ToTable("AccessGroupAccounts", "master");

            entity.Property(e => e.AccessGroupAccountId).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.AccessAccount).WithMany(p => p.AccessGroupAccounts)
                .HasForeignKey(d => d.AccessAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_accessgroupaccount_accessaccount");

            entity.HasOne(d => d.AccessGroup).WithMany(p => p.AccessGroupAccounts)
                .HasForeignKey(d => d.AccessGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_accessgroupaccount_accessgroup");
        });

        modelBuilder.Entity<AccessGroupRole>(entity =>
        {
            entity.HasKey(e => e.AccessGroupRoleId).HasName("AccessGroupRoles_pkey");

            entity.ToTable("AccessGroupRoles", "master");

            entity.Property(e => e.AccessGroupRoleId).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.AccessGroup).WithMany(p => p.AccessGroupRoles)
                .HasForeignKey(d => d.AccessGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_accessgrouprole_accessgroup");

            entity.HasOne(d => d.AccessRole).WithMany(p => p.AccessGroupRoles)
                .HasForeignKey(d => d.AccessRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_accessgrouprole_accessrole");
        });

        modelBuilder.Entity<AccessRole>(entity =>
        {
            entity.HasKey(e => e.AccessRoleId).HasName("AccessRole_pkey");

            entity.ToTable("AccessRole", "master");

            entity.Property(e => e.AccessRoleId).ValueGeneratedNever();
            entity.Property(e => e.AccessRoleName).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<AccessToken>(entity =>
        {
            entity.HasKey(e => new { e.AccessTokenId, e.RefreshToken, e.AccessTokenExpiry }).HasName("AccessTokens_pkey");

            entity.ToTable("AccessTokens", "data");

            entity.Property(e => e.RefreshToken).HasMaxLength(500);
            entity.Property(e => e.AccessTokenExpiry).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AccessScope).HasMaxLength(150);
            entity.Property(e => e.AccessToken1)
                .HasMaxLength(500)
                .HasColumnName("AccessToken");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.AccessAccount).WithMany(p => p.AccessTokens)
                .HasForeignKey(d => d.AccessAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AccessToken_accessaccount");
        });

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.HasKey(e => e.ConfigurationKey).HasName("Configurations_pkey");

            entity.ToTable("Configurations", "master");

            entity.Property(e => e.ConfigurationKey).HasMaxLength(150);
            entity.Property(e => e.ConfigurationType).HasDefaultValueSql("0");
            entity.Property(e => e.ConfigurationValue).HasMaxLength(500);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<DataSchema>(entity =>
        {
            entity.HasKey(e => e.SchemaId).HasName("DataSchema_pkey");

            entity.ToTable("DataSchema", "master");

            entity.HasIndex(e => new { e.SchemaNameSpace, e.SchemaName }, "DataSchema_SchemaNameSpace_SchemaName_key").IsUnique();

            entity.Property(e => e.SchemaId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.IsPublic)
                .IsRequired()
                .HasDefaultValueSql("true");
            entity.Property(e => e.SchemaName).HasMaxLength(50);
            entity.Property(e => e.SchemaNameSpace).HasMaxLength(150);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<SchemaDatum>(entity =>
        {
            entity.HasKey(e => e.SchemaDataId).HasName("SchemaData_pkey");

            entity.ToTable("SchemaData", "data");

            entity.Property(e => e.SchemaDataId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.SchemeDataValue).HasMaxLength(1500);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.SchemaField).WithMany(p => p.SchemaData)
                .HasForeignKey(d => d.SchemaFieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SchemaFieldMapping");

            entity.HasOne(d => d.SchemaInstance).WithMany(p => p.SchemaData)
                .HasForeignKey(d => d.SchemaInstanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SchemaInstanceMapping");
        });

        modelBuilder.Entity<SchemaField>(entity =>
        {
            entity.HasKey(e => e.SchemaFieldId).HasName("SchemaFields_pkey");

            entity.ToTable("SchemaFields", "master");

            entity.HasIndex(e => new { e.SchemaFieldName, e.SchemaId }, "SchemaFieldsUniqueKey").IsUnique();

            entity.Property(e => e.SchemaFieldId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.SchemaFieldName).HasMaxLength(50);
            entity.Property(e => e.SchemaFieldRegex).HasMaxLength(150);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.SchemaFieldTypeKeyNavigation).WithMany(p => p.SchemaFields)
                .HasForeignKey(d => d.SchemaFieldTypeKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FieldTypeMapping");

            entity.HasOne(d => d.Schema).WithMany(p => p.SchemaFields)
                .HasForeignKey(d => d.SchemaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SchemaIdMapping");
        });

        modelBuilder.Entity<SchemaFieldType>(entity =>
        {
            entity.HasKey(e => e.FieldTypeId).HasName("SchemaFieldTypes_pkey");

            entity.ToTable("SchemaFieldTypes", "master");

            entity.Property(e => e.FieldTypeId)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(100L, null, null, null, null, null);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.FieldTypeDefaultValue).HasMaxLength(50);
            entity.Property(e => e.FieldTypeName).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<SchemaInstance>(entity =>
        {
            entity.HasKey(e => e.SchemaInstanceId).HasName("SchemaInstance_pkey");

            entity.ToTable("SchemaInstance", "data");

            entity.Property(e => e.SchemaInstanceId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.SchemaInstanceName).HasMaxLength(50);
            entity.Property(e => e.SchemaInstanceNamespace).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Schema).WithMany(p => p.SchemaInstances)
                .HasForeignKey(d => d.SchemaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SchemaIdMapping");
        });

        modelBuilder.Entity<SettingItem>(entity =>
        {
            entity.HasKey(e => e.SettingItemId).HasName("SettingItems_pkey");

            entity.ToTable("SettingItems", "data");

            entity.Property(e => e.SettingItemId).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.SettingItemKey).HasMaxLength(150);
            entity.Property(e => e.SettingItemNamespace).HasMaxLength(250);
            entity.Property(e => e.SettingItemValue).HasMaxLength(500);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
