using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TicketService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Insertar(Tickets ticket)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tickets.Add(ticket);

        return await contexto.SaveChangesAsync() > 0;

    }

    private async Task<bool> Modificar(Tickets ticket)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.Update(ticket);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var EliminarTicket = await contexto.Tickets
            .Where(t => t.ClienteId == id).ExecuteDeleteAsync();

        return EliminarTicket > 0;
    }

    public async Task<bool> Existe(int ticketId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Tickets.AnyAsync(t => t.TicketId == ticketId);
    }

    public async Task<bool> Guardar(Tickets ticket)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (!await Existe(ticket.TicketId))
        {
            return await Insertar(ticket);
        }
        else
        {
            return await Modificar(ticket);
        }
    }

    public async Task<Tickets?> Buscar(int id = 0, string? prioridad = null)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (id > 0)
        {
            return await contexto.Tickets.AsNoTracking().
            FirstOrDefaultAsync(c => c.TicketId == id);
        }
        else if (!string.IsNullOrEmpty(prioridad))
        {
            return await contexto.Tickets.AsNoTracking().
            FirstOrDefaultAsync(c => c.Prioridad.ToLower().Equals(prioridad.ToLower()));
        }
        return null;
    }

    public async Task<bool> ExistePrioridad(string prioridad, int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AnyAsync(c =>
            (c.Prioridad == prioridad) &&
            c.TicketId != id
        );
    }

    public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Tickets.AsNoTracking().Include(t => t.tecnico).Include(t => t.clientes).Where(criterio).ToListAsync();

    }

    public async Task<List<Tickets>> ListarTickets()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AsNoTracking().ToListAsync();
    }

    

}