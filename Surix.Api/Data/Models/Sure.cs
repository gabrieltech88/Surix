using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

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
    public float oddA { get; set; }
    [Required]
    public float oddB { get; set; }
    [Required]
    public float Stake { get; set; }
    
    // 🔗 Chave estrangeira para o usuário
    public string UserId { get; set; }

    // 🔗 Propriedade de navegação para o usuário
    public User User { get; set; }

}
