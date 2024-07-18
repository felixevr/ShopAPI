using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tienda.Application.Interfaces;
using Tienda.Application.Services;

namespace Tienda.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration); // Create an IConfiguration instance for the entire app
            services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic)));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICategoryApplication, CategoryApplication>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IProductApplication, ProductApplication>();
            services.AddScoped<ICartApplication, CartApplication>();
            //services.AddScoped<ICartItemApplication, CartItemApplication>();




            return services;
        }
    }
}
