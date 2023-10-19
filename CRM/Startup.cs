using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyCRM.Authorize;
using MyCRM.Database;
using MyCRM.Repositories;

namespace MyCRM
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }


        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddControllers();
            services.AddEndpointsApiExplorer();
       //     services.AddMediatR(typeof(Startup));
            services.AddSwaggerGen();
            services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"));

            var secretKey = Configuration.GetSection("JwtOptions:SecretKey").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true
                };
            });

            AddDbContext(services);
            services.AddTransient<WaiterRepository>();


            

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(builder =>
            {

                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void AddDbContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("local");
  

            services.AddDbContext<MainDbContext>(options =>
            options.UseSqlServer(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information));

        }
    }
}
