using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SportsStore.Models;

namespace SportsStore.Migrations.AppIdentityDb;

// ReSharper disable once UnusedMember.Global
[DbContext(typeof(AppIdentityDbContext))]
[Migration("20230110231339_Initial")]
public class Initial : Migration
{
	protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(name: "AspNetRoleClaims");
		migrationBuilder.DropTable(name: "AspNetUserClaims");
		migrationBuilder.DropTable(name: "AspNetUserLogins");
		migrationBuilder.DropTable(name: "AspNetUserRoles");
		migrationBuilder.DropTable(name: "AspNetUserTokens");
		migrationBuilder.DropTable(name: "AspNetRoles");
		migrationBuilder.DropTable(name: "AspNetUsers");
	}

	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "AspNetRoles",
			columns: table => new
			{
				Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
				Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey(
					name: "PK_AspNetRoles",
					columns: x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUsers",
			columns: table => new
			{
				Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
				UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
				PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
				SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
				PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
				PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
				TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
				LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
				LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
				AccessFailedCount = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey(
					name: "PK_AspNetUsers",
					columns: x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "AspNetRoleClaims",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
				ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey(
					name: "PK_AspNetRoleClaims",
					columns: x => x.Id);

				table.ForeignKey(
					name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserClaims",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
				ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey(
					name: "PK_AspNetUserClaims",
					columns: x => x.Id);

				table.ForeignKey(
					name: "FK_AspNetUserClaims_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserLogins",
			columns: table => new
			{
				LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
				ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
				ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
				UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey(
					name: "PK_AspNetUserLogins",
					columns: x => new { x.LoginProvider, x.ProviderKey });

				table.ForeignKey(
					name: "FK_AspNetUserLogins_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserRoles",
			columns: table => new
			{
				UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
				RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey(
					name: "PK_AspNetUserRoles",
					columns: x => new { x.UserId, x.RoleId });

				table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);

				table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserTokens",
			columns: table => new
			{
				UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
				LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
				Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
				Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey(
					name: "PK_AspNetUserTokens",
					columns: x => new { x.UserId, x.LoginProvider, x.Name });

				table.ForeignKey(
					name: "FK_AspNetUserTokens_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "IX_AspNetRoleClaims_RoleId",
			table: "AspNetRoleClaims",
			column: "RoleId");

		migrationBuilder.CreateIndex(
			name: "RoleNameIndex",
			table: "AspNetRoles",
			column: "NormalizedName",
			unique: true,
			filter: "[NormalizedName] IS NOT NULL");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserClaims_UserId",
			table: "AspNetUserClaims",
			column: "UserId");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserLogins_UserId",
			table: "AspNetUserLogins",
			column: "UserId");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserRoles_RoleId",
			table: "AspNetUserRoles",
			column: "RoleId");

		migrationBuilder.CreateIndex(
			name: "EmailIndex",
			table: "AspNetUsers",
			column: "NormalizedEmail");

		migrationBuilder.CreateIndex(
			name: "UserNameIndex",
			table: "AspNetUsers",
			column: "NormalizedUserName",
			unique: true,
			filter: "[NormalizedUserName] IS NOT NULL");
	}
}