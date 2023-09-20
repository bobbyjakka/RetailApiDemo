using Dapper;
using DotnetCoding.Core.Constants;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductDetails>, IProductRepository
    {
        public ProductRepository(DbContextClass dbContext) : base(dbContext)
        {

        }

        public async Task<ProductDetails> CreateProduct(ProductDetails product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _dbContext.Set<ProductDetails>().Add(product);

            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task UpdateProduct(ProductDetails product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var existingProduct = await GetById(product.Id);

            if (existingProduct == null)
            {
                throw new ApplicationException($"Product with ID {product.Id} not found.");
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.ProductDescription = product.ProductDescription;
            existingProduct.ProductPrice = product.ProductPrice;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductStatus(int productId)
        {
            var existingProduct = await GetById(productId);

            if (existingProduct == null)
            {
                throw new ApplicationException($"Product with ID {productId} not found.");
            }

            existingProduct.ProductStatus = ProductStatus.Active;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await GetById(id);

            if (product != null)
            {
                _dbContext.Set<ProductDetails>().Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ProductDetails> GetById(int id)
        {
            return await _dbContext.Set<ProductDetails>().FindAsync(id);
        }

    }
}
