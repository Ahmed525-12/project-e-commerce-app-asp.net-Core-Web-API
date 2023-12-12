using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Core.Specifications;
using Talabat.PL.DTOs;
using Talabat.PL.Errors;
using Talabat.PL.Helper;

namespace Talabat.PL.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IMapper mapper,
                                    IUnitOfWork UnitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = UnitOfWork;
        }

        //Get All Products
        [CachedAttribute(300)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProducts([FromQuery] ProductSpecParams Params)
        {
            var Spec = new ProductWithBrandAndTypeSpec(Params);
            var Products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(Spec);
            var MappedProduct = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
            var Count = Spec.Count;
            return Ok(new Pagination<ProductToReturnDto>(Params.PageSize, Params.index, MappedProduct, Count));
        }

        // Get Product Using Id
        [HttpGet("id")]
        [ProducesResponseType(typeof(ProductToReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpec(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(Spec);
            if (product is null)
                return NotFound(new ApiResponse(404));
            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(MappedProduct);
        }

        // Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }

        // Get All Types
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAllTypes()
        {
            var type = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(type);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            try
            {
                var Spec = new ProductWithBrandAndTypeSpec(id);

                var products = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(Spec);

                if (products == null)
                {
                    return NotFound(new ApiResponse(404, "Product not found"));
                }

                await _unitOfWork.Repository<Product>().DeleteAsync(products);
                await _unitOfWork.CompleteAsync();  // Assuming that Complete() is the method that saves changes

                return Ok(new ApiResponse(200, "Product deleted successfully"));
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting

                // Return a more detailed error response
                return StatusCode(500, new ApiResponse(500, "Internal server error"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductToReturnDto>> CreateProducts(Product productDto)
        {
            try
            {
                // Map the DTO to the Products entity

                // Add the product to the repository
                var addedProduct = _unitOfWork.Repository<Product>().AddAsync(productDto);

                // Check if the product was successfully added
                if (addedProduct == null)
                {
                    return BadRequest(new ApiResponse(400, "Failed to add the product."));
                }

                // Map the created product to DTO
                var mapped = _mapper.Map<Product, ProductToReturnDto>(productDto);
                // Return the mapped DTO
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                // Return a more detailed error response
                return StatusCode(500, new ApiResponse(500, "Internal server error"));
            }
        }
    }
}