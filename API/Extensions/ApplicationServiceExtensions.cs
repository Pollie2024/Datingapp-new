// Purpose: Contains the AddApplicationServices method which adds services to the application. 
// This method is called in the Program.cs file to add services to the container. 
// The services added include controllers, database context, CORS, and token service. 
// This method is used to organize the code and make it more readable by separating the service configuration from the main Program.cs file.
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
