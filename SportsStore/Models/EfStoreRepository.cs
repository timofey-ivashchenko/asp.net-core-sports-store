namespace SportsStore.Models;

public class EfStoreRepository : IStoreRepository
{
	readonly StoreDbContext _context;

	public EfStoreRepository(StoreDbContext context) =>
		_context = context;

	public IQueryable<Product> Products => _context.Products;
}