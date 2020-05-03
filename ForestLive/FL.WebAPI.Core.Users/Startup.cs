using System;
using System.Text;
using FL.WebAPI.Core.Users.Entity.Database;
using FL.WebAPI.Core.Users.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace FL.WebAPI.Core.Users
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
            IoCApi.AddInjection(services);

            services.AddControllers();

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
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"])),
                   ValidateIssuer = false,
                   ValidateAudience = false,
               };
           });

           AddIdentity(services, Configuration.GetSection("ConnectionStringUsersSite").Get<string>());
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddIdentity(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<Domain.Entities.User, IdentityRole<Guid>>(options =>
            {
                //options.User.AllowedUserNameCharacters = AuthOptions.AllowedUserNameCharacters;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<UserDbContext>();
            //.AddDefaultTokenProviders();

            services.TryAddScoped<SignInManager<Domain.Entities.User>>();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Events.OnRedirectToLogin = context =>
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        return Task.CompletedTask;
            //    };
            //});
        }
    }
}
