using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopDbContext _context;

        public ProductController
            (
            ShopDbContext context,
            IProductServ

            )
        {
            _context = context;
                }
        public IActionResult Index()
        {
            var result = _context.Product
                .Select(x => new ProductListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Amount = x.Amount
                });
            return View(result);
        }

        public async Task<IActionResult> Delete(Guid id )
        {
            var product = await  _
            return RedirectToAction(nameof(Index), product);
        }
    }
}
