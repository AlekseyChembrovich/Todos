﻿using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Todos.Application.Configuration;

public static class ServicesConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
