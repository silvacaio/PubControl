using DGPub.Application.Tabs.Handlers;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Tabs.Handlers;
using DGPub.Domain.Tabs.Repositories;
using Microsoft.Extensions.DependencyInjection;
using DGPub.Infra.Data.Repositories.Tabs;
using DGPub.Infra.Data.Repositories.Items;
using DGPub.Infra.Data.Context;
using DGPub.Infra.Data.UoW;
using DGPub.Domain.Core;
using DGPub.Application.Promotions.Handlers;
using DGPub.Domain.Promotions.Handlers;

namespace DGPub.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Repositories
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemTabRepository, ItemTabRepository>();
            services.AddScoped<ITabRepository, TabRepository>();
            services.AddScoped<DGPubContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Handlers
            services.AddScoped<IAddItemTabHandler, AddItemTabHandler>();
            services.AddScoped<ITabHandler, TabHandler>();
            services.AddScoped<IPromotionHandler, PromotionHandler>();

            services.AddScoped<IPromotion, BeerWithJuicePromotion>();       

            // Infra - Identity
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
            //services.AddScoped<IUser, AspNetUser>();
        }
    }
}
