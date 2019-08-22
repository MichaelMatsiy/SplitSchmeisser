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
            //context.Database.EnsureCreated();

            if (context.Groups.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User{ Name = "Admin", Password="Admin" },
                new User{ Name = "Jhon", Password="Admin" },
                new User{ Name = "Mark", Password="Admin" },
                new User{ Name = "Kevin", Password="Admin" }
            };


            var groups = new Group[]
            {
                new Group{Name="Carson", Users = users},
                new Group{Name="Meredith"},
                new Group{Name="Arturo"},
                new Group{Name="Gytis"},
                new Group{Name="Yan"},
                new Group{Name="Peggy"},
                new Group{Name="Laura"},
                new Group{Name="Nino"}
            };

            context.Groups.AddRange(groups);
            context.Users.AddRange(users);

            context.SaveChanges();
        }
    }
}
