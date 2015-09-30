using System;
using System.IO;
using Autofac;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using Serilog;
using Serilog.Events;
using WebApp.Infrastructure.ErrorHandling;
using WebApp.Infrastructure.FluentScheduler;
using WebApp.Infrastructure.Logger;
using WebApp.Infrastructure.SignalR;

namespace WebApp
{
    public class Program
    {
        private static IDisposable _service;
        private static IContainer _container;

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerFactory(LogEventLevel.Debug, "http://localhost:5341").CreateLogger();

            _container = IoC.LetThereBeIoC();
            
            var service = Microsoft.Owin.Hosting.WebApp.Start("http://localhost:8080", app =>
            {
                app.UseAutofacMiddleware(_container);
                app.UseSerilogErrorHandling();

                app.UseCors(CorsOptions.AllowAll);

                app.UseSignalR("/signalr", _container);

                app.Use((context, next) =>
                {
                    var path = context.Request.Path.Value;
                    if (!(path.Contains(".") || path.StartsWith("/api")))
                    {
                        context.Request.Path = new PathString("/index.html");
                    }

                    return next();
                });

                var assetsPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Assets"));

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileSystem = new PhysicalFileSystem(assetsPath)
                });
                
                app.UseWebApi(new ApiHttpConfiguration());
            });

            var scheduler = _container.Resolve<IScheduler>();
            scheduler.Start();

            Console.ReadKey();
        }
    }
}