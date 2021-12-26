using Microsoft.Extensions.DependencyInjection;

namespace LottoMk2.App;

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
        ApplicationConfiguration.Initialize();

        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.SetCompatibleTextRenderingDefault(true);

        var services = new ServiceCollection();

        ConfigureServices(services);

        var provider = services.BuildServiceProvider();

        using (var scope = provider.CreateScope())
        {
            var mainForm = scope.ServiceProvider.GetRequiredService<Form1>();

            Application.Run(mainForm);
        }            
    }

    public static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<Form1>();

        return services;
    }
}