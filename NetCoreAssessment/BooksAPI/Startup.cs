using Books.Common.Mappers;
using Books.DTO.Book;
using Books.Models.DataContext;
using Books.Services.Interfaces;
using Books.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace BooksAPI
{
    public class Startup
    {
        public IConfiguration iConfig { get; }
        public Startup(IConfiguration iConfig)
        {
            this.iConfig = iConfig;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connString = iConfig.GetConnectionString("DbConnString");
            services.AddDbContextPool<AppDbContext>(o => o.UseSqlServer(connString));
            services.AddScoped<IBookInterface, BookService>();
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(AutoMapping));
            services.AddSingleton<IConfiguration>(iConfig);
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
