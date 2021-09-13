using System.Linq;
using GraphqlDemo.Core.Domain;
using GraphqlDemo.Data;
using HotChocolate;

namespace GraphqlDemo.Web.Graphql.Platforms.Query
{
    public class PlatformResolvers
    {
        public IQueryable<Command> GetCommands(Platform platform, [ScopedService] Db db)
        {
            return db.Commands
                .Where(c => c.PlatformId == platform.Id);
        }
    }
}