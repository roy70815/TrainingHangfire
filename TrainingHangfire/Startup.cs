using Hangfire;
using Hangfire.MissionControl;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TrainingHangfire.Common.Helpers.DapperHelper;
using TrainingHangfire.Common.Helpers.DatabaseHelper;
using TrainingHangfire.Repository.Implement;
using TrainingHangfire.Repository.Interface;
using TrainingHangfire.Service.Implement;
using TrainingHangfire.Service.Interface;
using TrainingHangfire.Work.Job.Implement;
using TrainingHangfire.Work.Job.Interface;
using TrainingHangfire.Work.Setting;

namespace TrainingHangfire
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IDataBaseHelper, DataBaseHelper>();
            services.AddTransient<IDapperHelper, DapperHelper>();

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

            var serviceProvider = services.BuildServiceProvider();
            var hangFireConnection = serviceProvider.GetRequiredService<IDataBaseHelper>().GetHangFireConnection();

            services.AddHangfire(
                (provider, x) => x.UseSqlServerStorage(
                    hangFireConnection.ConnectionString,
                    new SqlServerStorageOptions
                    {
                        SchemaName = "HangFire",
                        PrepareSchemaIfNecessary = false
                    }
                )
                .UseMissionControl(typeof(StockScheduleMethod).Assembly)
            );


            services.AddHangfireServer(
                options =>
                new BackgroundJobServerOptions()
                {
                    Queues = new[] { "default" },
                    WorkerCount = 10
                }
            );


            //DI
            services.AddTransient<ICallStockAPIRepository, CallStockAPIRepository>();
            services.AddTransient<IStockDailyQuotesRepository, StockDailyQuotesRepository>();

            services.AddTransient<IStockService, StockService>();

            services.AddTransient<IStockScheduleMethod, StockScheduleMethod>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                IgnoreAntiforgeryToken = true,
                Authorization = new[] { new HangfireAuthorityFilter() }
            });


            //WINDOWS與LINUX時區不一樣
            string timeArea = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Taipei Standard Time" : "Asia/Taipei";
            RecurringJob.AddOrUpdate<IStockScheduleMethod>("StockEveryDayInfo", x => x.GetStockEveryDayInfoAsync(), "0 22 * * 1-5", TimeZoneInfo.FindSystemTimeZoneById(timeArea));
        }
    }
}
