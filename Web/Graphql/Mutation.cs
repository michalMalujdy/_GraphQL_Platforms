using System.Threading;
using System.Threading.Tasks;
using GraphqlDemo.Core.Domain;
using GraphqlDemo.Data;
using GraphqlDemo.Web.Graphql.Commands.Mutation.AddCommand;
using GraphqlDemo.Web.Graphql.Platforms.Mutation.AddPlatform;
using GraphqlDemo.Web.Graphql.Platforms.Mutation.DeletePlatform;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GraphqlDemo.Web.Graphql
{
    public class Mutation
    {
        [UseDbContext(typeof(Db))]
        public async Task<AddPlatformPayload> AddPlatform(
            AddPlatformInput input,
            [ScopedService] Db db,
            [Service] ITopicEventSender topicEventSender,
            CancellationToken ct)
        {
            var platform = new Platform {Name = input.Name};

            db.Platforms.Add(platform);
            await db.SaveChangesAsync(ct);

            await topicEventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, ct);

            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(Db))]
        public async Task<DeletePlatformPayload> DeletePlatform(
            DeletePlatformInput input,
            [ScopedService] Db db,
            CancellationToken ct)
        {
            var platform = await db.Platforms.FirstOrDefaultAsync(p => p.Id == input.Id, ct);
            db.Remove(platform);
            await db.SaveChangesAsync(ct);

            return new DeletePlatformPayload(platform);
        }

        [UseDbContext(typeof(Db))]
        public async Task<AddCommandPayload> AddCommand(
            AddCommandInput input,
            [ScopedService] Db db,
            CancellationToken ct)
        {
            var command = new Command
            {
                CommandLine = input.CommandLine,
                HowTo = input.HowTo,
                PlatformId = input.PlatformId
            };

            db.Commands.Add(command);
            await db.SaveChangesAsync(ct);

            return new AddCommandPayload(command);
        }
    }
}