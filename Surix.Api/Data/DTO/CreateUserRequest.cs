using System.ComponentModel.DataAnnotations;

namespace Surix.Api.Data.DTO;

public class CreateUserRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}