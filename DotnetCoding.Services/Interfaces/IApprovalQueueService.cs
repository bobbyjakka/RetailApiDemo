using System;
using DotnetCoding.Core.Constants;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Services.Interfaces
{
    public interface IApprovalQueueService
    {
        Task<IEnumerable<ApprovalQueue>> GetApprovalQueue();
        Task AddToQueue(ProductDetails product, RequestType requestType);
        Task ApproveItem(int itemId);
        Task RejectItem(int itemId, string reason);
    }
}

