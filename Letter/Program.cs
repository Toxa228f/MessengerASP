using global:: Microsoft.AspNetCore.Hosting;
using global:: Microsoft.Extensions.Configuration;
using global:: Microsoft.Extensions.Hosting;
using global:: Microsoft.Extensions.Logging;
using global:: System;
using global:: System.Collections.Generic;
using global:: System.Linq;
using global:: System.Threading.Tasks;

namespace Letter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
