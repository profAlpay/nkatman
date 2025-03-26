using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nkatman.Core.DTOs.UpdateDTOs;
using nkatman.Core.DTOs;
using nkatman.Core.Services;
using nkatman.Core.Models;
using Nkatman.API.Filters;
using nkatman.Service.Services;

namespace Nkatman.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        //get user from token 
        int userId = 1;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = _productService.GetAll().ToList();
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(products).ToList();


            var result = new CustomResponseDto<List<ProductDto>>().Success(200, productDto);
            return CreateActionResult(result);

        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            var productDto = _mapper.Map<ProductDto>(product);

            var nesne = new CustomResponseDto<ProductDto>();
            return CreateActionResult(nesne.Success(200, productDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Payment>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
          
            //get user from token
            int PaymentId = 1;
            var product = await _productService.GetByIdAsync(id);
            product.UpdateBy = userId; 

            _productService.ChangeStatus(product);

            return CreateActionResult(new CustomResponseDto<NoContentDto>().Success(204));
        }

        [HttpPost]

        public async Task<IActionResult> Save(ProductDto productDto)
        {
            //get user from token 
            var userId = 1;

            var processedEntity = _mapper.Map<Product>(productDto);

            processedEntity.UpdateBy = userId;

            processedEntity.CreatedBy = userId;

            var Product = await _productService.AddAsync(processedEntity);

            var productResponseDto = _mapper.Map<ProductDto>(Product);

            var response = new CustomResponseDto<ProductDto>().Success(201, productDto);


            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            var userId = 1;

            var currentProduct = await _productService.GetByIdAsync(productDto.Id);

            currentProduct.UpdateBy = userId;
            currentProduct.Name = productDto.Name;
            currentProduct.UnitPrice = productDto.UnitPrice;


            _productService.Update(currentProduct);

            return CreateActionResult(new CustomResponseDto<ProductUpdateDto>().Success(204));

        }
    }
}
