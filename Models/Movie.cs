using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Models;

[Table("movies")]
public partial class Movie
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(45)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("type")]
    [StringLength(45)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    [Column("director")]
    [StringLength(45)]
    [Unicode(false)]
    public string? Director { get; set; }

    [Column("summary")]
    [StringLength(45)]
    [Unicode(false)]
    public string? Summary { get; set; }

    [Column("length")]
    public int? Length { get; set; }

    [Column("image_url")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ImageUrl { get; set; }

    [InverseProperty("Movie")]
    public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}
