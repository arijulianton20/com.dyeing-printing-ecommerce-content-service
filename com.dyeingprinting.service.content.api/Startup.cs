using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.dyeingprinting.service.content.business;
using com.dyeingprinting.service.content.business.Service;
using com.dyeingprinting.service.content.data;
using com.dyeingprinting.service.content.data.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace com.dyeingprinting.service.content.api
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

            services.AddApiVersioning(options => options.DefaultApiVersion = new ApiVersion(1, 0));
            services.Configure<Storage>(Configuration.GetSection("Storage"));
            services.AddControllers();
            services.AddDbContext<ContentDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IService<MobileContent>, MobileContentService>();
            services.AddTransient<IService<WebContent>, WebContentService>();

            services.AddSwaggerGen();

            #region Cors
            services.AddCors(o => o.AddPolicy("CorePolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithMethods("POST", "GET", "DELETE", "PUT", "OPTIONS");
                //.WithExposedHeaders("Content-Disposition", "api-version", "content-length", "content-md5", "content-type", "date", "request-id", "response-time");
            }));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ContentDbContext>();
                context.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorePolicy");
            app.UseCors(
                options => options.WithOrigins("http://localhost:3000/")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                 .AllowAnyHeader());
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
