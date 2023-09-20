using System;
using DotnetCoding.Core.Constants;

namespace DotnetCoding.Core.Models
{
    using Newtonsoft.Json;

    public class ApprovalQueue
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("requestType")]
        public RequestType RequestType { get; set; }

        [JsonProperty("requestReason")]
        public string? RequestReason { get; set; }

        [JsonProperty("requestDate")]
        public DateTime RequestDate { get; set; }
    }

}

