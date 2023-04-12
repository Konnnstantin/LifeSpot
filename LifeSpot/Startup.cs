using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LifeSpot
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            string footerHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "footer.html"));
            string sideBarHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "sideBar.html"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "index2.html");

                    var html = new StringBuilder(await File.ReadAllTextAsync(viewPath))
                    .Replace("<!--SIDEBAR-->", sideBarHtml)
                    .Replace("<!--FOOTER-->", footerHtml); 

                    await context.Response.WriteAsync(html.ToString());
                });

                endpoints.MapGet("/Static/CSS/Index.css", async context =>
                 {
                     var cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "CSS", "Index.css");
                     var css = await File.ReadAllTextAsync(cssPath);
                     await context.Response.WriteAsync(css);
                 });
                endpoints.MapGet("/Static/JS/Index.js", async context =>
                {
                    var jsPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "JS", "Index.js");
                    var js = await File.ReadAllTextAsync(jsPath);
                    await context.Response.WriteAsync(js);
                });
                endpoints.MapGet("/about", async context =>
                 {
                     var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "about.html");

                     var html = new StringBuilder(await File.ReadAllTextAsync(viewPath))
                    .Replace("<!--SIDEBAR-->", sideBarHtml)
                    .Replace("<!--FOOTER-->", footerHtml);

                     await context.Response.WriteAsync(html.ToString());
                 });
                endpoints.MapGet("/Static/JS/about.js", async context =>
                {
                    var jsPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "JS", "about.js");
                    var js = await File.ReadAllTextAsync(jsPath);
                    await context.Response.WriteAsync(js);
                });
            });
        }

    }
}