using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Common;
using System.Text;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Repository.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Web
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
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login/";
                });
            SYS_DATA.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            var SecretKey = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
            new MyService(services);
            new MyRepository(services);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60 * 24);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            new MyService(services);
            new MyRepository(services);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                    ValidateIssuer = true,
                    //Usually this is your application base URL - JRozario
                    ValidIssuer = SYS_DATA.URL,
                    ValidateAudience = true,
                    //Here we are creating and using JWT within the same application. In this case base URL is fine - JRozario
                    //If the JWT is created using a web service then this could be the consumer URL - JRozario
                    ValidAudience = SYS_DATA.URL,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePages(async context =>
                {
                    var request = context.HttpContext.Request;
                    var response = context.HttpContext.Response;

                    if (context.HttpContext.Response.StatusCode == 401)
                    {
                        response.Redirect("/DashBoard?msg=401");
                    }
                    if (context.HttpContext.Response.StatusCode == 403)
                    {
                        response.Redirect("/DashBoard?msg=403");
                    }
                    if (context.HttpContext.Response.StatusCode == 400)
                    {
                        response.Redirect("/Home/Login");
                    }
                    if (context.HttpContext.Response.StatusCode == 404)
                    {
                        response.Redirect("/Home/Error404");
                    }
                    if (context.HttpContext.Response.StatusCode == 405)
                    {
                        response.Redirect("/Home/Error405");
                    }
                });
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseSession();
            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
            });
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = new DbContext_Sql();
                context.Database.Migrate();
            }
        }
    }
}
