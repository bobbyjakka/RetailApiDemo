using DotnetCoding.Core.Constants;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IApprovalQueueService _approvalQueueService;


        public ProductService(IUnitOfWork unitOfWork
            , IApprovalQueueService approvalQueueService
            )
        {
            _approvalQueueService = approvalQueueService;
        }

        public async Task<IEnumerable<ProductDetails>> GetAllProducts()
        {
            var productDetailsList = await _unitOfWork.Products.GetAll();
            return productDetailsList;
        }

        public async Task<ProductDetails> CreateProduct(ProductDetails request)
        {
            if (request.ProductPrice > 10000)
            {
                throw new ApplicationException("Product price cannot exceed $10,000.");
            }


            var createdProduct = await _unitOfWork.Products.CreateProduct(request);

            if (request.ProductPrice > 5000)
            {
                await _approvalQueueService.AddToQueue(createdProduct, RequestType.Create);
            }

            return createdProduct;
        }


        public async Task UpdateProduct(ProductDetails request)
        {
            var existingProduct = await _unitOfWork.Products.GetById(request.Id);

            if (existingProduct == null)
            {
                throw new ApplicationException($"Product with ID {request.Id} not found.");
            }

            if (request.ProductPrice > existingProduct.ProductPrice * 1.5m)
            {
                await _approvalQueueService.AddToQueue(existingProduct, RequestType.Update);
            }

            await _unitOfWork.Products.UpdateProduct(request);
        }

        public async Task DeleteProduct(int productId)
        {
            var existingProduct = await _unitOfWork.Products.GetById(productId);

            if (existingProduct == null)
            {
                throw new ApplicationException($"Product with ID {productId} not found.");
            }

            await _approvalQueueService.AddToQueue(existingProduct, RequestType.Delete);
        }

    }
}
