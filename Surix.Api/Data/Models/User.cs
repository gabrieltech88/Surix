using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Surix.Api.Data.Models;

public class User : IdentityUser
{
    [Required]
    public string Name { get; set; }

    // Relacionamento: 1 usuário → muitos sures
    public ICollection<Sure> Sures { get; set; }
}
