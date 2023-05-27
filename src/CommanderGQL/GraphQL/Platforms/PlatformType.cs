using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents any software or service that has a command line interface.");

            descriptor.Field(c => c.LicenseKey).Ignore();

            descriptor.Field(c => c.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
                .UseDbContext<ApplicationDbContext>()
                .Description("Represents a avaliable commands.");
        }

        private class Resolvers
        {
            public IQueryable<Command> GetCommands(Platform platform, [ScopedService] ApplicationDbContext ctx)
            {
                return ctx.Commands.Where(p => p.PlatformId == platform.Id);
            }
        }
    }
}
