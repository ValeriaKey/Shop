using Shop.Core.Domain;
using Shop.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface IProductService
    {
        Task<Product> Delete(Guid id);

        Task<Product> Add(ProductDto dto);

        Task<Product> Edit(Guid id);

        Task<Product> Update(ProductDto dto);
    }
}
