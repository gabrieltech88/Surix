using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Surix.Api.Data.Models;

public class User : IdentityUser
{ 
    public string Name { get; set; }
}
