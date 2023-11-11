
using System.Net;
using System.Text;
using Microsoft.OpenApi.Models;
using Booking.API.Repos;
using Microsoft.AspNetCore.Http;
using Booking.API.Context;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Booking.API.Interfaces.Repos;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using log4net;
using System.Reflection;
using log4net.Config;



namespace Booking
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        //private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo(Configuration.GetSection("appSettings:log4netFile").Value));

                // BookingConst.loadConfigData();

                
                services.AddDbContext<BookingContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DBConnectionString")));

                

                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking API", Version = "v1",Description="Swagger for Booking system " });
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new List<string>()
                        }
                    });
                });

                services.AddCors();
        
                // services.AddAutoMapper(typeof(Startup));
                services.AddScoped<IProductRepository, ProductRepository>();
               

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                                .GetBytes("sukey")),
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ValidIssuer = "aye",
                        };
                    });

                 try
                {
                    //Quartz
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionScopedJobFactory();
                        // Just use the name of your job that you created in the Jobs folder.
                        var jobKey = new JobKey("WaitListRefund");
                        q.AddJob<BirthdayNoti>(opts => opts.WithIdentity(jobKey));

                        q.AddTrigger(opts => opts
                            .ForJob(jobKey)
                            .WithIdentity("WaitListRefund-trigger")
                            //This Cron interval can be described as "run every minute" (when second is zero)
                            .WithCronSchedule("0 0 09 ? * *")
                        );
                    });
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                }
                catch(Exception e)
                {
                    log.Error("" + e.Message);
                    throw e;
                }
            

                

                services.AddControllers();
                services.AddHttpContextAccessor();
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 

                

                //payemnt
                services.AddMvcCore();
                services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {           
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            //context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //swagger login
            //app.UseSwaggerBasicAuthorized();
            //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SecureSwagger v1"));
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("v1/swagger.json", "API");
                c.RoutePrefix = "swagger";
            });
        }
    }
}
