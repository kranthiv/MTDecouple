using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MTDecoupling.Framework;
using MTDecoupling.Framework.Command;
using MTDecoupling.Framework.CommandHandlers;
using MTDecoupling.Framework.Contract;

namespace MTDecoupling
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<ICommandHandler<IEmail>, EmailHandler>();
            services.AddHostedService<WebHosting>();

            services.AddMassTransit(c =>
            {
                //Open generics is not supported for this
                //Maybe, Merge this with ReceiveEndpoint method and write an extension to register both
                c.AddConsumer(typeof(CommandConsumer<IEmail>));

                c.AddBus(sp =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(f =>
                    {
                        var h = f.Host("localhost", "/", hf =>
                        {
                            hf.Username("guest");
                            hf.Password("guest");
                        });

                        f.ReceiveEndpoint(h,"sample", e =>
                        {
                            e.Consumer<CommandConsumer<IEmail>>(sp);

                        });
                    });
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
