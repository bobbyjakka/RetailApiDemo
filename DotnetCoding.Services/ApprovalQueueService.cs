using System;
using DotnetCoding.Core.Constants;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class ApprovalQueueService : IApprovalQueueService
    {
        public IUnitOfWork _unitOfWork;

        public ApprovalQueueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ApprovalQueue>> GetApprovalQueue()
        {
            return await _unitOfWork.ApprovalQueues.GetAllAsync();
        }

        public async Task AddToQueue(ProductDetails product, RequestType requestType)
        {
            var entry = new ApprovalQueue
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                RequestType = requestType,
                RequestReason = requestType == RequestType.Delete ? "Delete" : "Price Change",
                RequestDate = DateTime.UtcNow
            };

            await _unitOfWork.ApprovalQueues.AddAsync(entry);
        }

        public async Task ApproveItem(int itemId)
        {
            var approvalQueue = await _unitOfWork.ApprovalQueues.GetById(itemId);

            if (approvalQueue.RequestType == RequestType.Delete)
            {
                await _unitOfWork.ApprovalQueues.RemoveAsync(itemId);
                await _unitOfWork.Products.DeleteProduct(approvalQueue.ProductId);
            }
            else
            {
                await _unitOfWork.ApprovalQueues.RemoveAsync(itemId);
                await _unitOfWork.Products.UpdateProductStatus(approvalQueue.ProductId);
            }
        }

        public async Task RejectItem(int itemId, string reason)
        {
            await _unitOfWork.ApprovalQueues.RemoveAsync(itemId);
        }

    }

}

