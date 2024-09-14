using _253503_Yarmak.Persistense.Data;
using _253503_Yarmak.Persistense.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Persistense;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistense(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWork, EFUnitOfWork>();
        return services;
    }
    public static IServiceCollection AddPersistense(this IServiceCollection services,
        DbContextOptions options)
    {
        services.AddPersistense()
            .AddSingleton<AppDbContext>(
                new AppDbContext((DbContextOptions<AppDbContext>)options));
        return services;
    }
}
