using SplitSchmeisser.DAL.Entities;
using System;
using System.Xml.Serialization;

namespace SplitSchmeisser.BLL.Models
{
    [Serializable, XmlRoot(ElementName = "User")]
    public class UserDTO
    {
        [XmlIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public static UserDTO FromEntity(User user)
        {
            return new UserDTO {
                Id = user.Id,
                Name = user.Name
            };
        }
    }
}
