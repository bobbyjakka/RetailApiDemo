

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotnetCoding.Core.Constants;
//using DotnetCoding.Core.Models.Request;
using Newtonsoft.Json;

namespace DotnetCoding.Core.Models
{
    public class ProductDetails
    {
        [JsonProperty("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonProperty("productName")]
        [Required(ErrorMessage = "Product name is required.")]
        public string ProductName { get; set; }

        [JsonProperty("ProductDescription")]
        public string ProductDescription { get; set; }

        [JsonProperty("productPrice")]
        [Required(ErrorMessage = "Product price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than 0.")]
        public decimal ProductPrice { get; set; }

        [JsonProperty("ProductStatus")]
        public ProductStatus ProductStatus { get; set; }
    }
}
