using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
namespace RegistroTecnicos.DAL;

    public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {

    }
    public DbSet<Tecnicos> Tecnicos { get; set; }

    public DbSet<Clientes> Clientes { get; set; }

    public DbSet<Ciudades> Ciudades { get; set; }

    public DbSet<Tickets> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tickets>()
            .HasOne(t => t.tecnico)
            .WithMany()
            .HasForeignKey(t => t.TecnicoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Tickets>()
            .HasOne(t => t.clientes)
            .WithMany()
            .HasForeignKey(t => t.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}