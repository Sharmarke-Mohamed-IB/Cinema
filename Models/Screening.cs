using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Models;

[Table("screenings")]
public partial class Screening
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("movie_id")]
    public int MovieId { get; set; }

    [Column("hall_id")]
    public int HallId { get; set; }

    [Column("datetime_of_play", TypeName = "datetime")]
    public DateTime? DatetimeOfPlay { get; set; }

    [ForeignKey("HallId")]
    [InverseProperty("Screenings")]
    public virtual Hall Hall { get; set; } = null!;

    [ForeignKey("MovieId")]
    [InverseProperty("Screenings")]
    public virtual Movie Movie { get; set; } = null!;

    [InverseProperty("Scr")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
