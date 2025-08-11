using System.ComponentModel.DataAnnotations;

namespace Surix.Api.Data.DTO;

public class SureCreateRequest
{
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
}