using Kursach.Database;
using Kursach.Objects;
using Kursach.Objects.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kursach.ViewModels;

public class SkladViewModel : ViewModelBase
{
    public SkladViewModel()
    {
        var details = SqlModel.GetInstance().SelectDetails();

        if (details.Count == 0)
        {
            foreach (var d in Details)
                SqlModel.GetInstance().Insert(d);
        }
        else
            Details = details;

        OrderDetailCommand = new(o =>
        {
            if (!Cash.Instance.Pay(TotalPrice, $"Заказана деталь '{SelectedDetail.Type}' в количестве {Amount} на сумму {TotalPrice} ₽")) return;

            Details[Types.IndexOf(SelectedDetail!.Type!)].Amount += Amount;

            SqlModel.GetInstance().Update(Details[Types.IndexOf(SelectedDetail!.Type!)]);

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

        ShowTakeDetailsCommand = new(o =>
        {
            var window = new VzyatDetal()
            {
                DataContext = this
            };
            window.ShowDialog();
        });

        TakeDetailCommand = new(o =>
        {
            Details[Types.IndexOf(SelectedDetail!.Type!)].Amount -= Amount;

            SqlModel.GetInstance().Update(Details[Types.IndexOf(SelectedDetail!.Type!)]);

            SelectedDetail = null;
            TotalPrice = 0;
            Amount = 0;

            sklad.Instance.UpdateList();

        }, b => SelectedDetail is not null && Amount > 0 && SelectedDetail.Amount >= Amount);
    }
    private const string HYPER_X = "HyperX";

    public List<Detail> Details { get; } = new()
    {
        new()
        {
            Type = "Мышка",
            Model = HYPER_X,
            Price = 2000,
            Number = 1
        },
        new()
        {
            Type = "Клавиатура",
            Model = HYPER_X,
            Price = 3000,
            Number = 2
        },
        new()
        {
            Type = "Монитор",
            Model = HYPER_X,
            Price = 20000,
            Number = 3
        },
        new()
        {
            Type = "Системный блок",
            Model = "AliExpress PC",
            Price = 1500,
            Number = 4
        },
        new()
        {
            Type = "Видеокарта",
            Model = "RTX 2080ti",
            Price = 56546,
            Number = 4
        },
        new()
        {
            Type = "Мат. Плата",
            Model = "Asus",
            Price = 20000,
            Number = 5
        },
        new()
        {
            Type = "Процессор",
            Model = "Intel core i7",
            Price = 25000,
            Number = 6
        },
        new()
        {
            Type = "Оперативная память",
            Model = HYPER_X,
            Price = 8000,
            Number = 7
        },
        new()
        {
            Type = "Блок питания",
            Model = "Palit",
            Price = 15000,
            Number = 8
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
            Price = 500,
            Number = 9
        },
        new()
        {
            Type = "Наушники",
            Model = HYPER_X,
            Price = 3000,
            Number = 10
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
    public Command TakeDetailCommand { get; }
    public Command ShowTakeDetailsCommand { get; }

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