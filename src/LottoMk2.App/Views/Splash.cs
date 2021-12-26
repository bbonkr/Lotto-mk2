using LottoMk2.Data;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using LottoMk2.Data.Services;
using LottoMk2.App.Features.UpdateData;

namespace LottoMk2.App.Views;

public partial class Splash : Form
{
    public Splash(LottoDataService dataService ,UpdateDataService updateDataService)
    {
        this.dataService = dataService;
        this.updateDataService = updateDataService;

        InitializeComponent();

        titleLabel.Text = "Lotto Mk. 2";
        messageLabel.Text = "";
        timer = new System.Windows.Forms.Timer();

        timer.Interval = 1000;
        timer.Tick += Timer_Tick;
        timer.Start();

        progressBar1.Minimum = 0;
        progressBar1.Maximum = 100;
        progressBar1.Style = ProgressBarStyle.Continuous;
        
        Load += Splash_Load;

        
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        progressBar1.Increment(progressBar1.Step);
    }

    private async void Splash_Load(object? sender, EventArgs e)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        try
        {
            messageLabel.Text = "Start: database migration";
            // migrations
            await dataService.MigrateAsync(cancellationTokenSource.Token);
            messageLabel.Text = "Completed: database migration";

            // Update lotto data
            messageLabel.Text = "Start: Update data";
            await updateDataService.UpdateAsync(cancellationTokenSource.Token);
            messageLabel.Text = "Completed: Update data";
        }
        catch (Exception ex)
        {
            cancellationTokenSource.Cancel();
            
            Application.Exit();
        }
        finally
        {
            timer.Stop();
        }
    }

    private readonly LottoDataService dataService;
    private readonly System.Windows.Forms.Timer timer;
    private readonly UpdateDataService updateDataService;
}
