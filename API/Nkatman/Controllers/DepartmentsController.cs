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
    public class DepartmentsController : CustomBaseController
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public DepartmentsController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var departments = _departmentService.GetAll();
            var dtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments).ToList();


            var result = new CustomResponseDto<List<DepartmentDto>>().Success(200, dtos);
            return CreateActionResult(result);

        }

        [HttpPost]

        public async Task<IActionResult> Save(DepartmentDto departmentDto)
        {
            //get user from token 
            var userId = 1;

            var processedEntity = _mapper.Map<Department>(departmentDto);

            processedEntity.UpdateBy = userId;

            processedEntity.CreatedBy = userId;

            var Department = await _departmentService.AddAsync(processedEntity);

            var departmentResponseDto = _mapper.Map<DepartmentDto>(Department);

            var response = new CustomResponseDto<DepartmentDto>().Success(201, departmentDto);


            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(DepartmentUpdateDto departmentDto)
        {
            var userId = 1;

            var currentDepartment = await _departmentService.GetByIdAsync(departmentDto.Id);

            currentDepartment.UpdateBy = userId;
            currentDepartment.Name = departmentDto.Name;

            _departmentService.Update(currentDepartment);

            return CreateActionResult(new CustomResponseDto<DepartmentUpdateDto>().Success(204));

        }
    }
}
