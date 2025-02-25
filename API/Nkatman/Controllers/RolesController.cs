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
    public class RolesController : CustomBaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RolesController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var roles = _roleService.GetAll().ToList();
            var dtos = _mapper.Map<IEnumerable<RoleDto>>(roles).ToList();


            var result = new CustomResponseDto<List<RoleDto>>().Success(200, dtos);
            return CreateActionResult(result);

        }

        [HttpPost]

        public async Task<IActionResult> Save(RoleDto roleDto)
        {
            //get user from token 
            var userId = 1;

            var processedEntity = _mapper.Map<Role>(roleDto);

            processedEntity.UpdateBy = userId;

            processedEntity.CreatedBy = userId;

            var Role = await _roleService.AddAsync(processedEntity);

            var roleResponseDto = _mapper.Map<RoleDto>(Role);

            var response = new CustomResponseDto<RoleDto>().Success(201, roleDto);


            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleUpdateDto roleDto)
        {
            var userId = 1;

            var currentRole = await _roleService.GetByIdAsync(roleDto.Id);

            currentRole.UpdateBy = userId;
            currentRole.Name = roleDto.Name;

            _roleService.Update(currentRole);

            return CreateActionResult(new CustomResponseDto<RoleUpdateDto>().Success(204));

        }
    }
}
