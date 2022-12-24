﻿using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models;

public class StoreDbContext : DbContext
{
	public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) {}

	public DbSet<Order> Orders => Set<Order>();

	public DbSet<Product> Products => Set<Product>();
}