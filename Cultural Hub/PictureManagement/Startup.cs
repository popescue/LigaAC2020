using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository.SQL;
using Services.Client;
using Services.User;
using WebApp.Repositories;

namespace PictureManagement
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
            services.AddControllers();

            services.AddDbContext<CulturalHubContext>(
               options => options.UseSqlServer(Configuration.GetConnectionString("CulturalHubConnection")));

			// register repositories
			services.AddScoped<IUserEventsReader, UserEventsReader>();
			services.AddScoped<IClientEventsReader, ClientEventsReader>();
			services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IPicturesRepository, PicturesRepository>();

            //register services
            services.AddScoped<UserEventsService, UserEventsService>();
            services.AddScoped<ClientEventsService, ClientEventsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
