using Todos.Domain.Aggregates.Note;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Todos.Infrastructure.EfCore.Data;

public static class DbInitializer
{
    public static void Initialize(this IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var dbCreator = context.GetService<IDatabaseCreator>();
        var relationalDbCreator = dbCreator as RelationalDatabaseCreator;
        bool exists = relationalDbCreator.Exists();
        if (exists)
        {
            context.Database.Migrate();
        }
        else
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
            EnsureSeedData(context);
        }
    }

    private static void EnsureSeedData(ApplicationDbContext context)
    {
        context.Notes.AddRangeAsync(new[]
        {
            new Note(Guid.Parse("50ec1b82-e78f-430c-9f49-72b4dbc0f4e3"),
                "Need to create a virtual card.", DateTime.Now, DateTime.Now.AddDays(2)),
            new Note(Guid.Parse("94fd3538-89a9-4a2f-b5c9-c442b1960e46"),
                "Need to recover the phone.", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(2)),
            new Note(Guid.Parse("1a4c911b-c8ac-4abe-b314-03c1b0737a59"),
                "Need to cook dinner.", DateTime.Now, DateTime.Now.AddHours(7)),
            new Note(Guid.Parse("b5d48673-34e7-4e36-a324-ada47c944663"),
                "Need to read the next chapter of the book.", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2)),
            new Note(Guid.Parse("60df4138-bbe1-4368-9ed7-92fad8d9b829"),
                "Need to clean house.", DateTime.Now, DateTime.Now.AddDays(2))
        });

        context.SaveChanges();
    }
}
