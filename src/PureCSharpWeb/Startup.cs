using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
//using PureCSharpWeb.Repositories;
//using PureCSharpWeb.Entities;
using PureCSharpWeb.Framework;
using PureCSharpWeb.Views;
using PureCSharpWeb.Models;
using PureCSharpWeb.Controllers;

namespace PureCSharpWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            Order order = new Order() { Customer = new Customer() { Id = "cus123", Address ="1 vegas way", Name ="Anivdelarev"}, Id = "ord123",
                LineItems = { new LineItem() {ProductId ="prod1", Price =10.00m, Quantity =10, ExtendedPrice =100.00m },
                                new LineItem() { ProductId = "prod2", Price = 12.00m, Quantity = 10, ExtendedPrice = 120.00m } }, SubTotal = 220.00m,
            NewLineItem = new LineItem() { ProductId = "", Price =0.00m, Quantity = 1} };
            OrderEntrySample view = new OrderEntrySample(order, new OrderController());
            var htmlConverter = new HtmlConverter();
            var url = ControllerRegistry.GetControllerUrl<Order>(OrderController.AddItem);
            app.Run(async (context) =>
            {
                if (context.Request.Path == "/")
                {
                    await context.Response.WriteAsync(htmlConverter.Convert(view));
                }
                else 
                {
                    await context.Response.WriteAsync(ControllerRegistry.ExecuteController(context.Request.Path, context.Request.Body));
                }
            });
        }
    }
}
