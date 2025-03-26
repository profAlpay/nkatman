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
    public class GroupsController : CustomBaseController
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            //var groups = _groupService.GetAll().Where(g => g.Name.Contains("Admin")).ToList();
            var groups = _groupService.GetAll().ToList();
            var dtos = _mapper.Map<IEnumerable<GroupDto>>(groups).ToList();


            var result = new CustomResponseDto<List<GroupDto>>().Success(200, dtos);
            return CreateActionResult(result);

        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var group = await _groupService.GetByIdAsync(id);

            var groupDto = _mapper.Map<GroupDto>(group);

            var nesne = new CustomResponseDto<GroupDto>();
            return CreateActionResult(nesne.Success(200, groupDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            //get user from token
            int GroupId = 1;
            var Group = await _groupService.GetByIdAsync(id);
            Group.UpdateBy = GroupId;

            _groupService.ChangeStatus(Group);

            return CreateActionResult(new CustomResponseDto<NoContentDto>().Success(204));
        }

        [HttpPost]

        public async Task<IActionResult> Save(GroupDto groupDto)
        {

            try { 
            //get user from token 
            var userId = 1;
            groupDto.CreatedDate = DateTime.Now;
            var processedEntity = _mapper.Map<Group>(groupDto);

            processedEntity.UpdateBy = userId;

            processedEntity.CreatedBy = userId;
            

            var Group = await _groupService.AddAsync(processedEntity);

            var groupResponseDto = _mapper.Map<GroupDto>(Group);

            var response = new CustomResponseDto<GroupDto>().Success(201, groupDto);


            return CreateActionResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Hata oluştu.",
                    InnerException = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(GroupUpdateDto groupDto)
        {
            var userId = 1;

            var currentGroup = await _groupService.GetByIdAsync(groupDto.Id);

            currentGroup.UpdateBy = userId;
            currentGroup.Name = groupDto.Name;

            _groupService.Update(currentGroup);

            return CreateActionResult(new CustomResponseDto<GroupUpdateDto>().Success(204));

        }
    }
}
