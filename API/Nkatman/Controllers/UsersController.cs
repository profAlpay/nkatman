using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nkatman.Core.DTOs.UpdateDTOs;
using nkatman.Core.DTOs;
using nkatman.Core.Services;
using nkatman.Core.Models;
using Microsoft.EntityFrameworkCore;
using nkatman.Service.Hashing;
using Nkatman.API.Filters;
using Microsoft.AspNetCore.Authorization;

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

        // pagination

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            var userDto = _mapper.Map<UserDto>(user);

            var nesne = new CustomResponseDto<UserDto>();
            return  CreateActionResult(nesne.Success(200, userDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            //get user from token
            int userId = 1;
            var user = await _userService.GetByIdAsync(id);
            user.UpdateBy = userId;

            _userService.ChangeStatus(user);

            return CreateActionResult(new CustomResponseDto<NoContentDto>().Success(204));
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
        [HttpPost("[action]")]
        [AllowAnonymous]

        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            Token token = await _userService.Login(userLoginDto);
            if (token == null)
            {
                return CreateActionResult(new CustomResponseDto<Token>().Fail(401, "Bilgiler Uyusmuyor"));
            }
           return  CreateActionResult(new CustomResponseDto<Token>().Success(200, token));
        }
    }
}
