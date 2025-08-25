using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surix.Api.Data.Models;

public class Sure
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow.Date;
    [Required]
    public string Event { get; set; }
    [Required]
    public string CasaA { get; set; }
    [Required]
    public string CasaB { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public double oddA { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public double oddB { get; set; }
    [Required]
    public double Stake { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public double? Lucro { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public double? ROI { get; set; }
    
    // 🔗 Chave estrangeira para o usuário
    public string UserId { get; set; }

    // 🔗 Propriedade de navegação para o usuário
    public User User { get; set; }

}
