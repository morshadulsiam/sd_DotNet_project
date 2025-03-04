namespace Crud.web.ViewModels
{
    public class EditProfileViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public IFormFile? ProfilePictureFile { get; set; } // For file upload
    }
}


