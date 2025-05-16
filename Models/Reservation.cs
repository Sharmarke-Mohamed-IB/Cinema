using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Models;

[Table("reservations")]
public partial class Reservation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("costumer_id")]
    public int CostumerId { get; set; }

    [Column("number_of_seats")]
    public int? NumberOfSeats { get; set; }

    [Column("scr_id")]
    public int? ScrId { get; set; }

    [ForeignKey("CostumerId")]
    [InverseProperty("Reservations")]
    public virtual User Costumer { get; set; } = null!;

    [ForeignKey("ScrId")]
    [InverseProperty("Reservations")]
    public virtual Screening? Scr { get; set; }
}
