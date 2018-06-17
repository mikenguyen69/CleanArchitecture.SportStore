using CASportStore.Core.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace CASportStore.Core.Entities
{
    public class Product : BaseEntity
    {        
        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter a description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Please specify a category")]
        public string Category { get; set; }
    }
}
