using global:: Letter.Models;
using global:: Microsoft.AspNetCore.Builder;
using global:: Microsoft.AspNetCore.Hosting;
using global:: Microsoft.AspNetCore.HttpsPolicy;
using global:: Microsoft.Extensions.Configuration;
using global:: Microsoft.Extensions.DependencyInjection;
using global:: Microsoft.Extensions.Hosting;
using global:: System;
using global:: System.Collections.Generic;
using global:: System.Linq;
using global::System.Threading.Tasks;
using global::RegexCheckLibrary;
using global::Microsoft.EntityFrameworkCore;
using global::System.Security.Claims;
using Letter.Dal.Interfaces;
using Letter.Dal.Repositories;
using Letter.Service.Implementations;
using Letter.Service.Interfaces;
using Letter.Dal;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Letter.Controllers;

namespace Letter
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
            services.AddSignalR();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatService, ChatService>();

            services.AddScoped<IUserInChatRepository, UserInChatRepository>();
            services.AddScoped<IUserInChatService, UserInChatService>();

            services.AddScoped<IChatMessagesRepository, ChatMessagesRepository>();
            services.AddScoped<IChatMessagesService, ChatMessagesService>();

            services.AddScoped<IPrivateChatsRepository, PrivateChatsRepository>();
            services.AddScoped<IPrivateChatsService, PrivateChatsService>();

            services.AddScoped<IPrivateChatMessagesRepository, PrivateChatMessagesRepository>();
            services.AddScoped<IPrivateChatMessagesService, PrivateChatMessagesService>();

            services.AddScoped<IChatHub, ChatHub>();
            
            var connection = Configuration.GetConnectionString("Default");
            services.AddDbContextPool<ApplicationDBContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option=>
            {
                option.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login/Log");
                option.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Login/Log");
                
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".MyApp.Session";
                options.IdleTimeout = TimeSpan.FromHours(672);
                options.Cookie.IsEssential = true;
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();     // авторизация

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapPost("NewMessageFromAPI", async (HttpContext context) =>
                {

                });

                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
