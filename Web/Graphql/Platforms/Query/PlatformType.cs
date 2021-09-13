using GraphqlDemo.Core.Domain;
using GraphqlDemo.Data;
using HotChocolate.Types;

namespace GraphqlDemo.Web.Graphql.Platforms.Query
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents any software or service that has CLI.");
            
            descriptor
                .Field(p => p.LicenseKey)
                .Ignore();

            descriptor
                .Field(p => p.Commands)
                .Description("List of commands available for given Platform")
                .ResolveWith<PlatformResolvers>(pr => pr.GetCommands(default!, default!))
                .UseDbContext<Db>();
        }
    }
}