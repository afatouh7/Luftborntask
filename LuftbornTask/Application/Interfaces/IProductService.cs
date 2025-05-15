using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> CreateAsync(ProductDto productDto);
        Task UpdateAsync(ProductDto productDto);
        Task DeleteAsync(Guid id);
    }
}
