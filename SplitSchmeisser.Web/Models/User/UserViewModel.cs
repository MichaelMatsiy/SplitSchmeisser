using Microsoft.AspNetCore.Mvc.Rendering;
using SplitSchmeisser.BLL.Models;
using System;
using System.Collections.Generic;

namespace SplitSchmeisser.Web.Models
{
    [Serializable]
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.Groups = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<SelectListItem> Groups { get; set; }

        public static UserViewModel FromDTO(UserDTO dto)
        {
            return new UserViewModel
            {
                Name = dto.Name
            };
        }
    }
}
