using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ECommerce.Models;
using System.Linq;
using Stripe;

namespace ECommerce.Controllers
{
    public class OrderController : Controller
    {
        private MainContext _context {get;set;}
        public OrderController(MainContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View(InitializeOrderIndex());
        }
        [HttpPost]
        public IActionResult Create(OrderIndexModels models)
        {
            NewOrder nOrder = models.NewOrder;
            Product oProduct = _context.products.Where(p => p.product_id == nOrder.ProductId).SingleOrDefault();
            if(oProduct.quantity < nOrder.Quantity)
                ModelState.AddModelError("NewOrder.Quantity", "Not enough for your order!");
            
            if(ModelState.IsValid)
            {
                Order theOrder = new Order
                {
                    customer_id = nOrder.CustomerId,
                    product_id = nOrder.ProductId,
                    quantity = nOrder.Quantity,
                    created_at = DateTime.Now
                };
                _context.orders.Add(theOrder);

                oProduct.quantity -= nOrder.Quantity;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index", InitializeOrderIndex(nOrder));
        }
        public IActionResult Delete(int id)
        {
            _context.orders.Remove(
                _context.orders.Where(o => o.order_id == id).FirstOrDefault()
            );
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Charge(string  stripeEmail, string stripeToken)
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions {
            Email = stripeEmail,
            SourceToken = stripeToken
            });

            var charge = charges.Create(new StripeChargeCreateOptions {
            Amount = 500,
            Description = "Sample Charge",
            Currency = "usd",
            CustomerId = customer.Id
            });

            return View();
        }

        public OrderIndexModels InitializeOrderIndex(NewOrder order = null)
        {
            NewOrder orderToSend = order != null ? order : new NewOrder(); 
            return new OrderIndexModels
            {
                NewOrder = orderToSend,
                Orders = _context.orders.ToList(),
                Products = _context.products.ToList(),
                Customers = _context.customers.ToList()
            };
        }
    }
}
