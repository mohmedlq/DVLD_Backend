using DVLD.Domain.Enums;

namespace DVLD.Application.DTOs.Users
{
    public class UserResponseDto
    {
        public int UserID { get; set; }
        public int? PersonID { get; set; }
        public string UserName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public enPermissions? Permissions { get; set; }
    }

    public class CreateUserDto
    {
        public int? PersonID { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool? IsActive { get; set; }
        public enPermissions? Permissions { get; set; }
    }

    public class UpdateUserDto
    {
        public int? PersonID { get; set; }
        public string UserName { get; set; } = null!;
        public bool? IsActive { get; set; }
    }

    public class UserChangePasswordDto
    {
        public string NewPassword { get; set; } = null!;
    }

    public class UpdatePermissionsDto
    {
        public enPermissions Permissions { get; set; }
    }

    public class LoginDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
