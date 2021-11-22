using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
    public class ProductServices : IProductService
    {
        private readonly ShopDbContext _context;

        public ProductServices
            (
            ShopDbContext context
            )
        {
            _context = context;
        }
        public async Task<Product>Delete(Guid id)
        {
            var productId = await _context.Product
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Product.Remove(productId);
            await _context.SaveChangesAsync();

            return productId;

        }

        public async Task<Product> Add(ProductDto dto)
        {
            var domain = new Product()
            {
                Id = dto.Id,
                Description = dto.Description,
                Name = dto.Name,
                Amount = dto.Amount,
                Price = dto.Price,
                ModifiedAt = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            await _context.Product.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<Product> Edit(Guid id)
        {
            var result = await _context.Product
                .FirstOrDefaultAsync(x => x.Id == id);
            var dto = new ProductDto();
            var domain = new Product()
            {
                Id = dto.Id,
                Description = dto.Description,
                Name = dto.Name,
                Amount = dto.Amount,
                Price = dto.Price,
                ModifiedAt = DateTime.Now,
                CreatedAt = dto.CreatedAt,
            };

            return domain;
        }
    }
}
