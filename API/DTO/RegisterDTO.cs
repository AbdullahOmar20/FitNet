
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string  DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string  Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        ErrorMessage ="The password must have at least 1 upper case, 1 lower case , 1 numeric, and 1 special character, aslo must be 6-10 character long")]
        public string Password { get; set; }
    }
}