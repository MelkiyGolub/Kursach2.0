using System.Windows;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для sklad.xaml
    /// </summary>
    public partial class sklad : Window
    {
        private sklad()
        {
            InitializeComponent();
        }

        public static sklad Instance { get; } = new();

        public void UpdateList()
        {
            DetailsList.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
