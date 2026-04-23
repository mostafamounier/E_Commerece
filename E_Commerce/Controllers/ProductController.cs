using AutoMapper;
using E_Commerce.Dtos;
using E_Commerce.Errors;
using E_Commerece.Core.Models;
using E_Commerece.Core.Repositories;
using E_Commerece.Core.Specifications;
using E_Commerece.Repository.Repositoriees;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{

    public class ProductController : ApiControllerBase
    {
        private readonly IGenericRepository<Product> genericRepo;
        private readonly IMapper mapper;
        private readonly IGenericRepository<ProductType> productType_Repo;
        private readonly IGenericRepository<ProductBrand> productBrand_Repo;

        public ProductController(IGenericRepository<Product> ProductRepo,IMapper mapper,
            IGenericRepository<ProductType>ProductType_Repo,IGenericRepository<ProductBrand> ProductBrand_Repo)
        {
            this.genericRepo = ProductRepo;
            this.mapper = mapper;
            productType_Repo = ProductType_Repo;
            productBrand_Repo = ProductBrand_Repo;
        }





        [HttpGet]
        public async Task<ActionResult<PagniationResponse<ProductDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            ProductSpecification productSpecification = new ProductSpecification(productSpecParams);
            var products =await genericRepo.GetAllWithSpecAsync(productSpecification);
            var productspeccount = new ProductCountSpec(productSpecParams);
            var count= await genericRepo.GetCountWithSpecFilteration(productspeccount);
            var productsdto = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var productspagniation = new PagniationResponse<ProductDto>(productSpecParams.PageIndex, productSpecParams.PageSize,count, productsdto.ToList());
            return Ok(productspagniation);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            ProductSpecification productSpecification = new ProductSpecification(id);
            var product = await genericRepo.GetByIdWithSpecAsync(productSpecification);
            if (product == null)
                return NotFound(new ApiErrorResponse(404));
            var productdto = mapper.Map<Product, ProductDto>(product);
            return Ok(productdto);
        }

        [HttpGet("ProductTypes")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAllProductTypes()
        {
            var productTypes =await productBrand_Repo.GetAllAsync();

            return Ok(productTypes);
        }

        [HttpGet("ProductBrands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllProductBrands()
        {
            var productBrands = await productType_Repo.GetAllAsync();

            return Ok(productBrands);
        }

    }
}
