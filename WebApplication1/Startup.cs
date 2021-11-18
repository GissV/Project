
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication1
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContextPool<Models.AppDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("ConexionSQL")));
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o => o.LoginPath = new PathString("/account/login"));
            services.AddSession();
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            //services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)

        {

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Lista}/{id?}");
            });
        

        //RotativaConfiguration.Setup(env);

        /*app.UseMvc(router =>
        {

            router.MapRoute(
            name: "default",
            template: "{controller=Login}/{action=Login}/{id?}");

        });*/

    }
}
}
