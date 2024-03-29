using API.DTO;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;

using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{

    public class ProductsController : BaseController
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
        [Cached(600)]
        [HttpGet]
        public async Task< ActionResult<Pagination<ProductToReturnDTO>>> GetProducts(
            [FromQuery]ProductSpecParams productparams)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(productparams);
            var countspec = new ProductsWithFilterForCountSpecification(productparams);
            var total = await _productrepo.CountAsync(countspec);
            var result = await _productrepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>,
                IReadOnlyList<ProductToReturnDTO>>(result);
            
            return Ok(new Pagination<ProductToReturnDTO>(productparams.PageIndex,productparams.PageSize,total,data));
        }


        [HttpGet("{id}")]
        //used to document the api in swagger 
        //the things swagger can't docment as the return of the 404 no found that we specify
        //not really needed to write it to every method but if you want to tell more info so people use the api knows
        //what types of error they might have 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        [Cached(600)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(id);
            var product = await _productrepo.GetEntityWithSpec(spec);
            if(product == null) return NotFound(new APIResponse(404));
            return _mapper.Map<Product,ProductToReturnDTO>(product);
        }
        [Cached(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productbrandrepo.ListAllAsync());
        }
        [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTyperepo.ListAllAsync());
        }
        
    }
}