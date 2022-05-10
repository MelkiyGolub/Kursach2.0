using System.Windows;

namespace Kursach
{
    /// <summary>
    /// Interaction logic for CashWindow.xaml
    /// </summary>
    public partial class CashWindow : Window
    {
        public CashWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
