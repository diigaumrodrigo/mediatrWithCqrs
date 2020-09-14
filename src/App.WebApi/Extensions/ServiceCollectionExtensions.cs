using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using App.Application.Domain.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace App.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            return services;
        }

        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddNewtonsoftJson();

            return services;
        }

        public static void AddMediatr(this IServiceCollection services)
        {
            const string applicationAssemblyName = "App.Application";
            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);            

            services.AddMediatR(typeof(Startup), typeof(CadastraProdutoValidator));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));

            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddTransient(result.InterfaceType, result.ValidatorType));
        }
    }
}