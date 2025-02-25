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
    public class PaymentsController : CustomBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var payments = _paymentService.GetAll().ToList();
            var dtos = _mapper.Map<IEnumerable<PaymentDto>>(payments).ToList();


            var result = new CustomResponseDto<List<PaymentDto>>().Success(200, dtos);
            return CreateActionResult(result);

        }

        [HttpPost]

        public async Task<IActionResult> Save(PaymentDto paymentDto)
        {
            //get user from token 
            var userId = 1;

            var processedEntity = _mapper.Map<Payment>(paymentDto);

            processedEntity.UpdateBy = userId;

            processedEntity.CreatedBy = userId;

            var Payment = await _paymentService.AddAsync(processedEntity);

            var paymentResponseDto = _mapper.Map<PaymentDto>(Payment);

            var response = new CustomResponseDto<PaymentDto>().Success(201, paymentDto);


            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentUpdateDto paymentDto)
        {
            var userId = 1;

            var currentPayment = await _paymentService.GetByIdAsync(paymentDto.Id);

            currentPayment.UpdateBy = userId;
            currentPayment.CustomerId = paymentDto.CustomerId;
            currentPayment.Amount = paymentDto.Amount;

            _paymentService.Update(currentPayment);

            return CreateActionResult(new CustomResponseDto<PaymentUpdateDto>().Success(204));

        }
    }
}
