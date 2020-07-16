using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPub.API.Configurations;
using DGPub.Infra.CrossCutting.Identity.Data;
using DGPub.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DBPub.API
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
            // Contexto do EF para o Identity
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configurações de Autenticação, Autorização e JWT.
            services.AddMvcSecurity(Configuration);

            // Options para configurações customizadas
            services.AddOptions();

            // MVC com restrição de XML e adição de filtro de ações.
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Configurações do Swagger
            services.AddSwaggerConfig();

            NativeInjectorBootStrapper.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {

            #region Configurações MVC

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
            //  app.UseHttpsRedirection();

            #endregion

            #region Swagger

            if (env.IsProduction())
            {
                // Se não tiver um token válido no browser não funciona.
                // Descomente para ativar a segurança.
                // app.UseSwaggerAuthorized();
            }

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "DBPub.IO API v1.0");
            });

            #endregion
        }
    }
}
