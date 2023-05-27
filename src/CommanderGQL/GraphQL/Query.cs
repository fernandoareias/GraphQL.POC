using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(ApplicationDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatforms([ScopedService] ApplicationDbContext ctx)
        {
            return ctx.Platforms;   
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommands([ScopedService] ApplicationDbContext ctx)
        {
            return ctx.Commands;
        }
    }
}
