using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.BLL.CommonLogic;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System;

namespace SplitSchmeisser.Web.Models
{
    [Serializable, XmlRoot(ElementName = "Group")]
    public class GroupViewModel
    {
        [XmlIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OperationViewModel> Operations { get; set; }

        public List<Debt> Debts { get; set; }

        public static GroupViewModel FromDTO(GroupDTO dto)
        {
            return new GroupViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Debts = dto.UserDebts,
                Operations = dto.Operations.Select(x => OperationViewModel.FromDTO(x)).ToList()
            };
        }

    }
}