using Kursach.Objects;
using Kursach.Objects.Models;

namespace Kursach.ViewModels;

internal class CashWindowViewModel : ViewModelBase
{
    public Cash Cash { get; } = Cash.Instance;
}