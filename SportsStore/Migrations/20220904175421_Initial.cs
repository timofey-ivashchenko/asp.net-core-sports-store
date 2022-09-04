using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SportsStore.Models;

namespace SportsStore.Migrations;

[DbContext(typeof(StoreDbContext))]
[Migration("20220904175421_Initial")]
public class Initial : Migration
{
	protected override void BuildTargetModel(ModelBuilder modelBuilder) =>
		modelBuilder
			.HasAnnotation("ProductVersion", "6.0.8")
			.HasAnnotation("Relational:MaxIdentifierLength", "128")
			.UseIdentityColumns(1L, 1)
			.Entity<Product>(builder =>
			{
				builder
					.Property<long?>("Id")
					.HasColumnType("bigint")
					.UseIdentityColumn(1L, 1)
					.ValueGeneratedOnAdd();

				builder
					.Property<string>("Name")
					.HasColumnType("nvarchar(max)")
					.IsRequired();

				builder
					.Property<string>("Description")
					.HasColumnType("nvarchar(max)")
					.IsRequired();

				builder
					.Property<string>("Category")
					.HasColumnType("nvarchar(max)")
					.IsRequired();

				builder
					.Property<decimal>("Price")
					.HasColumnType("decimal(8,2)")
					.IsRequired();

				builder.ToTable("Products").HasKey("Id");
			});

	protected override void Down(MigrationBuilder migrationBuilder) =>
		migrationBuilder.DropTable(name: "Products");

	protected override void Up(MigrationBuilder migrationBuilder) =>
		migrationBuilder.CreateTable(
			name: "Products",
			columns: table => new
			{
				Id = table
					.Column<long>(type: "bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Name = table.Column<string>(
					type: "nvarchar(max)", nullable: false),
				Description = table.Column<string>(
					type: "nvarchar(max)", nullable: false),
				Category = table.Column<string>(
					type: "nvarchar(max)", nullable: false),
				Price = table.Column<decimal>(
					type: "decimal(8,2)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Products", x => x.Id);
			});
}