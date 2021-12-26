using LottoMk2.App.Features.UpdateData;
using LottoMk2.App.Views;
using LottoMk2.Data;
using LottoMk2.Data.Services;
using LottoMk2.Services.LottoService;

using Microsoft.EntityFrameworkCore;
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
            using (var mainForm = scope.ServiceProvider.GetRequiredService<Splash>())
            {
                Application.Run(mainForm);
            }            
        }            
    }

    public static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(DefaultValues.DefaultConnectionString, sqliteOptions =>
            {
                sqliteOptions.MigrationsAssembly(typeof(LottoMk2.Data.Sqlite.PlaceHolder).Assembly.FullName);
            });
        });

        services.AddHttpClient();
        services.AddScoped<LottoService>();
        services.AddScoped<LottoDataService>();
        services.AddScoped<UpdateDataService>();

        services.AddScoped<Splash>();        

        services.AddAutoMapper(
            typeof(PlaceHolder).Assembly, 
            typeof(LottoMk2.Data.Services.PlaceHolder).Assembly);

        return services;
    }
}