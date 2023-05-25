using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CustomerEntities.CustomerEntities>(optionsAction: options =>
                                    options.UseSqlServer(configRoot.GetConnectionString("CustomerDB")));
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddRazorPages();

        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerAPI v1");
            });
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.Run();
        }
    }
}
