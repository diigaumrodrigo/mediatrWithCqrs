using System;
using System.Collections.Generic;
using System.Linq;
using App.Application.Domain.Repositories;
using App.Infra.DbConfigurations.Dapper;
using App.Infra.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace App.Kernel.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAutoMapperProfiles(
            this IServiceCollection services,
            params Type[] mapeamentosAdicionais)
        {
            List<Type> mapeamentos = mapeamentosAdicionais.ToList();
            mapeamentos.Add(typeof(App.Infra.MappingProfiles.MappingProfile));
            services.AddAutoMapper(mapeamentos.ToArray());
            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<ICategoriaProdutoRepository, CategoriaProdutoRepository>();

            return services;
        }

        public static IServiceCollection AddOrm(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseFactory, DatabaseFactory>();

            return services;
        }
    }
}