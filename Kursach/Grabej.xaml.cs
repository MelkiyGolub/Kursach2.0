using Kursach.Objects;
using Kursach.Objects.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Kursach;

public partial class Grabej : Window, INotifyPropertyChanged
{
    public Grabej(ComputerControl control)
    {
        InitializeComponent();
        DataContext = this;

        PayCommand = new(o =>
        {
            Cash.Instance.RecieveMoney(SelectedTarif.Price, $"Компьютер номер {control.Number} был занят для игры по тарифу '{SelectedTarif.Title}'");

            control.Time = TimeSpan.FromHours(SelectedTarif.Time);
            control.Timer.Start();
            control.Status = "Занят";
            control.statusCanvas.Background = Brushes.Red;

            Statictic.Clients++;

            Hide();
        });
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    private decimal _hours;
    public decimal Hours
    {
        get => _hours;
        set
        {
            _hours = value;

            if (SelectedTarif is not null)
                Price = SelectedTarif.Price;
        }
    }

    public decimal Price { get; set; }

    private Tarif _selectedTarif;
    public Tarif SelectedTarif
    {
        get => _selectedTarif;
        set
        {
            _selectedTarif = value;
            Hours = value.Time;
        }
    }

    public Tarif[] Tarify { get; set; } = new Tarif[]
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

    public Command PayCommand { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;
}

public class Tarif
{
    public string Title { get; set; }
    public int Price { get; set; }
    public int Time { get; set; }
}