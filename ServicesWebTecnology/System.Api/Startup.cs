using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Infrastructure.Helpers;
using System.Infrastructure.IRepository;
using System.Infrastructure.Mapping;
using System.Infrastructure.Repository;
using System.Infrastructure.Services;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using SystemQuickzal.Contexts;

namespace System.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));


            services.AddDbContext<AplicationDataContext>(cfg =>
            {
                cfg.UseNpgsql(Configuration.GetConnectionString("Connection"));
            });

            #region List of Repository
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICompositioncategoryRepository, CompositioncategoryRepository>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            #endregion

            #region Helper


            #endregion

            #region Services
            services.AddScoped<CompositioncategoryServices>();
            services.AddScoped<UsersGoalsServices>();
            #endregion

            #region Configuration of Swuager
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafío Back-End Junior", Version = "v1", Description = "Esta documentación es utilizada para Información solicitada por la empresa" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                     {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                        },
                             new string[]{}
                     }
               });

            });

            services.AddCors(options =>
            {
                options.AddPolicy("Todos",
                builder => builder.WithOrigins("*").WithHeaders("*").WithMethods("*"));
            });

            services.AddSwaggerGen();

            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddFile("LogStore/Log-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();

            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SystemApi v1");
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("Todos");
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
