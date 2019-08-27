using Microsoft.Extensions.Configuration;
using SplitSchmeisser.DAL.Context;
using System.Data.Entity.Infrastructure;

namespace SplitSchmeisser.Web
{
    //public class SchmeisserContextFactory : IDbContextFactory<SchmeisserContext>
    //{
        //public IConfiguration Configuration { get; }

        //public SchmeisserContextFactory(
        //    IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //SchmeisserContext IDbContextFactory<SchmeisserContext>.Create()
        //{
        //    return new SchmeisserContext(Configuration.GetConnectionString("DefaultConnection"));
        //}
    //}
}
