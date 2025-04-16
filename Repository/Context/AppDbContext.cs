using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace RH.API.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
    {

    }
    public DbSet<Pensamentos>? pensamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}