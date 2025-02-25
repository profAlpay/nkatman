using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using nkatman.Core.DTOs;
using nkatman.Core.DTOs.UpdateDTOs;
using nkatman.Core.Models;
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
           var customers =  _customerService.GetAll().ToList();
            var dtos = _mapper.Map<IEnumerable<CustomerDto>>(customers).ToList();
          

            var result = new CustomResponseDto<List<CustomerDto>>().Success(200, dtos);
            return CreateActionResult(result);

        }

        [HttpPost]

        public async Task<IActionResult> Save(CustomerDto customerDto)
        {
            //get user from token 
            var userId = 1;

            var processedEntity = _mapper.Map<Customer>(customerDto);

            processedEntity.UpdateBy = userId;

            processedEntity.CreatedBy = userId;

            var Customer = await _customerService.AddAsync(processedEntity);

            var customerResponseDto = _mapper.Map<CustomerDto>(Customer);

            var response = new CustomResponseDto<CustomerDto>().Success(201, customerResponseDto);
            

            return  CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerUpdateDto customerDto)
        {
            var userId = 1;

            var currentCustomer = await _customerService.GetByIdAsync(customerDto.Id);

            currentCustomer.UpdateBy = userId;
            currentCustomer.Name = customerDto.Name;

            _customerService.Update(currentCustomer);

            return CreateActionResult(new CustomResponseDto<CustomerUpdateDto>().Success(204));

        }
    }
}
