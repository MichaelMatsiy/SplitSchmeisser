using System.ComponentModel.DataAnnotations;

namespace SplitSchmeisser.Web.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Name is empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
