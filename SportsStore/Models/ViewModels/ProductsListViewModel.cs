namespace SportsStore.Models.ViewModels;

public class ProductsListViewModel
{
	public PagingInfo PagingInfo { get; set; } = new ();

	public IEnumerable<Product> Products { get; set; }
		= Enumerable.Empty<Product>();
}