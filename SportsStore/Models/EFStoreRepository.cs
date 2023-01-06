namespace SportsStore.Models;

public class EFStoreRepository : IStoreRepository
{
	readonly StoreDbContext _context;

	public EFStoreRepository(StoreDbContext context) => _context = context;

	public IQueryable<Product> Products => _context.Products;

	public void CreateProduct(Product product)
	{
		throw new NotImplementedException();
	}

	public void DeleteProduct(Product product)
	{
		throw new NotImplementedException();
	}

	public void SaveProduct(Product product)
	{
		throw new NotImplementedException();
	}
}