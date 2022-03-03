using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Storage.Configuration;
using ToDoList.Storage.Repositories.Mock;
using ToDoList.Storage.Repositories.MongoDB;
using ToDoList.Storage.Repository;

namespace ToDoList.API
{
    public class Startup
    {
        private const string CorsPolicy = "allowAll";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<ITodoListRepoitory, TodoListRepositoryMock>();
            services.AddSingleton<ITodoListRepoitory, TodoListRepositoryMock>();
            //services.AddScoped<IMongoDBConfiguration, MongoDBConfiguration>();
            //services.AddScoped<ITodoListRepoitory, TodoListRepositoryMongo>();

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoList.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList.API v1"));
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
