using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Middleware;
using CarRentalManagerAPI.Models.Car;
using CarRentalManagerAPI.Models.Client;
using CarRentalManagerAPI.Models.Rental;
using CarRentalManagerAPI.Models.User;
using CarRentalManagerAPI.Models.Validators;
using CarRentalManagerAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManagerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationSettings = new AuthenticationSettings();

            Configuration.GetSection("Authentication").Bind(authenticationSettings);
            services.AddSingleton(authenticationSettings);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,//authenticationSettings.JwtIssuer,
                    ValidateAudience = false,//authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });

            services.AddControllers().AddFluentValidation();
            services.AddDbContext<CarRentalManagerDbContext>
                (options=>options.UseSqlServer(Configuration.GetConnectionString("CarRentalManagerDbConnection")));
            services.AddScoped<RentalSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<IValidator<CreateCarDto>, CreateCarDtoValidator>();
            services.AddScoped<IValidator<UpdateCarDto>, UpdateCarDtoValidator>();
            services.AddScoped<IValidator<CreateClientDto>, CreateClientDtoValidator>();
            services.AddScoped<IValidator<UpdateClientDto>, UpdateClientDtoValidator>();
            services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
            services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
            services.AddScoped<IValidator<CreateRentalDto>, CreateRentalDtoValidator>();
            services.AddScoped<IValidator<FinishRentalDto>, FinishRentalDtoValidator>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", builder =>

                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:3000")
                    .WithOrigins("https://ajdachoo.github.io")

                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RentalSeeder seeder)
        {
            app.UseCors("FrontEndClient");
            
            seeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();


            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRentalManager API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
