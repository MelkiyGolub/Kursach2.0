using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kursach.Objects.Models;

public class Model
{
    [Column("id")]
    public Guid ID { get; set; } = Guid.NewGuid();
}