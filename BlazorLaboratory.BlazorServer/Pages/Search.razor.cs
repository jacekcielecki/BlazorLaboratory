namespace BlazorLaboratory.BlazorServer.Pages;

public partial class Search
{
    class Product
    {
        public required string Name { get; init; }
        public string? Code { get; init; }
        public double Price { get; init; }
    }

    private Product _selectedProduct;
    private readonly IEnumerable<Product> _products = new List<Product>
    {
        new Product { Name = "Apple", Code = "X01", Price = 5 },
        new Product { Name = "Orange", Code = "X01", Price = 1 },
        new Product { Name = "Red Apple", Code = "X02", Price = 50 },
        new Product { Name = "Green Apple", Code = "X02", Price = 8 },
        new Product { Name = "Big Orange", Code = "X03", Price = 15 }
    };

    private async Task<IEnumerable<Product>> SearchProducts(string searchText)
    {
        await Task.Delay(1000);

        if (string.IsNullOrWhiteSpace(searchText))
            return _products;

        return _products.Where(product => 
            product.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            (product.Code != null && 
             product.Code.Contains(searchText, StringComparison.OrdinalIgnoreCase)));
    }
}
