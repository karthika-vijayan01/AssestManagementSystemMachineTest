using AssestManagementSystemMachineTest.Models;
using AssestManagementSystemMachineTest.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AssestManagementSystemMachineTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

              .AddJwtBearer(opt =>
              {
                  opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = builder.Configuration["Jwt:Issuer"],
                      ValidAudience = builder.Configuration["Jwt:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                           builder.Configuration["Jwt:Key"]))
                  };
              });

            builder.Services.AddControllersWithViews()
            .AddJsonOptions(
            options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services.AddDbContext<AssestManagementExamContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("PropelAug2024Connection")));



            builder.Services.AddScoped<ILoginRepository, LoginRepository>();

          builder.Services.AddScoped<IAssestManagementRepository, AssestManagementRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run(); ;
        }
    }
}
