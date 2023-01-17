using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchoolECommerce.Services.Catalog.Dtos;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Services.Catalog.Services
{
    public interface IProductService
    {
        Task<ResponseDto<List<ProductDto>>> GetAllAsync();
        Task<ResponseDto<ProductDto>> GetByIdAsync(string id);
        Task<ResponseDto<ProductDto>> CreateAsync(CreateProductDto createProductDto);
        Task<ResponseDto<NoContent>> UpdateAsync(UpdateProductDto updateProductDto);
        Task<ResponseDto<NoContent>> DeleteAsync(string id);
    }
}
