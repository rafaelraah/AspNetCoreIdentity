using AspNetCoreIdentity.Areas.Identity.Data;
using AspNetCoreIdentity.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Config
{
    public static class IdentityConfig
    {
        

        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization(
                   options =>
                   {
                       options.AddPolicy(name: "PodeExcluir", configurePolicy: policy => policy.RequireClaim("PodeExcluir"));

                       options.AddPolicy(name: "PodeLer", configurePolicy: policy => policy.Requirements.Add(new PermissaoNecessaria(permissao: "PodeLer")));
                       options.AddPolicy(name: "PodeEscrever", configurePolicy: policy => policy.Requirements.Add(new PermissaoNecessaria(permissao: "PodeEscrever")));
                       options.AddPolicy(name: "PodeGravar", configurePolicy: policy => policy.Requirements.Add(new PermissaoNecessaria(permissao: "PodeGravar")));
                   }
               );

            return services;
        }

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AspNetCoreIdentityContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("AspNetCoreIdentityContextConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<AspNetCoreIdentityContext>();

            return services;
        }

    }
}
