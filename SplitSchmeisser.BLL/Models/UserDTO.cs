using SplitSchmeisser.DAL.Entities;

namespace SplitSchmeisser.BLL.Models
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static UserDTO FromEntity(User user)
        {
            return new UserDTO {
                Id = user.Id,
                Name = user.UserName
            };
        }
    }
}
