using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using historianalarmservice.Data;
using historianalarmservice.Service;
using historianalarmservice.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace historianalarmservice {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddTransient<IThingService, ThingService> ();
            services.AddTransient<IAlarmService, AlarmService> ();
            services.AddTransient<IHistorianAlarmsService, HistorianAlarmsService> ();
            services.AddCors (o => o.AddPolicy ("CorsPolicy", builder => {
                builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ();
            }));

            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseNpgsql (Configuration.GetConnectionString ("HistorianAlarmDB")));
            services.AddMvc ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            app.UseCors ("CorsPolicy");
            app.UseForwardedHeaders (new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseMvc ();
        }
    }
}