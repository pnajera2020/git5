
using Business;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API_Productos
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        private const string API_CONNECTION = "MyWebApiConection";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApiDBContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString(API_CONNECTION)));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Microservicio de productos",
                    Description = "" +
                    "",
                    License = new OpenApiLicense
                    {
                        Name = "C&A Systems"
                    }
                });
            });

            //20200917
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("*")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod()
                                                  .WithMethods("POST", "PUT", "DELETE", "GET");
                                  });
            });

            services.AddScoped<IBsCategoria, BsCategoria>();
            services.AddScoped<IBsMarca, BsMarca>();
            services.AddScoped<IBsProducto, BsProducto>();
            services.AddScoped<IBsProveedor, BsProveedor>();
            services.AddScoped<IBsUnidadMedida, BsUnidadMedida>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
            });

            app.UseRouting();


            //20200412
            app.UseCors(MyAllowSpecificOrigins);
            //20200412


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
