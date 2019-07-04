using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitSchmeisser.BLL.Models
{
    public class GroupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static GroupDTO FromEntity(Group group)
        {
            return new GroupDTO
            {
                Id = group.Id,
                Name = group.GroupName
            };
        }
    }
}
