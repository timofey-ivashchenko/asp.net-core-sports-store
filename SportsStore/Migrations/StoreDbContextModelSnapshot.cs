using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SportsStore.Models;

#nullable disable

namespace SportsStore.Migrations;

[DbContext(typeof(StoreDbContext))]
class StoreDbContextModelSnapshot : ModelSnapshot
{
	protected override void BuildModel(ModelBuilder modelBuilder)
	{
#pragma warning disable 612, 618

		modelBuilder
			.HasAnnotation("ProductVersion", "6.0.8")
			.HasAnnotation("Relational:MaxIdentifierLength", "128")
			.UseIdentityColumns()
			.Entity<Product>(builder =>
			{
				builder
					.Property<long?>("Id")
					.HasColumnType("bigint")
					.UseIdentityColumn()
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

#pragma warning restore 612, 618
	}
}