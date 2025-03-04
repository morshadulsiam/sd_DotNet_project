using Microsoft.AspNetCore.Identity;

namespace Crud.web.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
        public string? ProfilePicture { get; set; } // Store profile picture path
    }
}

