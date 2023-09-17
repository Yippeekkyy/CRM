using Microsoft.EntityFrameworkCore;
using TemplateAspNet.Database;

namespace TemplateAspNet
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


           AddDbContext(services);



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
