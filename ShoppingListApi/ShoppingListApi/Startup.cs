﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using ShoppingListApi.Data;
using ShoppingListApi.Entities;
using ShoppingListApi.Services;

namespace ShoppingListApi
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddSignalR();

            var connectionString = Configuration["connectionStrings:shoppingListDBConnectionString"];
            services.AddDbContext<ShoppingListContext>(o => o.UseSqlServer(connectionString));

            // register the repository
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
            services.AddScoped<IShoppingItemRepository, ShoppingItemRepository>();
            services.AddScoped<IShoppingListItemRepository, ShoppingListItemRepository>();
            services.AddScoped<IShoppingListUserRepository, ShoppingListUserRepository>();

            services.AddScoped<IShoppingListHub, ShoppingListHub>();

            //AddIdentityServer registers the IdentityServer services in DI. It also registers an in-memory store for runtime state. This is useful for development scenarios. For production scenarios you need a persistent or shared store like a database or cache for that. See the EntityFramework quickstart for more information.
            //The AddDeveloperSigningCredential extension creates temporary key material for signing tokens. Again this might be useful to get started, but needs to be replaced by some persistent key material for production scenarios.
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                //.AddClientStore<MyClientStore>()
                .AddInMemoryClients(Config.GetClients());
            //.AddTestUsers(Config.GetUsers());

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "shoppingListApi";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, ShoppingListContext shoppingListContext)
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<ShoppingListHub>("ShoppingListHub");
            });

            //In Configure the middleware is added to the HTTP pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true
                //});
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);
                        }

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.ShoppingList, Models.ShoppingListDto>();
                //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Id))
                //.ForMember(dest => dest.LastEditedBy, opt => opt.MapFrom(src => src.LastEditedBy.Id));
                cfg.CreateMap<Models.ShoppingListDto, Entities.ShoppingList>();

                cfg.CreateMap<Entities.ShoppingItem, Models.ShoppingItemDto>();
                cfg.CreateMap<Models.ShoppingItemDto, Entities.ShoppingItem>();

                cfg.CreateMap<Entities.User, Models.UserDto>();
                //.ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                //    $"{src.FirstName} {src.LastName}"));

                cfg.CreateMap<Models.ShoppingListForCreationDto, Entities.ShoppingList>();
                cfg.CreateMap<Entities.ShoppingListItem, Models.ShoppingListItemDto>();

                cfg.CreateMap<Models.ShoppingListForEditDto, Entities.ShoppingList>();
                cfg.CreateMap<Models.ShoppingItemForEditDto, Entities.ShoppingItem>();
            });

            shoppingListContext.EnsureSeedDataForContext();

            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.UseMvc();
        }
    }
}