using CSnakes.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PoseCompare;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        //SetupPython();
        ApplicationConfiguration.Initialize();
        Application.Run(new FormSimple());
        //Application.Run(new FormCompare());
        //Application.Run(new Form3D());
    }

    internal static IPythonEnvironment Python { get; private set; }

    static void SetupPython()
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                var home = Path.Join(Environment.CurrentDirectory, ".");
                services.WithPython()
                    .FromFolder(@"C:\Users\byd\AppData\Local\Programs\Python\Python312", "3.12.5")
                    .WithHome(home)
                    .WithVirtualEnvironment(Path.Join(home, ".venv"))
                    .WithPipInstaller();
            });

        var app = builder.Build();
        var env = app.Services.GetRequiredService<IPythonEnvironment>();
        Python = env;
    }
}