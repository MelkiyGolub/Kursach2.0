using Kursach.Objects;
using Kursach.Objects.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kursach.ViewModels;

public class SkladViewModel : ViewModelBase
{
    public SkladViewModel()
    {
        OrderDetailCommand = new(o =>
        {
            if (!Cash.Instance.Pay(TotalPrice, $"Заказана деталь '{SelectedDetail.Type}' в количестве {Amount} на сумму {TotalPrice:0.###} ₽")) return;

            Details[Types.IndexOf(SelectedDetail!.Type!)].Amount += Amount;

            SelectedDetail = null;
            TotalPrice = 0;
            Amount = 0;

            sklad.Instance.UpdateList();

        }, b => Amount > 0 && SelectedDetail is not null);

        ShowOrdersCommand = new(o => 
        {
            var window = new OrderDetailWindow
            {
                DataContext = this
            };
            window.ShowDialog();
        });
    }
    private const string HYPER_X = "HyperX";

    public ObservableCollection<Detail> Details { get; set; } = new()
    {
        new()
        {
            Type = "Мышка",
            Model = HYPER_X,
            Price = 2000
        },
        new()
        {
            Type = "Клавиатура",
            Model = HYPER_X,
            Price = 3000
        },
        new()
        {
            Type = "Монитор",
            Model = HYPER_X,
            Price = 20000
        },
        new()
        {
            Type = "Системный блок",
            Model = "AliExpress PC",
            Price = 1500
        },
        new()
        {
            Type = "Видеокарта",
            Model = "RTX 2080ti",
            Price = 56546
        },
        new()
        {
            Type = "Мат. Плата",
            Model = "Asus",
            Price = 20000
        },
        new()
        {
            Type = "Процессор",
            Model = "Intel core i7",
            Price = 25000
        },
        new()
        {
            Type = "Оперативная память",
            Model = HYPER_X,
            Price = 8000
        },
        new()
        {
            Type = "Блок питания",
            Model = "Palit",
            Price = 15000
        },
        new()
        {
            Type = "SSD",
            Model = "Kindgston",
            Price = 17000
        },
        new()
        {
            Type = "Кулер",
            Model = "AliExpress PC",
            Price = 500
        },
        new()
        {
            Type = "Наушники",
            Model = HYPER_X,
            Price = 3000
        }
    };

    public List<string> Types { get; } = new()
    {
        "Мышка",
        "Клавиатура",
        "Монитор",
        "Системный блок",
        "Видеокарта",
        "Мат. Плата",
        "Процессор",
        "Оперативная память",
        "Блок питания",
        "SSD",
        "Кулер",
        "Наушники"
    };

    public Command ShowOrdersCommand { get; } 
    public Command OrderDetailCommand { get; }

    public int _amount;
    public int Amount
    {
        get => _amount;
        set
        {
            _amount = value;

            if (SelectedDetail != null)
                TotalPrice = SelectedDetail!.Price * value;
        }
    }
    private Detail? _seletedDetail;
    public Detail? SelectedDetail
    {
        get => _seletedDetail;
        set
        {
            _seletedDetail = value;

            if (value != null)
                TotalPrice = value!.Price * Amount;
        }
    }

    public int TotalPrice { get; private set; }
}