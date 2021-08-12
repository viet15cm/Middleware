using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Middleware.AddMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware
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
            services.AddSingleton<TwoMiddleware>();
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

           
            app.Use(async(context, next)=> {

                Console.WriteLine("OneMiddleware");
                context.Items.Add("KeyOne", $"{DateTime.Now.ToString()}");
                await next();
   
            });

            app.UseMiddleware<TwoMiddleware>();
            app.UseMiddleware<ThreeMiddleware>();

            app.UseRouting();

            app.UseEndpoints((endpoint) => {

                //endpoint.MapControllers();
                // endpoint.MapRazorPages();
                //E1
                endpoint.MapGet("/about.html", async (context) => {

                    await context.Response.WriteAsync("<h1>Trang gioi thieu</h1>");
                
                });
                //E2
                endpoint.MapGet("/SanPham.html", async (context) => {

                    await context.Response.WriteAsync("<h1>Trang san pham</h1>");

                });

            });

            app.Map("/admin",  (app1) => {
                app1.UseRouting();

                app1.UseEndpoints((endpoint) => {

                    //endpoint.MapControllers();
                    // endpoint.MapRazorPages();
                    //E1
                    endpoint.MapGet("/user", async (context) => {

                        await context.Response.WriteAsync("<h1>Trang quan ly user</h1>");

                    });

                    endpoint.MapGet("/product", async (context) => {

                        await context.Response.WriteAsync("<h1>Trang quan ly product</h1>");

                    });

                });

                app1.Run(async (context) => {

                        await context.Response.WriteAsync("<h1>Trang ADMIN</h1>");

                });
            });

            app.Run(async (context)=>{

                await context.Response.WriteAsync("<h1>XIN CHAO VIET<h1>");

            });

            

            /* app.UseEndpoints(endpoints =>
             {
                 endpoints.MapRazorPages();
             });
             */
        }
    }
}
