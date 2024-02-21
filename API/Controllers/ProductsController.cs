using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productrepo;
        private readonly IGenericRepository<ProductBrand> _productbrandrepo;
        private readonly IGenericRepository<ProductType> _productTyperepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo,
         IGenericRepository<ProductBrand> ProductBrandRepo, 
         IGenericRepository<ProductType> ProductTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productrepo=ProductRepo;
            _productbrandrepo = ProductBrandRepo;
            _productTyperepo = ProductTypeRepo;
            
        }
        [HttpGet]
        public async Task< ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpec();
            var result = await _productrepo.ListAsync(spec);
            
            return Ok(_mapper.Map<IReadOnlyList<Product>,
                IReadOnlyList<ProductToReturnDTO>>(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(id);
            var product = await _productrepo.GetEntityWithSpec(spec);
            return _mapper.Map<Product,ProductToReturnDTO>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productbrandrepo.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTyperepo.ListAllAsync());
        }
        
    }
}