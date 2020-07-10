using DGPub.Application.Promotions.Handlers;
using DGPub.Application.Tabs.Handlers;
using DGPub.Domain.Promotions.Handlers;
using DGPub.Domain.Tabs.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DGPub.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Repositories

            //Handlers
            services.AddScoped<IAddItemTabHandler, AddItemTabHandler>();
            services.AddScoped<ICreateTabHandler, TabHandler>();

            //services.AddScoped<IPromotionHandler>();

           

            // Infra - Identity
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
            //services.AddScoped<IUser, AspNetUser>();
        }
    }
}
