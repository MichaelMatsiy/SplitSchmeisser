using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.Web.Models
{
    public class CreateGroupModel
    {
        public string Name { get; set; }

        public double Amount { get; set; }

        public List<SelectListItem> Users { get; set; }

        public List<int> UserIds { get; set; }
    }
}
