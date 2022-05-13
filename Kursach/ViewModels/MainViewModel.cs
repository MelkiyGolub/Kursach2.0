using Kursach.Database;
using Kursach.Objects;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace Kursach.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            SqlModel.GetInstance().InitializeCash();

            _timer.Interval = new(0, 1, 0);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object? sender, System.EventArgs e)
        {
            CurrentTime = System.DateTime.Now.ToShortTimeString();
        }

        private readonly DispatcherTimer _timer = new();

        public Command ExitCommand { get; } = new(o => Application.Current.Shutdown());
        public Command HideCommand { get; } = new(o => Application.Current.MainWindow.WindowState = WindowState.Minimized);

        public Command ShowSkladCommand { get; } = new(o => sklad.Instance.ShowDialog());

        public Command ShowCashCommand { get; } = new(o => new CashWindow().ShowDialog());

        public Command ShowPacketsCommand { get; } = new(o => new Packets().ShowDialog());

        public Command ShowStatisticCommand { get; } = new(o => new Statistic().ShowDialog());

        public Command ShowSettingsCommand { get; } = new(o => new SettingsWindow().ShowDialog());

        public string CurrentTime { get; private set; } = System.DateTime.Now.ToShortTimeString();

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