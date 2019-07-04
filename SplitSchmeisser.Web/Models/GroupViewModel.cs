﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.Web.Models
{
    public class GroupViewModel
    {
        public GroupViewModel()
        {
            this.Users = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }

        public List<SelectListItem> Users { get; set; }

    }
}