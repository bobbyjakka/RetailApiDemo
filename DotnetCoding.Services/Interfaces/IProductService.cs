using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;
//using DotnetCoding.Core.Models.Request;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetails>> GetAllProducts();

        Task<ProductDetails> CreateProduct(ProductDetails request);

        Task UpdateProduct(ProductDetails request);

        Task DeleteProduct(int productId);
    }
}
