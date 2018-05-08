using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ECommerce.Models;
using System.Linq;

namespace ECommerce.Controllers
{
    public class CustomerController : Controller
    {
        private MainContext _context {get;set;}
        public CustomerController(MainContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View(InitializeCustomerIndex());
        }
        [HttpPost]
        public IActionResult Create(CustomerIndexModels models)
        {
            NewCustomer nCustomer = models.NewCustomer;
            if(_context.customers.Where(c => c.name == nCustomer.Name).SingleOrDefault() != null)
                ModelState.AddModelError("Name", "That Customer already exists");
            if(ModelState.IsValid)
            {
                Customer theCustomer = new Customer
                {
                    name = nCustomer.Name,
                    created_at = DateTime.Now
                };
                _context.customers.Add(theCustomer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index", InitializeCustomerIndex(nCustomer));
        }
        public IActionResult Delete(int id)
        {
            _context.customers.Remove(
                _context.customers.Where(c => c.customer_id == id).FirstOrDefault()
            );
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public CustomerIndexModels InitializeCustomerIndex(NewCustomer customer = null)
        {
            NewCustomer customerToSend = customer != null ? customer : new NewCustomer(); 
            return new CustomerIndexModels
            {
                NewCustomer = customerToSend,
                Customers = _context.customers.ToList()
            };
        }
    }
}
