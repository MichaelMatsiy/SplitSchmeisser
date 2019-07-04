using SplitSchmeisser.DAL.Context;
using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SplitSchmeisser.DAL
{
    public class DbInitializer
    {
        public static void Initialize(SchmeisserContext context)
        {
            context.Database.EnsureCreated();

            if (context.Groups.Any())
            {
                return; 
            }


            var groups = new Group[]
            {
                new Group{GroupName="Carson"},
                new Group{GroupName="Meredith"},
                new Group{GroupName="Arturo"},
                new Group{GroupName="Gytis"},
                new Group{GroupName="Yan"},
                new Group{GroupName="Peggy"},
                new Group{GroupName="Laura"},
                new Group{GroupName="Nino"}
            };

            var users = new User[]
            {
                new User{ UserName = "Admin" },
                new User{ UserName = "Jhon"},
                new User{ UserName = "Mark"},
                new User{ UserName = "Kevin"}
            };

            context.Groups.AddRange(groups);
            context.Users.AddRange(users);

            context.SaveChanges();
        }
    }
}
