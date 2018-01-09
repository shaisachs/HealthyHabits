using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using HealthyHabits.Models;
using HealthyHabits.Translators;
using HealthyHabits.Dtos;
using HealthyHabits.Repositories;

namespace HealthyHabits
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }    

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication().AddRapidApiAuthentication();

            if (HostingEnvironment.IsDevelopment())
            {
                services.AddDbContext<HealthyHabitsContext>(opt => opt.UseInMemoryDatabase("HealthyHabits"));
            }
            else
            {
                var connection = Configuration.GetConnectionString("defaultConnection");
                services.AddDbContext<HealthyHabitsContext>(options => options.UseSqlServer(connection));
            }

            services.AddMvc();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddAutoMapper(x=> x.AddProfile(new MappingsProfile()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "HealthyHabits API", Version = "v1" });
            });

            // TODO: automate this piece
            services.AddScoped<BaseTranslator<Habit, HabitDto>, HabitTranslator>();
            services.AddScoped<BaseTranslator<HabitCompletion, HabitCompletionDto>, HabitCompletionTranslator>();
            services.AddScoped<HabitRepository, HabitRepository>();
            services.AddScoped<HabitCompletionRepository, HabitCompletionRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthyHabits API V1");
            });

            app.UseMvc();
        }
    }
}
