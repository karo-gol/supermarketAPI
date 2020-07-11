using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Persistence.Contexts;
using Supermarket.API.Persistence.Repositories;
using Supermarket.API.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Supermarket.API.Mapping;


namespace Supermarket.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<AppDbContext>(options => {
                options.UseInMemoryDatabase("supermarket-api-in-memory");
            });

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddAutoMapper(typeof(ModelToResourceProfile));
            
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });            
        }
    }
}
