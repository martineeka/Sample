using System.Text;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Chaka.Api.Security
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //perintah hidayat
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.AddAutoMapper();
            services.AddDbContext<ChakaContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Registering DI Services
            services.RegisterPhoenixDependencies();


            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
            });


            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
           .AddCookie()
           .AddOpenIdConnect(options =>
           {
               options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
               options.Authority = appSettings.Authority;
               options.SignedOutRedirectUri = "/connect/back";
               options.ClientSecret = "secret";
               options.ClientId = "client";
               options.SaveTokens = true;
               options.GetClaimsFromUserInfoEndpoint = true;
               options.RequireHttpsMetadata = false;

               options.Scope.Add("Employee");
               //options.Events = new OpenIdConnectEvents
               //{
               //    OnTokenValidated = context =>
               //    {
               //        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
               //        var userId = int.Parse(context.Principal.Identity.Name);
               //        var user = userService.GetById(userId);
               //        if (user == null)
               //        {
               //             // return unauthorized if user no longer exists
               //             context.Fail("Unauthorized");
               //        }
               //        return Task.CompletedTask;
               //    }
               //};
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc();

            app.UseAuthentication();

        }
    }
}
