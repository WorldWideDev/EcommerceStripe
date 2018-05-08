using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ECommerce.Models;
using System.Linq;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private MainContext _context {get;set;}
        public ProductController(MainContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View(InitializeProductIndex());
        }
        [HttpPost]
        public IActionResult Create(ProductIndexModels models)
        {
            NewProduct nProduct = models.NewProduct;
            if(ModelState.IsValid)
            {
                Product theProduct = new Product
                {
                    name = nProduct.Name,
                    image_link = nProduct.Image,
                    description = nProduct.Description,
                    quantity = nProduct.Quantity
                };
                _context.products.Add(theProduct);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index", InitializeProductIndex(nProduct));
        }
        public IActionResult Delete(int id)
        {
            _context.products.Remove(
                _context.products.Where(p => p.product_id == id).FirstOrDefault()
            );
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ProductIndexModels InitializeProductIndex(NewProduct Product = null)
        {
            NewProduct ProductToSend = Product != null ? Product : new NewProduct(); 
            return new ProductIndexModels
            {
                NewProduct = ProductToSend,
                Products = _context.products.ToList()
            };
        }
    }
}
