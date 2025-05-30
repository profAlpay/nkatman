﻿using AutoMapper;
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
    public class GroupInRolesController : CustomBaseController
    {
        private readonly IGroupInRoleService _groupInRoleService;
        private readonly IMapper _mapper;
        public GroupInRolesController(IGroupInRoleService groupInRoleService, IMapper mapper)
        {
            _groupInRoleService = groupInRoleService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var groupInRoles = _groupInRoleService.GetAll().ToList();
            var dtos = _mapper.Map<IEnumerable<GroupInRoleDto>>(groupInRoles).ToList();


            var result = new CustomResponseDto<List<GroupInRoleDto>>().Success(200, dtos);
            return CreateActionResult(result);

        }

        [ServiceFilter(typeof(NotFoundFilter<GroupInRole>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var groupInRole = await _groupInRoleService.GetByIdAsync(id); 

            var groupInRoleDto = _mapper.Map<GroupInRoleDto>(groupInRole);

            var nesne = new CustomResponseDto<GroupInRoleDto>();
            return CreateActionResult(nesne.Success(200, groupInRoleDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<GroupInRole>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            //get user from token
            int GroupInRoleId = 1;
            var GroupInRole = await _groupInRoleService.GetByIdAsync(id);
            GroupInRole.UpdateBy = GroupInRoleId;

            _groupInRoleService.ChangeStatus(GroupInRole);

            return CreateActionResult(new CustomResponseDto<NoContentDto>().Success(204));
        }


        [HttpPost]

        public async Task<IActionResult> Save(GroupInRoleDto groupInRoleDto)
        {
         

            var processedEntity = _mapper.Map<GroupInRole>(groupInRoleDto);         

            var GroupInRole = await _groupInRoleService.AddAsync(processedEntity);

            var response = new CustomResponseDto<GroupInRoleDto>().Success(201, groupInRoleDto);


            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(GroupInRoleUpdateDto groupInRoleDto)
        {
            var userId = 1;

            var currentGroupInRole = await _groupInRoleService.GetByIdAsync(groupInRoleDto.Id);

            currentGroupInRole.UpdateBy = userId;
            currentGroupInRole.GroupId = groupInRoleDto.GroupId;
            currentGroupInRole.RoleId = groupInRoleDto.RoleId;

            try
            {

                _groupInRoleService.Update(currentGroupInRole);
            }
            catch (Exception ex)
            {

                return CreateActionResult(new CustomResponseDto<GroupInRoleUpdateDto>().Fail(400,ex.Message));

                            }

            return CreateActionResult(new CustomResponseDto<GroupInRoleUpdateDto>().Success(204));

        }

    }
}
