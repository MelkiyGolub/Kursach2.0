using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kursach.Objects.Models;

[Table("cashrecords")]
public class CashRecord : Model
{
    public CashRecord(int sum, string comment)
    {
        Sum = sum;
        Comment = comment;
        DateTime = DateTime.Now;
    }

    [Column("Sum")]
    public int Sum { get; private init; }

    [Column("Date")]
    public DateTime DateTime { get; init; } 

    [Column("Comment")]
    public string Comment { get; private init; }
}