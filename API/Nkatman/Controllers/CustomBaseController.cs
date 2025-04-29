using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nkatman.Core.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Nkatman.API.Controllers
{
    
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204) 
            { 
               return new ObjectResult(null){
                     StatusCode= response.StatusCode
                };

            }
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
        [NonAction]
        public int GetUserFromToken() { 
        string reduestHeader = Request.Headers["Authorization"];
            string jwt = reduestHeader?.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var JwtSecurityToken = handler.ReadToken(jwt) as JwtSecurityToken;
            string userId = JwtSecurityToken.Claims.FirstOrDefault(Claim => Claim.Type == "sub")?.Value;
            int id = Int32.Parse(userId);
            return id == 0 ? 0 : id;


        }
    }
}
