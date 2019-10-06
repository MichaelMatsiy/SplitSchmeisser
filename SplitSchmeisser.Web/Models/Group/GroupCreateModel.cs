using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SplitSchmeisser.Web.Models
{
    public class GroupCreateModel
    {
        public string Name { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public List<SelectListItem> Users { get; set; }

        public List<int> UserIds { get; set; }
    }
}
