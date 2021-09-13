using GraphqlDemo.Core.Domain;
using GraphqlDemo.Data;
using HotChocolate.Types;

namespace GraphqlDemo.Web.Graphql.Commands.Query
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("An executable command that can be run in a CLI");

            descriptor
                .Field(c => c.Platform)
                .Description("This is a platform that the command can be run on")
                .ResolveWith<CommandResolvers>(cr => cr.GetPlatform(default!, default!))
                .UseDbContext<Db>();
        }
    }
}