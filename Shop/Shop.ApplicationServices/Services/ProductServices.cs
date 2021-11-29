using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
    public class ProductServices : IProductService
    {
        private readonly ShopDbContext _context;
        private readonly IHostingEnvironment _env;
        public ProductServices
            (
            ShopDbContext context,
            IHostingEnvironment env
            )
        {
            _context = context;
            _env = env;
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
            Product product = new Product();

                product.Id = dto.Id;
                product.Description = dto.Description;
                product.Name = dto.Name;
                product.Amount = dto.Amount;
                product.Price = dto.Price;
                product.ModifiedAt = DateTime.Now; 
                product.CreatedAt = DateTime.Now;
                ProcessUploadedFile(dto, product);

            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> Edit(Guid id)
        {
            var result = await _context.Product
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
        
        public async Task<Product> Update(ProductDto dto)
        {
            Product product = new Product();

            product.Id = dto.Id;
            product.Description = dto.Description;
            product.Name = dto.Name;
            product.Amount = dto.Amount;
            product.Price = dto.Price;
            product.ModifiedAt = dto.ModifiedAt;
            product.CreatedAt = dto.CreatedAt;

            _context.Product.Update(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public string ProcessUploadedFile(ProductDto dto, Product product)
        {
            string uniqueFileName = null;

            if (dto.Files !=null && dto.Files.Count > 0)
            {

                foreach (var photo in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "multipleFileUpload");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);

                        ExistingFilePath paths = new ExistingFilePath
                        {
                            Id = Guid.NewGuid(),
                            FilePath = uniqueFileName,
                            ProductId = product.Id
                        };

                        _context.ExistingFilePaths.Add(paths);
                    }
                }

            }
            return uniqueFileName;
        }
    }
}
