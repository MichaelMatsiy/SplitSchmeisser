using SplitSchmeisser.BLL.Models;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System;

namespace SplitSchmeisser.Web.Models
{
    [Serializable, XmlRoot(ElementName = "Group")]
    public class GroupListModel
    {
        [XmlIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        [XmlIgnore]
        public List<int> UserIDs { get; set; }
        public static GroupListModel FromDTO(GroupDTO dto)
        {
            return new GroupListModel
            {
                Id = dto.Id,
                Name = dto.Name,
                UserIDs = dto.Users.Select(x => x.Id).ToList()
            };
        }

    }
}
