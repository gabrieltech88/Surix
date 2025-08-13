using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Surix.Api.Data.Models;

public class Surebet
{
    public string Evento { get; set; }
    public string Mercado1 { get; set; }
    public string Casa1 { get; set; }
    public string Odd1 { get; set; }
    public string Mercado2 { get; set; }
    public string Casa2 { get; set; }
    public string Odd2 { get; set; }
    public string Categoria { get; set; }
    public string Campeonato { get; set; }
    public string Data { get; set; }
    public string Hora { get; set; }
}