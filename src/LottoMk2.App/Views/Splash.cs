using LottoMk2.Data;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using LottoMk2.Data.Services;
using LottoMk2.App.Features.UpdateData;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
 
        progressBar1.Style = ProgressBarStyle.Marquee;
        progressBar1.MarqueeAnimationSpeed = 50;

        Load += Splash_Load;        
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
            this.Close();
        }
    }

    private readonly LottoDataService dataService;
    private readonly UpdateDataService updateDataService;
}
