﻿using Microsoft.EntityFrameworkCore;
using Tasker.Repositories.Tasks;
//using Tasker.Helpers;
//using Tasker.Services;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Tasker.Services.Tasks;
using AuthOrchestrator.Redis;
using Tasker.Services;
using Tasker.Repositories.Auth;
using AuthOrchestrator.Jwt;
using System.Text;

namespace Tasker
{
    public static class ServiceExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IAuthDbContext, AuthDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("TaskerConnection")));
            services.AddDbContext<ITaskDbContext, TaskDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("TaskerConnection")));
        }

        public static void ConfigureDependencies(this IServiceCollection services)
        {
            //services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddScoped<ITaskRepository, TaskRepository>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasker API", Version = "v1" });

                // Enable Authorization in swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 1234abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    //ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = false,
                    //ValidAudience = jwtSettings["Audience"]
                };
            });

            services.AddScoped<IJwtService, JwtService>();
        }

        public static void AddRedisServices(this IServiceCollection services, IConfiguration configuration)
        {
            //var redisConfig = configuration["Redis:Configuration"];
            //services.AddSingleton<IRedisHelper, RedisHelper>(sp => new RedisHelper(redisConfig));

            var redisSettings = new RedisSettings();
            configuration.GetSection("Redis").Bind(redisSettings);

            services.AddSingleton(redisSettings);
            services.AddSingleton<IRedisHelper, RedisHelper>(sp => new RedisHelper(redisSettings.Configuration));
            services.AddTransient<IRedisService, RedisService>();
        }

        //public static void ConfigureMiddlewares(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.UserSessionMiddleware
        //}
    }
}
