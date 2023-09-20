using System;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IApprovalQueueRepository : IGenericRepository<ApprovalQueue>
    {
        Task<IEnumerable<ApprovalQueue>> GetAllAsync();
        Task AddAsync(ApprovalQueue entry);
        Task RemoveAsync(int itemId);
        Task<ApprovalQueue> GetById(int id);
    }
}

