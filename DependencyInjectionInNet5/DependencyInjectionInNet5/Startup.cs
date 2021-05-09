using DependencyInjectionInNet5.Configuration;
using DependencyInjectionInNet5.Middleware;
using DependencyInjectionInNet5.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionInNet5
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
            services.AddTransient<IGuidService, GuidService>();
            services.AddScoped<ScopedDependency>();
            services.AddTransient<TransientDependency>();
            services.AddSingleton<SingletonDependeny>();

            services.AddScoped<IMailSenderService, MailSenderService>();
            services.AddScoped<IMailSenderService, XMailSenderService>();

            //services.Replace(ServiceDescriptor.Scoped<IMailSenderService, XMailSenderService>());
            //services.RemoveAll<IMailSenderService>();

            services.Configure<MailSettingsConfiguration>(Configuration.GetSection("MailSettings"));

            services.AddControllersWithViews();
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
            }

            app.UseGuidLogMiddleware();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
