using Todos.Domain.Aggregates.Note;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Todos.Infrastructure.EfCore.TypeConfigurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.ExpiryDate).IsRequired();

        builder.ToTable(nameof(Note));
    }
}
