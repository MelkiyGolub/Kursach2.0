namespace Kursach.Objects.Models;

public class Tarif
{
    public string Title { get; set; }
    public int Price { get; set; }
    public int Time { get; set; }

    public override string ToString() => $"Тариф '{Title}'. Доступное игровое время: {Time} часов. Цена: {Price}";
}