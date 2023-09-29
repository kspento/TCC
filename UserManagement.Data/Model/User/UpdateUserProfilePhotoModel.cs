using Microsoft.AspNetCore.Http;
using UserManagement.Data.Dto.User;

namespace UserManagement.Domain.Model.User
{
    public class UpdateUserProfilePhotoModel
    {
        public IFormFileCollection FormFile { get; set; }
        public string RootPath { get; set; }
    }
}
