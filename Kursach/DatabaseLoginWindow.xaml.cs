using Kursach.Objects;
using Kursach.Properties;
using System.Windows;

namespace Kursach
{
    /// <summary>
    /// Interaction logic for DatabaseLoginWindow.xaml
    /// </summary>
    public partial class DatabaseLoginWindow : Window
    {
        public DatabaseLoginWindow()
        {
            InitializeComponent();
            DataContext = this;

            LoginCommand = new(o =>
            {
                Settings.Default.Save();

                Hide();

                Application.Current.MainWindow = new MainWindow();
                Application.Current.MainWindow.Show();

            }, b => !string.IsNullOrEmpty(Login) &&
                    !string.IsNullOrEmpty(Password) &&
                    !string.IsNullOrEmpty(Server));
        }

        public string Login
        {
            get => Settings.Default.user;
            set => Settings.Default.user = value;
        }

        public string Password
        {
            get => Settings.Default.password;
            set => Settings.Default.password = value;
        }

        public string Server
        {
            get => Settings.Default.server;
            set => Settings.Default.server = value;
        }

        public Command LoginCommand { get; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
