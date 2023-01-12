using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Migrations.AppIdentityDb;

// ReSharper disable once UnusedMember.Global
[DbContext(typeof(AppIdentityDbContext))]
public class AppIdentityDbContextModelSnapshot : ModelSnapshot
{
	protected override void BuildModel(ModelBuilder modelBuilder)
	{
		modelBuilder
			.HasAnnotation("ProductVersion", "7.0.2")
			.HasAnnotation("Relational:MaxIdentifierLength", 128)
			.UseIdentityColumns();

		modelBuilder.Entity<IdentityRole>(entity =>
		{
			entity
				.Property<string>("Id")
				.HasColumnType("nvarchar(450)");

			entity
				.Property<string>("ConcurrencyStamp")
				.HasColumnType("nvarchar(max)")
				.IsConcurrencyToken();

			entity
				.Property<string>("Name")
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);

			entity
				.Property<string>("NormalizedName")
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);

			entity.HasKey("Id");

			entity
				.HasIndex("NormalizedName")
				.HasDatabaseName("RoleNameIndex")
				.HasFilter("[NormalizedName] IS NOT NULL")
				.IsUnique();

			entity.ToTable("AspNetRoles", (string?) null);
		});

		modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
		{
			entity
				.Property<int>("Id")
				.HasColumnType("int")
				.UseIdentityColumn()
				.ValueGeneratedOnAdd();

			entity
				.Property<string>("ClaimType")
				.HasColumnType("nvarchar(max)");

			entity
				.Property<string>("ClaimValue")
				.HasColumnType("nvarchar(max)");

			entity
				.Property<string>("RoleId")
				.HasColumnType("nvarchar(450)")
				.IsRequired();

			entity.HasKey("Id");

			entity.HasIndex("RoleId");

			entity.ToTable("AspNetRoleClaims", (string?) null);
		});

		modelBuilder.Entity<IdentityUser>(entity =>
		{
			entity
				.Property<string>("Id")
				.HasColumnType("nvarchar(450)");

			entity
				.Property<int>("AccessFailedCount")
				.HasColumnType("int");

			entity
				.Property<string>("ConcurrencyStamp")
				.HasColumnType("nvarchar(max)")
				.IsConcurrencyToken();

			entity
				.Property<string>("Email")
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);

			entity
				.Property<bool>("EmailConfirmed")
				.HasColumnType("bit");

			entity
				.Property<bool>("LockoutEnabled")
				.HasColumnType("bit");

			entity
				.Property<DateTimeOffset?>("LockoutEnd")
				.HasColumnType("datetimeoffset");

			entity
				.Property<string>("NormalizedEmail")
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);

			entity
				.Property<string>("NormalizedUserName")
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);

			entity
				.Property<string>("PasswordHash")
				.HasColumnType("nvarchar(max)");

			entity
				.Property<string>("PhoneNumber")
				.HasColumnType("nvarchar(max)");

			entity
				.Property<bool>("PhoneNumberConfirmed")
				.HasColumnType("bit");

			entity
				.Property<string>("SecurityStamp")
				.HasColumnType("nvarchar(max)");

			entity
				.Property<bool>("TwoFactorEnabled")
				.HasColumnType("bit");

			entity
				.Property<string>("UserName")
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);

			entity.HasKey("Id");

			entity
				.HasIndex("NormalizedEmail")
				.HasDatabaseName("EmailIndex");

			entity
				.HasIndex("NormalizedUserName")
				.HasDatabaseName("UserNameIndex")
				.HasFilter("[NormalizedUserName] IS NOT NULL")
				.IsUnique();

			entity.ToTable("AspNetUsers", (string?) null);
		});

		modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
		{
			entity
				.Property<int>("Id")
				.HasColumnType("int")
				.UseIdentityColumn()
				.ValueGeneratedOnAdd();

			entity
				.Property<string>("ClaimType")
				.HasColumnType("nvarchar(max)");

			entity
				.Property<string>("ClaimValue")
				.HasColumnType("nvarchar(max)");

			entity
				.Property<string>("UserId")
				.HasColumnType("nvarchar(450)")
				.IsRequired();

			entity.HasKey("Id");

			entity.HasIndex("UserId");

			entity.ToTable("AspNetUserClaims", (string?) null);
		});

		modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
		{
			entity
				.Property<string>("LoginProvider")
				.HasColumnType("nvarchar(450)");

			entity
				.Property<string>("ProviderKey")
				.HasColumnType("nvarchar(450)");

			entity
				.Property<string>("ProviderDisplayName")
				.HasColumnType("nvarchar(max)");

			entity
				.Property<string>("UserId")
				.HasColumnType("nvarchar(450)")
				.IsRequired();

			entity.HasKey("LoginProvider", "ProviderKey");

			entity.HasIndex("UserId");

			entity.ToTable("AspNetUserLogins", (string?) null);
		});

		modelBuilder.Entity<IdentityUserRole<string>>(entity =>
		{
			entity
				.Property<string>("UserId")
				.HasColumnType("nvarchar(450)");

			entity
				.Property<string>("RoleId")
				.HasColumnType("nvarchar(450)");

			entity.HasKey("UserId", "RoleId");

			entity.HasIndex("RoleId");

			entity.ToTable("AspNetUserRoles", (string?) null);
		});

		modelBuilder.Entity<IdentityUserToken<string>>(entity =>
		{
			entity
				.Property<string>("UserId")
				.HasColumnType("nvarchar(450)");

			entity
				.Property<string>("LoginProvider")
				.HasColumnType("nvarchar(450)");

			entity
				.Property<string>("Name")
				.HasColumnType("nvarchar(450)");

			entity
				.Property<string>("Value")
				.HasColumnType("nvarchar(max)");

			entity.HasKey("UserId", "LoginProvider", "Name");

			entity.ToTable("AspNetUserTokens", (string?) null);
		});

		modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
		{
			entity
				.HasOne<IdentityRole>()
				.WithMany()
				.HasForeignKey("RoleId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		});

		modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
		{
			entity
				.HasOne<IdentityUser>()
				.WithMany()
				.HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		});

		modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
		{
			entity
				.HasOne<IdentityUser>()
				.WithMany()
				.HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		});

		modelBuilder.Entity<IdentityUserRole<string>>(entity =>
		{
			entity
				.HasOne<IdentityRole>()
				.WithMany()
				.HasForeignKey("RoleId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			entity
				.HasOne<IdentityUser>()
				.WithMany()
				.HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		});

		modelBuilder.Entity<IdentityUserToken<string>>(entity =>
		{
			entity
				.HasOne<IdentityUser>()
				.WithMany()
				.HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		});
	}
}