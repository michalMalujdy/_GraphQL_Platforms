using System.Linq;
using GraphqlDemo.Core.Domain;
using GraphqlDemo.Data;
using HotChocolate;

namespace GraphqlDemo.Web.Graphql.Commands.Query
{
    public class CommandResolvers
    {
        public IQueryable<Platform> GetPlatform(Command command, [ScopedService] Db db)
        {
            return db.Platforms
                .Where(p => p.Id == command.PlatformId);
        }
    }
}