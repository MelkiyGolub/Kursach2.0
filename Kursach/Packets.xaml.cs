using Kursach.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для Packets.xaml
    /// </summary>
    public partial class Packets : Window
    {
        public Packets()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        public List<Tarif> Tarify { get; set; } = new()
        {
        new()
        {
           Title = "Обычный",
           Price = 60,
           Time = 1
        },
        new()
        {
            Title = "3 Часа",
            Price = 150,
            Time = 3
        },
         new()
         {
            Title = "Дотерский",
           Price = 250,
           Time = 5
         },

        new()
        {
         Title = "10 Часов",
         Price = 550,
         Time = 10
        },

        new()
        {
         Title = "Киберспортсмен",
         Price = 650,
         Time = 12
        },

        new()
        {
            Title = "Сутки напролёт",
           Price = 1200,
           Time = 24
        }
    };
    }
}
