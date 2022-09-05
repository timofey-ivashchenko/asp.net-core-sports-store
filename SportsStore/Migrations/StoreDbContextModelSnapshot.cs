using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Migrations;

[DbContext(typeof(StoreDbContext))]
// ReSharper disable UnusedMember.Global
class StoreDbContextModelSnapshot : ModelSnapshot
{
	protected override void BuildModel(ModelBuilder modelBuilder) =>
		modelBuilder
			.HasAnnotation("ProductVersion", "6.0.8")
			.HasAnnotation("Relational:MaxIdentifierLength", 128)
			.UseIdentityColumns()
			.Entity<Product>(builder =>
			{
				builder
					.Property<long?>(nameof(Product.Id))
					.HasColumnType("bigint")
					.UseIdentityColumn()
					.ValueGeneratedOnAdd();

				builder
					.Property<string>(nameof(Product.Name))
					.HasColumnType("nvarchar(max)")
					.IsRequired();

				builder
					.Property<string>(nameof(Product.Description))
					.HasColumnType("nvarchar(max)")
					.IsRequired();

				builder
					.Property<string>(nameof(Product.Category))
					.HasColumnType("nvarchar(max)")
					.IsRequired();

				builder
					.Property<decimal>(nameof(Product.Price))
					.HasColumnType("decimal(8,2)")
					.IsRequired();

				builder
					.ToTable("Products")
					.HasKey(nameof(Product.Id));
			});
}