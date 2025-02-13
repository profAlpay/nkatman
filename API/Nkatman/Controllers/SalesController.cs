using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nkatman.Core.DTOs.UpdateDTOs;
using nkatman.Core.DTOs;
using nkatman.Core.Services;
using nkatman.Core.Models;

namespace Nkatman.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : CustomBaseController
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;
        public SalesController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var sales = _saleService.GetAll();
            var dtos = _mapper.Map<IEnumerable<SaleDto>>(sales).ToList();


            var result = new CustomResponseDto<List<SaleDto>>().Success(200, dtos);
            return CreateActionResult(result);

        }

        [HttpPost]

        public async Task<IActionResult> Save(SaleDto saleDto)
        {
            //get user from token 
            var userId = 1;

            var processedEntity = _mapper.Map<Sale>(saleDto);

            processedEntity.UpdateBy = userId;

            processedEntity.CreatedBy = userId;

            var Sale = await _saleService.AddAsync(processedEntity);

            var saleResponseDto = _mapper.Map<SaleDto>(Sale);

            var response = new CustomResponseDto<SaleDto>().Success(201, saleDto);


            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(SaleUpdateDto saleDto)
        {
            var userId = 1;

            var currentSale = await _saleService.GetByIdAsync(saleDto.Id);

            currentSale.UpdateBy = userId;
            currentSale.CustomerId = saleDto.CustomerId;
            currentSale.ProductId = saleDto.ProductId;
            currentSale.Quantity = saleDto.Quantity;
            currentSale.UnitPrice = saleDto.UnitPrice;
            currentSale.TotalPrice = saleDto.TotalPrice;

            _saleService.Update(currentSale);

            return CreateActionResult(new CustomResponseDto<SaleUpdateDto>().Success(204));

        }
    }
}
