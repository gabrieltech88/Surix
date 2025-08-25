using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Surix.Api.Data.Models;

public class RoiPerDay
{
    public int Dia { get; set; }
    public int Mes { get; set; }
    public double SomaRoi { get; set; }
}