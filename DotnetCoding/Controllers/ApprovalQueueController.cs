using System;
using DotnetCoding.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalQueueController : ControllerBase
    {
        private readonly IApprovalQueueService _approvalQueueService;

        public ApprovalQueueController(IApprovalQueueService approvalQueueService)
        {
            _approvalQueueService = approvalQueueService;
        }

        /// <summary>
        /// Get the list of items in the approval queue.
        /// </summary>
        /// <returns>A list of items in the approval queue.</returns>
        [HttpGet]
        public async Task<IActionResult> GetApprovalQueue()
        {
            try
            {
                var approvalQueue = await _approvalQueueService.GetApprovalQueue();
                return Ok(approvalQueue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Approve an item in the approval queue by its ID.
        /// </summary>
        /// <param name="itemId">The ID of the item to approve.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [HttpPost("approve/{itemId}")]
        public async Task<IActionResult> ApproveItem(int itemId)
        {
            try
            {
                await _approvalQueueService.ApproveItem(itemId);
                return Ok("Item approved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Reject an item in the approval queue by its ID with a reason.
        /// </summary>
        /// <param name="itemId">The ID of the item to reject.</param>
        /// <param name="reason">The reason for rejecting the item.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [HttpPost("reject/{itemId}")]
        public async Task<IActionResult> RejectItem(int itemId, [FromBody] string reason)
        {
            try
            {
                await _approvalQueueService.RejectItem(itemId, reason);
                return Ok("Item rejected successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

