using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SportsStore.Models;

namespace SportsStore.Migrations;

[DbContext(typeof(StoreDbContext))]
[Migration("20220904175421_Initial")]
public class Initial : Migration
{
	protected override void BuildTargetModel(ModelBuilder modelBuilder)
	{
		modelBuilder
			.HasAnnotation("ProductVersion", "7.0.1")
			.HasAnnotation("Relational:MaxIdentifierLength", 128)
			.UseIdentityColumns();

		modelBuilder.Entity<Product>(entity =>
		{
			entity
				.Property<long?>("ProductID")
				.HasColumnType("bigint")
				.UseIdentityColumn()
				.ValueGeneratedOnAdd();

			entity
				.Property<string>("Category")
				.HasColumnType("nvarchar(max)")
				.IsRequired();

			entity
				.Property<string>("Description")
				.HasColumnType("nvarchar(max)")
				.IsRequired();

			entity
				.Property<string>("Name")
				.HasColumnType("nvarchar(max)")
				.IsRequired();

			entity
				.Property<decimal>("Price")
				.HasColumnType("decimal(8,2)");

			entity.HasKey("ProductID");
			entity.ToTable("Products");
		});
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(name: "Products");
	}

	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Products",
			columns: table => new
			{
				ProductID = table.Column<long>(type: "bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Products", x => x.ProductID);
			});
	}
}