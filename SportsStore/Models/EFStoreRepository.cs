namespace SportsStore.Models;

public class EFStoreRepository : IStoreRepository
{
	readonly StoreDbContext _context;

	public EFStoreRepository(StoreDbContext context) => _context = context;

	public IQueryable<Product> Products => _context.Products;
}