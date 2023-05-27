using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {

        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Represents a valid command executable");

            descriptor.Field(c => c.Platform)
                .ResolveWith<Resolvers>(c => c.GetPlatform(default!, default!))
                .UseDbContext<ApplicationDbContext>()
                .Description("This is the platform to wich the command.");

        }

        private class Resolvers
        {
            public Platform GetPlatform(Command command, [ScopedService] ApplicationDbContext ctx)
            {
                return ctx.Platforms.FirstOrDefault(f => f.Id == command.PlatformId);
            }
        }
    }
}
