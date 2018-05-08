using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ECommerce.Models
{
    public class ProductIndexModels
    {
        public NewProduct NewProduct {get;set;}
        public List<Product> Products {get;set;}
    }
    public class NewProduct
    {
        [Required]
        [Display(Name="Product Name")]
        public string Name{get;set;}

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name="Image (url)")]
        public string Image{get;set;}

        [Required]
        [Display(Name="Product Description")]
        public string Description{get;set;}

        [Required]
        [Min(1)]
        [Display(Name="Initial Quantity")]
        public int Quantity{get;set;}
    }
}