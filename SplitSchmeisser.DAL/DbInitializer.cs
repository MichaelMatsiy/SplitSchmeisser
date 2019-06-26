using SplitSchmeisser.DAL.Context;
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
        }
    }
}
