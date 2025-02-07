using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nkatman.Core.DTOs;
using nkatman.Core.Services;

namespace Nkatman.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : CustomBaseController
    {
        private readonly IcustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomersController(IcustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
           var customers =  _customerService.GetAll();
            var dtos = _mapper.Map<IEnumerable<CustomerDto>>(customers).ToList();
          

            var result = new CustomResponseDto<List<CustomerDto>>().Success(200, dtos);
            return CreateActionResult(result);

        }
    }
}
