using ContactsBackEnd.DATA.Repositories;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;

namespace ContactsBackEnd.WEBAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IContactsRepository, ContactsRepository>();


        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
