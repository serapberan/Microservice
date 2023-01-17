using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchoolECommerce.Services.Catalog.Dtos;
using UpSchoolECommerce.Services.Catalog.Services;
using UpSchoolECommerce.Shared.ControllerBases;

namespace UpSchoolECommerce.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var response = await _productService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> ActionResult(CreateProductDto createProductDto)
        {
            var response = await _productService.CreateAsync(createProductDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
        {
            var response = await _productService.UpdateAsync(updateProductDto);
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _productService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }

    }
}
