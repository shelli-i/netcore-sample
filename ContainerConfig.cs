using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace netcore_sample
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ReportRepository>().As<IReportRepository>();
            builder.RegisterType<ReportService>().As<IReportService>();
            return builder.Build();
        }
    }
}
