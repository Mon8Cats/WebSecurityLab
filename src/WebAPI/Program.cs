
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using WebAPI.Z2_Infrastructure.Data;
using WebAPI.Z3_Application.Interfaces;
using WebAPI.Z3_Application.Services;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        //builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();
        //builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AppDbContext>(options =>
            /*options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            )*/
            options.UseSqlite(
                builder.Configuration.GetConnectionString("SqliteConnection")
            )
        );
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration.GetSection("AppSettings:Issuer").Value!,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration.GetSection("AppSettings:Audience").Value!,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(
                            builder.Configuration.GetSection("AppSettings:Token").Value!
                        )
                    ),
                    ClockSkew = TimeSpan.Zero
                };
            });


        builder.Services.AddScoped<IAuthService, AuthService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
            //app.UseSwagger();
            //app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
