using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Files;
using Shop.Models.Product;
using System;
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
                CreatedAt = model.CreatedAt,
                Files = model.Files,
                ExistingFilePaths = model.ExistingFilePaths
                .Select(x => new ExistingFilePathDto
                {
                    PhotoId = x.PhotoId,
                    FilePath = x.FilePath,
                    ProductId = x.ProductId
                }).ToArray()
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
            if (product == null)
            {
                return NotFound();
            }

            var photos = await _context.ExistingFilePaths.Where(x => x.ProductId == id).Select(y => new ExistingFilePathViewModel
            {
                FilePath = y.FilePath,
                PhotoId = y.Id
            })
                .ToArrayAsync();
            var model = new ProductViewModel();

            model.Id = product.Id;
            model.Description = product.Description;
            model.Name = product.Name;
            model.Amount = product.Amount;
            model.Price = product.Price;
            model.ModifiedAt = product.ModifiedAt;
            model.CreatedAt = product.CreatedAt;
            model.ExistingFilePaths.AddRange(photos);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductViewModel model)
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

            var result = await _productService.Update(dto);

            if(result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), model);
        }
    }
}
