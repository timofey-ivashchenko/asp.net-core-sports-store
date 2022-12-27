using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SportsStore.Models;

namespace SportsStore.Migrations;

[DbContext(typeof(StoreDbContext))]
[Migration("20221227133157_Orders")]
public class Orders : Migration
{
	protected override void BuildTargetModel(ModelBuilder modelBuilder)
	{
		modelBuilder
			.HasAnnotation("ProductVersion", "7.0.1")
			.HasAnnotation("Relational:MaxIdentifierLength", 128)
			.UseIdentityColumns();

		modelBuilder.Entity<CartLine>(entity =>
		{
			entity
				.Property<int>("CartLineID")
				.HasColumnType("int")
				.UseIdentityColumn()
				.ValueGeneratedOnAdd();

			entity
				.Property<int?>("OrderID")
				.HasColumnType("int");

			entity
				.Property<long>("ProductID")
				.HasColumnType("bigint");

			entity
				.Property<int>("Quantity")
				.HasColumnType("int");

			entity.HasKey("CartLineID");
			entity.HasIndex("OrderID");
			entity.HasIndex("ProductID");
			entity.ToTable("CartLine");
		});

		modelBuilder.Entity<Order>(entity =>
		{
			entity
				.Property<int>("OrderID")
				.HasColumnType("int")
				.UseIdentityColumn()
				.ValueGeneratedOnAdd();

			entity
				.Property<string>("City")
				.HasColumnType("nvarchar(max)")
				.IsRequired();

			entity
				.Property<string>("Country")
				.HasColumnType("nvarchar(max)")
				.IsRequired();

			entity
				.Property<bool>("GiftWrap")
				.HasColumnType("bit");

			entity
				.Property<string>("Line1")
				.HasColumnType("nvarchar(max)")
				.IsRequired();

			entity
				.Property<string>("Line2")
				.HasColumnType("nvarchar(max)");

			entity
				.Property<string>("Line3")
				.HasColumnType("nvarchar(max)");

			entity
				.Property<string>("Name")
				.HasColumnType("nvarchar(max)")
				.IsRequired();

			entity
				.Property<string>("State")
				.HasColumnType("nvarchar(max)")
				.IsRequired();

			entity
				.Property<string>("Zip")
				.HasColumnType("nvarchar(max)");

			entity.HasKey("OrderID");
			entity.ToTable("Orders");
		});

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

		modelBuilder.Entity<CartLine>(entity =>
		{
			entity
				.HasOne<Order>()
				.WithMany("Lines")
				.HasForeignKey("OrderID");

			entity
				.HasOne<Product>("Product")
				.WithMany()
				.HasForeignKey("ProductID")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			entity.Navigation("Product");
		});

		modelBuilder.Entity<Order>(entity =>
		{
			entity.Navigation("Lines");
		});
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(name: "CartLine");
		migrationBuilder.DropTable(name: "Orders");
	}

	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Orders",
			columns: table => new
			{
				OrderID = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				City = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
				GiftWrap = table.Column<bool>(type: "bit", nullable: false),
				Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
				Line3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
				Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
				State = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Zip = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Orders", x => x.OrderID);
			});

		migrationBuilder.CreateTable(
			name: "CartLine",
			columns: table => new
			{
				CartLineID = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				ProductID = table.Column<long>(type: "bigint", nullable: false),
				Quantity = table.Column<int>(type: "int", nullable: false),
				OrderID = table.Column<int>(type: "int", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_CartLine", x => x.CartLineID);

				table.ForeignKey(
					name: "FK_CartLine_Orders_OrderID",
					column: x => x.OrderID,
					principalTable: "Orders",
					principalColumn: "OrderID");

				table.ForeignKey(
					name: "FK_CartLine_Products_ProductID",
					column: x => x.ProductID,
					principalTable: "Products",
					principalColumn: "ProductID",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "IX_CartLine_OrderID",
			table: "CartLine",
			column: "OrderID");

		migrationBuilder.CreateIndex(
			name: "IX_CartLine_ProductID",
			table: "CartLine",
			column: "ProductID");
	}
}