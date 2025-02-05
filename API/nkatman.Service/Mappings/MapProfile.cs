using AutoMapper;
using nkatman.Core.DTOs;
using nkatman.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Service.Mappings
{
    public class MapProfile : Profile
    {
        protected MapProfile()
        {
            CreateMap<Customer,CustomerDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Sale, SaleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<GroupInRole, GroupInRoleDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
        }
    }
}
