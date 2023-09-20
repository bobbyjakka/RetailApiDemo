using System;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ApprovalQueueRepository : GenericRepository<ApprovalQueue>, IApprovalQueueRepository
    {
        public ApprovalQueueRepository(DbContextClass context) : base(context)
        {
        }

        public async Task<IEnumerable<ApprovalQueue>> GetAllAsync()
        {
            return await _dbContext.ApprovalQueue.OrderByDescending(entry => entry.RequestDate).ToListAsync();
        }

        public async Task AddAsync(ApprovalQueue entry)
        {
            _dbContext.ApprovalQueue.Add(entry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int itemId)
        {
            var entry = await _dbContext.ApprovalQueue.FirstOrDefaultAsync(item => item.ProductId == itemId);

            if (entry != null)
            {
                _dbContext.ApprovalQueue.Remove(entry);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ApprovalQueue> GetById(int id)
        {
            return await _dbContext.Set<ApprovalQueue>().FindAsync(id);
        }
    }
}

