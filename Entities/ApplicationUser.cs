using Microsoft.AspNetCore.Identity;

namespace Apolchevskaya.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] Avatar { get; set; }
        public string MimeType { get; set; } = string.Empty;
    }


}


