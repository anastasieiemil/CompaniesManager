using CompanyManagement.Core.Parsers.Commons;
using CompanyManagement.Data;
using CompanyManagement.Data.Repositories;
using CompanyManagement.Data.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;


namespace CompanyManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<CompanyManagementDbContext>(options => options.UseSqlServer(
                       Configuration.GetConnectionString("DefaultConnection")));

            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "__RequestVerificationToken";
                options.HeaderName = "X-CSRF";
            });

            services.AddTransient<IDALAccessor, DALAccessor>();
            services.AddSingleton<ParserFactory>();

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


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller}/{action}"
                 );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
