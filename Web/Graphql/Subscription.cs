using GraphqlDemo.Core.Domain;
using HotChocolate;
using HotChocolate.Types;

namespace GraphqlDemo.Web.Graphql
{
    public class Subscription
    {
        [Topic]
        [Subscribe]
        public Platform OnPlatformAdded([EventMessage] Platform platform)
        {
            return platform;
        }
    }
}