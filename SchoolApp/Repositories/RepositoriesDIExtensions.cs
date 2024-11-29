﻿using SchoolApp.Abstractions;
using System.Runtime.CompilerServices;

namespace SchoolApp.Repositories
{
    public static class RepositoriesDIExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}