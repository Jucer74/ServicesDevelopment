using Microsoft.Extensions.DependencyInjection;
using Student.Application.Interfaces;
using Student.Application.Services;
using Student.Domain.Common;
using Student.Domain.Entities;
using Student.Domain.Interfaces.Repositories;
using Student.Infrastructure.Common;
using Student.Infrastructure.Repositories;

namespace Student.Api.Extensions
{
   public static class ModuleCollectionExtension
   {
      public static IServiceCollection AddCoreModules(this IServiceCollection services)
      {
         // Services / Use Cases
         services.AddScoped<IStudentService, StudentService>();

         return services;
      }

      public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
      {
         // Repositories
         services.AddScoped<IStudentRepository, PersonStudent

         return services;
      }
   }
}