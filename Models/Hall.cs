using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Models;

[Table("halls")]
public partial class Hall
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("number_of_seats")]
    public int? NumberOfSeats { get; set; }

    [Column("threedee")]
    public bool? Threedee { get; set; }

    [InverseProperty("Hall")]
    public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}
