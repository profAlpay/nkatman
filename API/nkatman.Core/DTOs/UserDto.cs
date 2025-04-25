using nkatman.Core.Models;

namespace nkatman.Core.DTOs
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public string PaswordDto { get; set; }

        public int DepartmentId { get; set; }

        public int GroupId { get; set; }

        
        public Department? Department { get; set; }

        public Group? Group { get; set; }

    }
}
