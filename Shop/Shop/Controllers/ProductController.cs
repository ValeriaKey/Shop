using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
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
        private readonly IProductService _productService;

        public ProductController
            (
            ShopDbContext context,
            IProductService productService

            )
        {
            _context = context;
            _productService = productService;
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
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.Delete(id);
            if (product == null)
            {
                RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), product);
        }

        [HttpGet]
        public IActionResult Add()
        {

            ProductViewModel model = new ProductViewModel();

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductListViewModel model)
        {
            var dto = new ProductDto()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                Amount = model.Amount,
                Price = model.Price,
                ModifiedAt = model.ModifiedAt,
                CreatedAt = model.CreatedAt
            };

            var result = await _productService.Add(dto);
            if (result == null) {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.Edit(id);

            if(product == null)
            {
                return NotFound();
            }

            var model = new ProductViewModel();

            var dto = new ProductDto()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                Amount = model.Amount,
                Price = model.Price,
                ModifiedAt = model.ModifiedAt,
                CreatedAt = model.CreatedAt
            };

            return View(model);
        }
    }
}
