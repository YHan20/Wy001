using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebApi.Api.Repository;
using WebApi.Api.Fliters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Api.ParamModel;

namespace WebApi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

         private string allowCors="AllowCors";
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(Options=>{
                Options.AddPolicy(allowCors,builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });


            //注册数据库上下文到容器
            services.AddDbContext<WebApi.Api.Db.ShowDb>(o => o.UseSqlServer());
            //注册对数据库的基本操作服务到容器
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            //注册验证器（使用什么配置来验证token）
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                //是否要求使用https
                option.RequireHttpsMetadata = false;
                //是否要保存token
                option.SaveToken = true;
                //使用配置中间件，获得token配置
                var token = Configuration.GetSection("tokenParameter").Get<TokenParameter>();
                //验证器核心属性
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    //是否要验证生成token的秘钥
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.Secret)),
                    //是否要验证发行token的组织或者个人
                    ValidateIssuer = true,
                    ValidIssuer = token.Issuer,
                    //是否验证受众
                    ValidateAudience = false,
                };
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(AuditLogActionFilter));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            // 将token的验证注册到中间件
            app.UseAuthentication();
            
            app.UseAuthorization();

            // 跨域
            app.UseCors(allowCors);
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
