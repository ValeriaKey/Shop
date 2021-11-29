using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            :base(options) { }

        public DbSet<Product> Product { get; set; }

        public DbSet<ExistingFilePath> ExistingFilePaths { get; set; }
    }
}
