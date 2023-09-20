using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DotnetCoding.Core.Models.Request
{
    public class ProductSearchCriteria
    {
        public string ProductName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}

