using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;   
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AutoMapper;
using Surix.Api.Data.Models;

namespace Surix.Api.Data;

public class SurixContext : IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Sure> Sures { get; set; }
    public SurixContext(DbContextOptions<SurixContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configurar relação 1:N entre User e Sure
        builder.Entity<Sure>()
            .HasOne(s => s.User)
            .WithMany(u => u.Sures)
            .HasForeignKey(s => s.UserId)
            .IsRequired();
    }
}