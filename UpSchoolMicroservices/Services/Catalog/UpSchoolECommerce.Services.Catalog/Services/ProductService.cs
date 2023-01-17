using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchoolECommerce.Services.Catalog.Dtos;
using UpSchoolECommerce.Services.Catalog.Models;
using UpSchoolECommerce.Services.Catalog.Settings;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Services.Catalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
           
            _mapper = mapper;
        }


        public async Task<ResponseDto<ProductDto>> CreateAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(product);
            return ResponseDto<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            var result = await _productCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            else
            {
                return ResponseDto<NoContent>.Fail("Silinecek ürün bulunamadı", 404);
            }
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productCollection.Find(product => true).ToListAsync();
            return ResponseDto<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), 200);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var products = await _productCollection.Find<Product>(x => x.Id == id).FirstOrDefaultAsync();
            if (products == null)
            {
                return ResponseDto<ProductDto>.Fail("Girilen ID ye ait bir ürün bulunamadı",404);
            }
            else
            {
                return ResponseDto<ProductDto>.Success(_mapper.Map<ProductDto>(products), 200);
            }
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(UpdateProductDto updateProductDto)
        {
            var updatedProduct = _mapper.Map<Product>(updateProductDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == updateProductDto.Id, updatedProduct);
            if (result == null)
            {
                return ResponseDto<NoContent>.Fail("Güncellenecek id değeri bulunamadı", 404);
            }
            else
            {
                return ResponseDto<NoContent>.Success(204);
            }
        }
    }
}
