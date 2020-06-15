using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.SQL;
using WebApp.Repositories;

namespace WebApp
{
    public class CulturalHubContextFactory : IDesignTimeDbContextFactory<CulturalHubContext>
    {
        public CulturalHubContext CreateDbContext(string[] args)
        {

            IConfiguration config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<CulturalHubContext>();
            var connectionString = config.GetConnectionString("CulturalHubConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new CulturalHubContext(optionsBuilder.Options);
        }
    }

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
            services.AddDbContext<CulturalHubContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("CulturalHubConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<CulturalHubContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            // register repositories
            //services.AddScoped<IUserEventsReader, UserEventsReader>();
            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IPicturesRepository, PicturesRepository>();

            //register services
            //services.AddScoped<UserEventsService, UserEventsService>();
            //services.AddScoped<ClientEventsService, ClientEventsService>();

            services.AddAuthorization(a => a.AddPolicy("AllowAll", b => b.RequireRole("User")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}