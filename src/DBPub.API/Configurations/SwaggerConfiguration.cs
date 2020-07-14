using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DBPub.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "UpServices API",
                    Description = "API UpServices",
                    TermsOfService = "Nenhum",
                    Contact = new Contact { Name = "Desenvolvedor X", Email = "email@Services.io", Url = "http://Services.io" },
                    License = new License { Name = "MIT", Url = "http://Services.io/licensa" }
                });

                s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }
    }
}
