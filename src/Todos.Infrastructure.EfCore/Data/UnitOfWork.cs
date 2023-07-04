using Todos.Domain.Abstractions;

namespace Todos.Infrastructure.EfCore.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync()
    {
        var count = await _context.SaveChangesAsync();

        return count > 0;
    }
}
