using System.ComponentModel.DataAnnotations;

namespace Surix.Api.Data.DTO;

public class PasswordResetRequest
{
    [Required]
    public string Password { get; set; }
    [Required]
    public string UserName { get; set; }
}