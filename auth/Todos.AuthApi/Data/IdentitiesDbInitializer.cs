namespace Todos.AuthApi.Data;

public static class IdentitiesDbInitializer
{
    public static void Initialize(IdentitiesDbContext context)
    {
        context.Database.EnsureCreated();
    }
}
