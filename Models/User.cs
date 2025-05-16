using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cinema.Models;

[Table("users")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(32)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(45)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("email")]
    [StringLength(32)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("create_time", TypeName = "datetime")]
    public DateTime? CreateTime { get; set; }

    [Column("role")]
    [StringLength(45)]
    [Unicode(false)]
    public string Role { get; set; } = null!;

    [InverseProperty("Costumer")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
