using Todos.Domain.Aggregates.Note;
using Microsoft.EntityFrameworkCore;
using Todos.Infrastructure.EfCore.TypeConfigurations;

namespace Todos.Infrastructure.EfCore.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Note> Notes { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("data");

        builder.ApplyConfiguration(new NoteConfiguration());

        base.OnModelCreating(builder);
    }
}
