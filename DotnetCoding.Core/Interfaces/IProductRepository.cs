using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductDetails>
    {
        Task<ProductDetails> CreateProduct(ProductDetails product);

        Task UpdateProduct(ProductDetails product);

        Task UpdateProductStatus(int productId);

        Task DeleteProduct(int id);

        Task<ProductDetails> GetById(int id);
    }
}
