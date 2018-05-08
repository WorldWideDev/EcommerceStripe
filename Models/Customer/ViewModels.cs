using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ECommerce.Models
{
    public class CustomerIndexModels
    {
        public NewCustomer NewCustomer {get;set;}
        public List<Customer> Customers {get;set;}
    }
    public class NewCustomer
    {
        [Required]
        [Display(Name="Customer Name")]
        public string Name{get;set;}
    }
}