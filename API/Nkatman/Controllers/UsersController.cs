﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nkatman.Core.DTOs.UpdateDTOs;
using nkatman.Core.DTOs;
using nkatman.Core.Services;
using nkatman.Core.Models;
using Microsoft.EntityFrameworkCore;
using nkatman.Service.Hashing;

namespace Nkatman.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = _userService.GetAll().ToList();
            var dtos = _mapper.Map<IEnumerable<UserDto>>(users).ToList();


            var result = new CustomResponseDto<List<UserDto>>().Success(200, dtos);
            return CreateActionResult(result);

        }

        [HttpPost]

        public async Task<IActionResult> Save(UserDto userDto)
        {
            //get user from token 
            var userId = 1;

            var processedEntity = _mapper.Map<User>(userDto);

            processedEntity.UpdateBy = userId;

            processedEntity.CreatedBy = userId;
            byte[] hashh = null;
            byte[] solt = null;
            HashingHelper.CreatePassword(userDto.PaswordDto, out hashh, out solt);

            processedEntity.PasswordHash = hashh;
            processedEntity.PasswordSalt = solt;
            try
            {
               await _userService.AddAsync(processedEntity);
            }
            catch (Exception ex)
            {

              
            }

            var userResponseDto = _mapper.Map<UserDto>(processedEntity);

            var response = new CustomResponseDto<UserDto>().Success(201, userResponseDto);


            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto userDto)
        {
            var userId = 1;

            var currentUser = await _userService.GetByIdAsync(userDto.Id);

            currentUser.UpdateBy = userId;
            currentUser.Name = userDto.Name;
            currentUser.DepartmentId = userDto.DepartmentId;
            currentUser.GroupId = userDto.GroupId;
       


            _userService.Update(currentUser);

            return CreateActionResult(new CustomResponseDto<UserUpdateDto>().Success(204));

        }
    }
}
