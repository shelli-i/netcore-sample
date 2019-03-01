using System;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace netcore_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var build = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = build.Build();

            var reptpath = configuration["report_path"];
            var sqlcon = configuration["ConnectionsString"];
            var fileOut = configuration["filecopy"];
            var sp = configuration["userSP"];

            var builder = new ContainerBuilder();

            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IReportService>();
                app.Write(reptpath, sqlcon, fileOut, sp);
            }
            Console.WriteLine("End program");
            //Console.ReadLine();
        }
    }
}
