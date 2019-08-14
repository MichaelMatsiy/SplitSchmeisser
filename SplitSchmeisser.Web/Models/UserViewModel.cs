using Microsoft.AspNetCore.Mvc.Rendering;
using SplitSchmeisser.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.Web.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.Groups = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }

        public List<SelectListItem> Groups { get; set; }

        public static UserViewModel FromDTO(UserDTO dto)
        {
            return new UserViewModel
            {
                UserName = dto.Name
            };
        }
    }
}
