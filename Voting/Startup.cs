using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;

namespace Voting
{
    public class Startup
    {
        #region Private Properties

        private readonly IConfiguration _configuration;

        #endregion

        #region Constructors

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<VotingContext>(
                options => options.UseSqlServer(
                    _configuration.GetConnectionString("SqlServer"), 
                    b => b.MigrationsAssembly("Voting")
                    )
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}"); });
        }
    }
}