using CommanderGQL.Data;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.Models;
using HotChocolate.Subscriptions;

namespace CommanderGQL.GraphQL
{
    public class Mutations
    {
        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, [ScopedService] ApplicationDbContext ctx, [Service] ITopicEventSender eventSender, CancellationToken token)
        {
            var platform = new Platform { Name = input.Name };

            ctx.Platforms.Add(platform);

            await ctx.SaveChangesAsync();

            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform);

            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] ApplicationDbContext ctx)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            ctx.Commands.Add(command);

            await ctx.SaveChangesAsync();

            return new AddCommandPayload(command);
        }
    }
}
