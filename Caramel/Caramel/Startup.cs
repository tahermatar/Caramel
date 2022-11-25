using AutoMapper;
using Caramel.Core.Mangers.BlogManger;
using Caramel.Core.Mangers.CommonManger;
using Caramel.Core.Mangers.ResturantManager;
using Caramel.Core.Mangers.RoleManger;
using Caramel.Data;
using Caramel.EmailService;
using Caramel.Extenstions;
using Caramel.Factory;
using CarProject.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Collections.Generic;
using System.Text;

namespace Caramel
{
    public class Startup
    {
        private MapperConfiguration _mapperConfiguration;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            _mapperConfiguration = new MapperConfiguration(a => {
                a.AddProfile(new Mapping());
            });

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var emailConfig = Configuration
                              .GetSection("EmailConfiguration")
                              .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);

            services.AddDbContext<CaramelDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CaramelConnection")));


            /*
            services.AddScoped<IUserManger, UserManger>();
            services.AddScoped<ICommonManager, CommonManager>();
            services.AddScoped<IBlogManager, BlogManager>();
            services.AddScoped<IResturantManager, ResturantManager>();
            services.AddScoped<IRoleManger, RoleManger>();*/

            services.AddSingleton(sp => _mapperConfiguration.CreateMapper());

            services.AddLogging();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Caramel", Version = "v1" });
                c.OperationFilter<SwaggerDefaultValues>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please insert Bearer JWT token into field. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
                {
                    option.SaveToken = true;
                    option.RequireHttpsMetadata = false;
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = Configuration["Jwt:Issuar"],
                        ValidIssuer = Configuration["Jwt:Issuar"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            ApiFactory.RegisterDependencies(services);


        }
        //services.AddSwaggerGen(options =>
        //{
        //    options.AddSecurityDefinition(name: "Caramel", securityScheme: new OpenApiSecurityScheme
        //    {
        //        Name = "Authorization",
        //        Type = SecuritySchemeType.ApiKey,
        //        Scheme = "Caramel",
        //        BearerFormat = "JWT",
        //        In = ParameterLocation.Header,
        //        Description = "Enter your JWT Key",
        //    });
        //    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        //    {
        //         {
        //             new OpenApiSecurityScheme
        //             {
        //                 Reference =new OpenApiReference
        //                 {
        //                     Type = ReferenceType.SecurityScheme,
        //                     Id ="Bearer",
        //                 },
        //                 Name = "Bearer",
        //                 In = ParameterLocation.Header,
        //             },
        //                 new List<string>()
        //             }
        //         });
        //    });
        //}


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Caramel v1"));
            }

            Log.Logger = new LoggerConfiguration()
                    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Minute)
                    .CreateLogger();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer { Url = $"{Configuration.GetSection("Domain").Value}" }
                    };
                });
            });

            app.ConfigureExceptionHandler(Log.Logger, env);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
