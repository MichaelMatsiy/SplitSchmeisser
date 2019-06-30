using SplitSchmeisser.DAL.Context;
using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitSchmeisser.DAL
{
    public class DbInitializer
    {
        public static void Initialize(SchmeisserContext context)
        {
            context.Database.EnsureCreated();


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

            foreach (Group g in groups)
            {
                context.Groups.Add(g);
            }
            context.SaveChanges();
        }
    }
}
