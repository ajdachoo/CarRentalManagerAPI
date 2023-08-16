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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
            services.AddControllers().AddFluentValidation();
            services.AddDbContext<CarRentalManagerDbContext>();
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
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", builder =>

                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:3000")

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
