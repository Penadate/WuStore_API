using BLL.Mappings;
using BLL.Services.Interfaces;
using BLL.Services;
using DAL.Repositories.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Extensions
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection ConfigureBusinessLayerServices(
            this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.ConfigureAutomapper();
            return services;
        }

        private static IServiceCollection ConfigureAutomapper(
            this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
            return services;
        }
    }
}
