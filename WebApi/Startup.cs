using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlSugar.IOC;
using StuManage.Repository;
using StuManage.Service;
using System;
using System.Text;


namespace StuManage.WebApi
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StuManage.WebApi", Version = "v1" });
                #region swagger使用授权组件
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                #endregion

            });

            #region sqlSugarIOC
            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = MySetting.Mysetting.conStr,
                DbType = IocDbType.Oracle,
                IsAutoCloseConnection = true
            });
            #endregion

            #region IOC依赖注入
            services.AddCustomIOC();
            #endregion

            #region JWT鉴权
            services.AddCustomJWT();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StuManage.WebApi v1"));
            }

            app.UseRouting();
            app.UseCors(
                options => options.WithOrigins(MySetting.Mysetting.conStr, "http://localhost:8080", "http://localhost:8081", "http://localhost:48735").AllowAnyMethod().AllowAnyHeader()
            );
            //添加
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    static class IOCExtend
    {
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            services.AddScoped<StudentRepository>();
            services.AddScoped<StudentService>();
            services.AddScoped<CompetitionRepository>();
            services.AddScoped<CompetitionService>();
            services.AddScoped<ScoreRepository>();
            services.AddScoped<ScoreService>();
            services.AddScoped<ExaminformationRepository>();
            services.AddScoped<ExaminformationService>();
            services.AddScoped<AnnounceRepository>();
            services.AddScoped<AnnounceService>();
            services.AddScoped<UserRepository>();
            services.AddScoped<UserService>();
            return services;
        }
        public static IServiceCollection AddCustomJWT(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GBTR-OEUR1-DCS-UYTGR-SDFGTRE-ES")),
                        ValidateIssuer = true,
                        ValidIssuer = MySetting.Mysetting.issuerURL,
                        ValidateAudience = true,
                        ValidAudience = MySetting.Mysetting.audienceURL,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(60)
                    };
                });
            return services;
        }

    }
}
