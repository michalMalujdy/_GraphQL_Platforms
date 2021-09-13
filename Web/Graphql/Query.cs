using System.Linq;
using GraphqlDemo.Core.Domain;
using GraphqlDemo.Data;
using HotChocolate;
using HotChocolate.Data;

namespace GraphqlDemo.Web.Graphql
{
    public class Query
    {
        [UseDbContext(typeof(Db))]
        [UseFiltering]
        public IQueryable<Platform> GetPlatforms([ScopedService] Db db)
        {
            return db.Platforms;
        }

        
        [UseDbContext(typeof(Db))]
        [UseFiltering]
        public IQueryable<Command> GetCommands([ScopedService] Db db)
        {
            return db.Commands;
        }
    }
}