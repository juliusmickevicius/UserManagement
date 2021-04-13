using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Serilog;
using UserManagement.Repository;

namespace UserManagement
{
    public class Startup
    {
        private IConfiguration _configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));

            services.AddMvcCore().AddNewtonsoftJson(n => n.SerializerSettings.Converters.Add(new StringEnumConverter()));
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(s => 
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "User management service", Version = "v1"});
            });

            services
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<ILogger>(l => Log.Logger);

        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            app.UsePathBase("/UserManagement");
            app.UseRouting();


            app.UseSwagger();
            app.UseSwaggerUI(a =>  a.SwaggerEndpoint("v1/swagger.json", "User management service"));

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
