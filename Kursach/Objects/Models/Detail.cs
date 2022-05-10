using System.ComponentModel.DataAnnotations.Schema;

namespace Kursach.Objects.Models;

[Table("Details")]
public class Detail : Model
{
    [Column("Type")]
    public string? Type { get; init; }

    [Column("Model")]
    public string? Model { get; init; }

    [Column("Amount")]
    public int Amount { get; set; }

    [Column("Price")]
    public int Price { get; init; }
}