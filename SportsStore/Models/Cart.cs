﻿namespace SportsStore.Models;

public class Cart
{
	public List<CartLine> Lines { get; set; } = new();

	public virtual void AddItem(Product product, int quantity)
	{
		var line = Lines.FirstOrDefault(
			x => x.Product.ProductID == product.ProductID);

		if (line is null)
		{
			Lines.Add(new() { Product = product, Quantity = quantity });
		}
		else
		{
			line.Quantity += quantity;
		}
	}

	public virtual void Clear() => Lines.Clear();

	public decimal ComputeTotalValue() =>
		Lines.Sum(x => x.Product.Price * x.Quantity);

	public virtual void RemoveLine(Product product) =>
		Lines.RemoveAll(x => x.Product.ProductID == product.ProductID);
}