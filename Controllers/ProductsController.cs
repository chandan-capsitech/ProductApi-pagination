using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;
using ProductApi.DTOs;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ApiResponse<PaginationResponse<Product>>> GetProducts(
        [FromQuery] int page, [FromQuery] int pageSize)
    {
        var res = new ApiResponse<PaginationResponse<Product>>();
        try
        {
            var data = await _productService.GetProductsAsync(page, pageSize);
            res.Result = data;
            res.Status = true;
            res.Message = "Fetched Successfully";
        }
        catch (Exception ex)
        {
            res.Status = false;
            res.Message = ex.Message;
        }
        return res;
    }

    [HttpPost]
    public async Task<ApiResponse<Product>> CreateProduct(ProductDto dto)
    {
        var res = new ApiResponse<Product>();
        try
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Category = dto.Category,
            };
            await _productService.CreateProductAsync(product);
            res.Status = true;
            res.Message = "Product created successfully";
            res.Result = product;
        }
        catch (Exception ex)
        {
            res.Status = false;
            res.Message = ex.Message;
        }
        return res;
    }
}