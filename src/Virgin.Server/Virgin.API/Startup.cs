using Castle.DynamicProxy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Serilog.Events;
using System;
using Virgin.API.Helpers;
using Virgin.API.Helpers.Extensions;
using Virgin.Core.Configuration;
using Virgin.Core.HostedServices;
using Virgin.Infrastructure.DAL;
using Virgin.Infrastructure.DAL.Interfaces;
using Virgin.MSGraph.Configuration;
using Virgin.MSGraph.Services;
using Virgin.MSGraph.Services.Interfaces;

namespace Virgin.API
{
    public class Startup
    {
        private readonly LogEventLevel _appDefaultLogLevel;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            if (Enum.TryParse(typeof(LogEventLevel), configuration.GetSection("Serilog:MinimumLevel:Default").Value, out object appDefaultLogLevel))
            {
                _appDefaultLogLevel = (LogEventLevel)appDefaultLogLevel;
            }
            else
            {
                throw new ArgumentException("Invalid application Minimum Log Level!");
            }
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(Configuration);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(VirginAuthorizationConstants.AdminPolicy,
                    policy => policy.RequireClaim(VirginAuthorizationConstants.RoleClaim, VirginRoles.VRGAdmin));
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(Configuration.GetSection("CORS").GetSection("ApplicationUrls").Get<string[]>());
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Virgin.API", Version = "v1" });
            });

            services.AddSingleton(new ProxyGenerator());

            StartupHelpers.RegisterDomainServices(services, _appDefaultLogLevel);

            services.Configure<MicrosoftGraphConfiguration>(Configuration.GetSection("MicrosoftGraph"));
            services.Configure<ClientConfiguration>(Configuration.GetSection("Client"));

            services.AddHttpContextAccessor();

            services.AddHostedService<DemoUserHostedService>();

            services.AddScoped<IVirginUnitOfWork, VirginUnitOfWork>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            StartupHelpers.AddDbContexts(services, Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Virgin.API v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MigrateDatabase<VirginDbContext>();
        }
    }
}