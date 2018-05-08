using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ECommerce.Models
{
    public class OrderIndexModels
    {
        public NewOrder NewOrder {get;set;}
        public List<Order> Orders {get;set;}
        public List<Customer> Customers {get;set;}
        public List<Product> Products {get;set;}
    }
    public class NewOrder
    {
        [Display(Name="Customer")]
        public int CustomerId {get;set;}

        [Display(Name="Product")]
        public int ProductId {get;set;}

        [Required]
        [Min(1)]
        [MaxOrder(10)]
        [Display(Name="Quantity")]
        public int Quantity{get;set;}
    }
}