using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            services.AddMvc()
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var connectionString = Configuration["connectionStrings:shoppingListDBConnectionString"];
            services.AddDbContext<ShoppingListContext>(o => o.UseSqlServer(connectionString));

            // register the repository
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
            services.AddScoped<IShoppingItemRepository, ShoppingItemRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, ShoppingListContext shoppingListContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.ShoppingList, Models.ShoppingListDto>();
                //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Id))
                //.ForMember(dest => dest.LastEditedBy, opt => opt.MapFrom(src => src.LastEditedBy.Id));
                cfg.CreateMap<Models.ShoppingListDto, Entities.ShoppingList>();

                cfg.CreateMap<Entities.ShoppingItem, Models.ShoppingItemDto>();
                cfg.CreateMap<Models.ShoppingItemDto, Entities.ShoppingItem>();

                cfg.CreateMap<Models.ShoppingListForCreationDto, Entities.ShoppingList>();
                cfg.CreateMap<Entities.ShoppingListItem, Models.ShoppingListItemDto>();
            });

            shoppingListContext.EnsureSeedDataForContext();

            app.UseMvc();
        }
    }
}