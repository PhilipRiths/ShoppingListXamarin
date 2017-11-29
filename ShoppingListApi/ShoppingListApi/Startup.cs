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
            services.AddMvc();

            var connectionString = Configuration["connectionStrings:shoppingListDBConnectionString"];
            services.AddDbContext<ShoppingListContext>(o => o.UseSqlServer(connectionString));

            // register the repository
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
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
            });

            shoppingListContext.EnsureSeedDataForContext();

            app.UseMvc();
        }
    }
}