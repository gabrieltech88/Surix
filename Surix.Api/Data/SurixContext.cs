using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;   
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AutoMapper;
using Surix.Api.Data.Models;

namespace Surix.Api.Data;

public class SurixContext : IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }
    public SurixContext(DbContextOptions<SurixContext> options) : base(options) { }
}