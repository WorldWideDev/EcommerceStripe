using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Customer
    {
        [Key]
        public int customer_id {get;set;}
        public string name {get;set;}
        public DateTime created_at {get;set;}
        public List<Order> Purchases {get;set;}
    }

    public class Product
    {
        [Key]
        public int product_id {get;set;}
        public string name {get;set;}
        public string image_link {get;set;}
        public string description {get;set;}
        public int quantity {get;set;}
        public List<Order> Orders {get;set;}
    }
    public class Order
    {
        [Key]
        public int order_id {get;set;}
        public int customer_id {get;set;}
        public int product_id {get;set;}
        public int quantity {get;set;}
        public DateTime created_at {get;set;}
        public Product Item {get;set;}
        public Customer Recipient {get;set;}
    }

        
}