using ContactsBackEnd.DATA.Repositories;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ContactsBackEnd.WEBAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().Configure<MvcOptions>(options =>
            {
                var jsonFormatter = new JsonOutputFormatter
                {
                    SerializerSettings =
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    }
                };

                options.OutputFormatters.RemoveTypesOf<JsonOutputFormatter>();
                options.OutputFormatters.Insert(0, jsonFormatter);

            });

            services.AddTransient<IContactsRepository, ContactsRepository>();

            services.ConfigureCors(options =>
            {
                options.AddPolicy(
                    "CorsTutaureliaNet",
                    builder =>
                    {
                        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                    });
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            app.UseCors("CorsTutaureliaNet");

            app.UseMvc();
        }
    }
}
