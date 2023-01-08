using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Migrations;

// ReSharper disable UnusedMember.Global
[DbContext(typeof(StoreDbContext))]
public class StoreDbContextModelSnapshot : ModelSnapshot
{
	protected override void BuildModel(ModelBuilder modelBuilder)
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
				.Property<bool>("Shipped")
				.HasColumnType("bit");

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
				.Property<long>("ProductID")
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
}