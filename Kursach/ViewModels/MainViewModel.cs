using Kursach.Database;
using Kursach.Objects;
using System.Diagnostics;
using System.Windows;

namespace Kursach.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            SqlModel.GetInstance().InitializeCash();
        }

        public Command ExitCommand { get; } = new(o => Application.Current.Shutdown());
        public Command HideCommand { get; } = new(o => Application.Current.MainWindow.WindowState = WindowState.Minimized);

        public Command ShowSkladCommand { get; } = new(o => sklad.Instance.ShowDialog());

        public Command ShowCashCommand { get; } = new(o => new CashWindow().ShowDialog());

        public Command ShowPacketsCommand { get; } = new(o => new Packets().ShowDialog());

        public Command ShowStatisticCommand { get; } = new(o => new Statistic ().ShowDialog());
        public Command TelegaCommand { get; } = new(o =>
        {
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = "http://t.me/Night_bred"
            });
        });
    }
}