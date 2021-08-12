using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.AddMiddleware
{
    public class TwoMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("MiddlewareTwo");
            context.Items.Add("KeyTwo", $"{context.Request.Path}");
            await next(context);
        }
    }
}
