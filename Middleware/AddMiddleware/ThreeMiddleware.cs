using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.AddMiddleware
{
    public class ThreeMiddleware
    {
        private readonly RequestDelegate _next;

        public ThreeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if(context.Request.Path == "/viet")
            {
                await Task.Run(async () => {

                   
                    string html = "<h1>CAM KHONG DUOC TRUY CAP</h1>";
                    Console.WriteLine("ThreeMiddleware");
                    context.Response.Headers.Add("TieuDe", "Day la tieu de cam truy cap middlewareThree");
                    var c = context.Items["KeyOne"] ?? "<h1>bien Onekey bang null</h1>";
                    var b = context.Items["KeyTwo"] ?? "<h1>bien Onekey bang null</h1>";
                    await context.Response.WriteAsync($"<h1>{(string)c}</h1>");
                    await context.Response.WriteAsync($"<h1>{(string)b}</h1>");
                    await context.Response.WriteAsync(html);

                });

              
            }
            else
            {
                Console.WriteLine("ThreeMiddleware");
                var c = context.Items["KeyOne"] ?? "bien Onekey bang null";
                var b = context.Items["KeyTwo"] ?? "bien TwoKey bang null";
                context.Response.Headers.Add("TieuDe", "Day la tieu de cho truy cap middlewareThree");
                await context.Response.WriteAsync($"<h1>{(string)c}</h1>");
                await context.Response.WriteAsync($"<h1>{(string)b}</h1>");
                await context.Response.WriteAsync($"<h1>Duoc Phep Truy Cap</h1>");
                await _next(context);
            }
            

          
        }

    }
}
