using Kursach.Database;
using Kursach.Objects;
using Kursach.Objects.Models;

namespace Kursach.ViewModels;

internal class CashWindowViewModel : ViewModelBase
{
    public CashWindowViewModel()
    {
        Cash.Instance.CashRecords = SqlModel.GetInstance().SelectCashRecords();
    }
    public Cash Cash { get; } = Cash.Instance;
}