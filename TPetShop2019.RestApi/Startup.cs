using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TPetshop2019.Core.ApplicationServices;
using TPetshop2019.Core.ApplicationServices.Services;
using TPetshop2019.Core.DomainServices;
using TPetShop2019.Infrastructure.SQL;
using TPetShop2019.Infrastructure.SQL.Repositories;

namespace TPetShop2019.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IValidateIdService, ValidateIdService>();

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<PetShopContext>(
                opt => opt.UseSqlite("Data Source=Petshop.db")
                );
            }
            else
            {
                services.AddDbContext<PetShopContext>(
                    opt => opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }

            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.MaxDepth = 3;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
               // FakeDB.InitData();
               using (var scope = app.ApplicationServices.CreateScope())
               {
                   var context = scope.ServiceProvider.GetRequiredService<PetShopContext>();
                   DbInitializer.SeedDb(context);
               }
               app.UseDeveloperExceptionPage();
            }
            else
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<PetShopContext>();
                    //context.Database.EnsureCreated();
                    DbInitializer.SeedDb(context);
                }
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
