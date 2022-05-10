using Kursach.Objects;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Kursach;
public partial class ComputerControl : UserControl, INotifyPropertyChanged
{
    public ComputerControl()
    {
        InitializeComponent();
        DataContext = this;

        Timer.Interval = new(0, 0, 1);
        Timer.Tick += Timer_Tick;
        TimeTextBlock.Text = TimeSpan.Zero.ToString();

        PayCommand = new(o => new Grabej(this).ShowDialog(),
                         b => Status.Equals("Свободен"));
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        Time = Time.Add(TimeSpan.FromSeconds(-1));

        TimeTextBlock.Text = Time.ToString();

        if (Time == TimeSpan.Zero)
        {
            Timer.Stop();
            Status = "Свободен";
            statusCanvas.Background = Brushes.Green;
        }
    }

    public DispatcherTimer Timer { get; } = new();
    public TimeSpan Time { get; set; }

    public int Number { get; set; }
    
    public string Status { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Command PayCommand { get; }
}