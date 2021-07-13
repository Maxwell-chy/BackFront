using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SqlSugar.IOC;
using StuManage.Repository;
using StuManage.Service;

namespace StuManage.JWT
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
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StuManage.JWT", Version = "v1" });
            });
            #region SqlSugarIOC
            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = MySetting.Mysetting.conStr,
                DbType = IocDbType.Oracle,
                IsAutoCloseConnection = true
            });
            #endregion
            services.AddScoped<CustomerRepository>();
            services.AddScoped<CustomerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StuManage.JWT v1"));
            }

            app.UseRouting();
            app.UseCors(
                options => options.WithOrigins(MySetting.Mysetting.conStr, "http://localhost:8080", "http://localhost:8081", "http://localhost:48735").AllowAnyMethod().AllowAnyHeader()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
