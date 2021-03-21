using BonusBot.Common.Defaults;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BonusBot.WebDashboardModule
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(p =>
                    p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().Build());
            });

            services.AddControllersWithViews();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
            });
            services.AddAuthentication()
                .AddCookie(options =>
                {
                    //options.AccessDeniedPath = "/login";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.LoginPath = "/login";
                });

            if (Constants.IsDocker) 
                services.AddDataProtection().PersistKeysToFileSystem(new("/bonusbot-data/temp-keys/"))
                    .UseCryptographicAlgorithms(new()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
                //app.UseHttpsRedirection();
            }
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseCors();
            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy(new()
            {
                MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            //Todo: Delete later? Needs testing
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../../../../Modules/WebDashboardModule/ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
