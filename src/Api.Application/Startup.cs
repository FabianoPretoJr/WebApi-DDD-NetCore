using System;
using System.Collections.Generic;
using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using Api.Data.Data;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if(_environment.IsEnvironment("Testing"))
            {
                Environment.SetEnvironmentVariable("DB_CONNECTION", "Persist Security Info=True;Server=localhost;Port=3306;DataBase=dbAPI_Integration;Uid=root;Pwd=root");
                Environment.SetEnvironmentVariable("DATABASE", "MYSQL");
                Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
                Environment.SetEnvironmentVariable("Audience", "FabianoPreto");
                Environment.SetEnvironmentVariable("Issuer", "FabianoPreto");
                Environment.SetEnvironmentVariable("Seconds", "2800");
            }
            
            services.AddControllers();

            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);

            var config = new AutoMapper.MapperConfiguration(cfg => {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(authOptions => 
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
                paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth => {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Version = "v1", 
                    Title = "Curso de API com DDD",
                    Description = "Arquitetura DDD",
                    TermsOfService = new Uri("http://www.exemplo.com.br"),
                    Contact = new OpenApiContact
                    {
                        Name = "Fabiano Preto Junior",
                        Email = "fabiano.pretojr@hotmail.com",
                        Url = new Uri("https://github.com/FabianoPretoJr")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Termo de licença de uso",
                        Url = new Uri("http://www.exemplo.com.br")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Entre com o token JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Curso de API AspNetCore - DDD");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if(Environment.GetEnvironmentVariable("MIGRATION").ToLower() == "APLICAR".ToLower())
            {
                using(var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using(var context = service.ServiceProvider.GetService<ApplicationDbContext>())
                    {
                        context.Database.Migrate();
                    }
                }
            }
        }
    }
}
