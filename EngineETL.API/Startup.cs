using EngineETL.API.Model;
using EngineETL.Core.Domain.Interfaces.Repository;
using EngineETL.Core.Domain.Interfaces.Service;
using EngineETL.Core.Domain.Services;
using EngineETL.Infrastructure.Data.Context;
using EngineETL.Infrastructure.Data.Repository;
using EngineETL.Tools.Formatters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace EngineETL.API
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
            services.AddMvc(options => {
                options.InputFormatters.Insert(0, new XDocumentInputFormatter());
            })
            .AddXmlSerializerFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            
            var appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton<TokenGenerator>();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "Engine ETL" }));

            services.AddDbContext<EngineETLContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("context")).EnableSensitiveDataLogging());


            #region Repository
                services.AddScoped<IExpectedFormatRepository, ExpectedFormatRepository>();
                services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Services
                services.AddScoped<IExpectedFormatService, ExpectedFormatService>();
                services.AddScoped<IUserService, UserService>();
            #endregion


        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Engine ETL");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }


}
