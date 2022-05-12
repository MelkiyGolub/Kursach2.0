using System.ComponentModel.DataAnnotations.Schema;

namespace Kursach.Objects.Models;

[Table("details")]
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

    [Column("Number")]
    public int Number { get; init; }
}