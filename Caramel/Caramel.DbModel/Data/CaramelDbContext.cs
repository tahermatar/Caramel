using Caramel.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Caramel.Data
{
    public partial class CaramelDbContext : DbContext
    {
        private bool IgnoreFilter { get; set; }
        public CaramelDbContext()
        {
        }

        public CaramelDbContext(DbContextOptions<CaramelDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Resturant> Resturants { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Userrole> Userroles { get; set; }
        public virtual DbSet<Rolepermission> Rolepermissions { get; set; }
        public virtual DbSet<Userpermissionview> Userpermissionviews { get; set; }
        public virtual DbSet<ViewOrderViewModel> ViewOrderViewModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=DESKTOP-K722RLO\\SQLEXPRESS;Database=CaramelDb;Trusted_Connection=True;");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.ResturantId)
                      .HasColumnType("int")
                      .HasDefaultValueSql("((''))");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ExtraInformation).HasMaxLength(255);

                entity.Property(e => e.Road).HasMaxLength(50);

            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.ConfirmPassword).HasMaxLength(255);

                entity.Property(e => e.Image).HasColumnType("nvarchar(255)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ConfirmationLink)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.EmailConfirmed).HasColumnType("int");
                entity.Property(e => e.RoleId).HasColumnType("int");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Archived).HasColumnType("int");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Address");
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.ToTable("Meal");

                entity.Property(e => e.Component).HasMaxLength(255);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.Image).HasColumnType("nvarchar(255)");


                entity.Property(e => e.IsAvailable).HasDefaultValueSql("((1))");

                entity.Property(e => e.MealName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Archived).HasColumnType("int");

                entity.HasOne(d => d.Resturant)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.ResturantId)
                    .HasConstraintName("FK_Meal_Resturant ");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => new { e.MId, e.Name })
                    .HasName("PK__module__1B8549BE29AAF231");

                entity.ToTable("module");

                entity.HasIndex(e => e.MId, "m_Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.MId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("m_Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Archived)
                .HasColumnType("smallint")
                .HasDefaultValueSql("('0')");

                entity.Property(e => e.CreatedUtc)
                    .HasPrecision(0)
                    .HasColumnName("CreatedUTC")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastUpdatedUtc)
                    .HasPrecision(0)
                    .HasColumnName("LastUpdatedUTC")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Quantity)
                   .HasColumnType("int")
                   .HasDefaultValueSql("0");

                entity.Property(e => e.TotalPrice)
                      .HasColumnType("float");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfExecution)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfOrder)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Archived)
                    .HasColumnType("int");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Meal");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.PId)
                    .HasName("PK__permissi__82E37F491206D202");

                entity.ToTable("permission");

                entity.HasIndex(e => e.ModuleId, "ModuleId_PrmessionModuleId_idx");

                entity.HasIndex(e => e.PId, "p_Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PId).HasColumnName("p_Id");

                entity.Property(e => e.Archived)
                .HasColumnType("smallint")
                .HasDefaultValueSql("('0')");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedUtc)
                    .HasPrecision(0)
                    .HasColumnName("CreatedUTC")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastUpdatedUtc)
                    .HasPrecision(0)
                    .HasColumnName("LastUpdatedUTC")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Permissions)
                    .HasPrincipalKey(p => p.MId)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ModuleId_PrmessionModuleId");
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("Rate");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Review).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Archived).HasColumnType("int");

                entity.HasOne(d => d.Resturant)
                      .WithMany(p => p.Rates)
                      .HasForeignKey(d => d.ResturantId)
                      .HasConstraintName("FK_Resturant_Rate");
            });

            modelBuilder.Entity<Resturant>(entity =>
            {
                entity.ToTable("Resturant");

                entity.Property(e => e.Address).HasColumnType("int");

                entity.Property(e => e.ConfirmationLink)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.EmailConfirmed).HasColumnType("int");

                entity.Property(e => e.RoleId).HasColumnType("int");

                entity.Property(e => e.Bio).HasColumnType("nvarchar(255)");

                entity.Property(e => e.ConfirmPassword)
                    .IsRequired()
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Image).HasColumnType("nvarchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Phone).HasColumnType("nvarchar(50)");

                entity.Property(e => e.TotalRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.WorkingTime).HasColumnType("nvarchar(255)");

                entity.Property(e => e.IsChef).HasColumnType("int");

                entity.Property(e => e.CreatedBy).HasColumnType("int");

                entity.Property(e => e.UpdatedBy).HasColumnType("int");

                entity.Property(e => e.Archived).HasColumnType("int");

                entity.Property(e => e.MealId).HasColumnType("int");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Resturants)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Resturant _Order1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RId)
                    .HasName("PK__role__C4751F1FB073D012");

                entity.ToTable("role");

                entity.HasIndex(e => e.RId, "r_Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.RId).HasColumnName("r_Id");

                entity.Property(e => e.Archived)
                .HasColumnType("smallint")
                .HasDefaultValueSql("('0')");

                entity.Property(e => e.CreatedUtc)
                    .HasPrecision(0)
                    .HasColumnName("CreatedUTC")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedUtc)
                    .HasPrecision(0)
                    .HasColumnName("LastUpdatedUTC")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Rolepermission>(entity =>
            {
                entity.HasKey(e => e.RpId)
                    .HasName("PK__roleperm__CBB7296A3F329AAD");

                entity.ToTable("rolepermission");

                entity.HasIndex(e => e.PermissionId, "PermissionId_RolePermission_idx");

                entity.HasIndex(e => e.RoleId, "RoleId_RolePermission_idx");

                entity.Property(e => e.RpId).HasColumnName("rp_Id");

                entity.Property(e => e.Archived)
                .HasColumnType("smallint")
                .HasDefaultValueSql("('0')");

                entity.Property(e => e.CreatedUtc)
                    .HasPrecision(0)
                    .HasColumnName("CreatedUTC")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedUtc)
                    .HasPrecision(0)
                    .HasColumnName("LastUpdatedUTC")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Rolepermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PermissionId_UserPermissionId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Rolepermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RoleId_RolePermission");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                  .HasColumnType("int")
                  .IsUnicode(true);

                entity.Property(e => e.UserName)
                    .HasColumnType("nvarchar(50)")
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnType("nvarchar(255)")
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired();

                entity.Property(e => e.ConfirmPassword)
                   .HasColumnType("nvarchar(255)")
                   .IsRequired();


                entity.Property(e => e.IsSuperAdmin)
                  .HasColumnType("int")
                  .IsRequired();

                entity.Property(e => e.CreatedBy)
                  .HasColumnType("int")
                  .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                  .HasColumnType("int")
                  .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Archived)
                 .HasColumnType("int")
                 .IsRequired();


            });

            modelBuilder.Entity<Userpermissionview>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("userpermissionview");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleKey)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewOrderViewModel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewOrderViewModel");

                entity.Property(e => e.OrderId)
                  .HasColumnType("int"); 

                entity.Property(e => e.CustomerId)
                  .HasColumnType("int");

                entity.Property(e => e.CustomerName)
                 .HasColumnType("nvarchar(255)")
                 .IsRequired()
                 .HasMaxLength(255);

                entity.Property(e => e.MealName)
                 .HasColumnType("nvarchar(255)")
                .IsRequired()
                .HasMaxLength(255);

                entity.Property(e => e.RestorantId)
                  .HasColumnType("int");

                entity.Property(e => e.ResturantName)
                 .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("float");

                entity.Property(e => e.DateOfOrder)
                   .HasColumnType("datetime");

                entity.Property(e => e.DateOfExcution)
                   .HasColumnType("datetime");

                entity.Property(e => e.Status)
                   .HasColumnType("nvarchar(255)");

            });

            modelBuilder.Entity<Userrole>(entity =>
            {
                entity.ToTable("userrole");

                entity.HasIndex(e => e.RoleId, "UserRole_RoleId_idx");

                entity.HasIndex(e => e.UserId, "UserRole_UserId_idx");

                entity.Property(e => e.Archived)
                .HasColumnType("smallint")
                .HasDefaultValueSql("('0')");

                entity.Property(e => e.CreatedUtc)
                    .HasPrecision(0)
                    .HasColumnName("CreatedUTC")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedUtc)
                    .HasPrecision(0)
                    .HasColumnName("LastUpdatedUTC")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userroles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRole_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRole_UserId");
            });

            modelBuilder.Entity<User>().HasQueryFilter(a => !a.Archived || IgnoreFilter);
            modelBuilder.Entity<Customer>().HasQueryFilter(a => !a.Archived || IgnoreFilter);
            modelBuilder.Entity<Resturant>().HasQueryFilter(a => !a.Archived || IgnoreFilter);
            modelBuilder.Entity<Meal>().HasQueryFilter(a => !a.Archived || IgnoreFilter);
            modelBuilder.Entity<Order>().HasQueryFilter(a => !a.Archived || IgnoreFilter);
            

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
