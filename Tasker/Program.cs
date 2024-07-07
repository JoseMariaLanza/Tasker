using System.Text.Json.Serialization;
using Tasker.Middlewares;
using Tasker.Services.Mapping;

namespace Tasker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            // Configure services in ServiceExtension class
            builder.Services.ConfigureDatabase(configuration);
            builder.Services.AddRedisServices(configuration);
            builder.Services.ConfigureDependencies();
            builder.Services.ConfigureJwtAuthentication(configuration);

            //builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                //x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                x.JsonSerializerOptions.MaxDepth = 80;
            });
            builder.Services.AddAutoMapper(typeof(StartupBase), typeof(TaskProfile));
            builder.Services.AddAutoMapper(typeof(StartupBase), typeof(CategorProfile));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfigureSwagger();

            var app = builder.Build();

            app.UseMiddleware<UserSessionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasker API v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}