namespace SisVentas
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
    using SisVentas.Repository;
    using System;

    public class Startup
    {
        private readonly string MiCors = "MiCors";
        public Startup(IConfiguration configuration) { Configuration = configuration; }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddCors(option => { 
                option.AddPolicy(name: MiCors, builder => {
                    builder.WithHeaders("*");
                    builder.WithOrigins("*"); 
                }); 
            });
            services.AddControllers(); 
            services.AddDbContextPool<DataContext>(options => options
                 .UseMySql(Configuration.GetConnectionString("ApiContext"), mySqlOptions => mySqlOptions
                     .ServerVersion(new Version(8, 0, 18), ServerType.MySql)
             ));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            var supportedCultures = new[] { "en", "es" };
            app.UseRequestLocalization(options =>
                options
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures)
                    .SetDefaultCulture(supportedCultures[0])
            );
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(MiCors);
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
