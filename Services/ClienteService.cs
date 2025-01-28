using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

    public class ClienteService(IDbContextFactory<Contexto> DbFactory)
    {

    private async Task<bool> Insertar(Clientes cliente)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Clientes.Add(cliente);

        return await contexto.SaveChangesAsync() > 0;

    }

    private async Task<bool> Modificar(Clientes cliente)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.Update(cliente);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var EliminarCliente = await contexto.Clientes
            .Where(t => t.ClienteId == id).ExecuteDeleteAsync();

        return EliminarCliente > 0;
    }

    public async Task<bool> Existe(int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Clientes.AnyAsync(t => t.ClienteId == clienteId);
    }

    public async Task<bool> Guardar(Clientes clientes)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (!await Existe(clientes.ClienteId))
        {
            return await Insertar(clientes);
        }
        else
        {
            return await Modificar(clientes);
        }
    }

    public async Task<Clientes?> Buscar(int id = 0, string? nombre = null)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (id > 0)
        {
            return await contexto.Clientes.AsNoTracking().
            FirstOrDefaultAsync(c => c.ClienteId == id);
        }
        else if (!string.IsNullOrEmpty(nombre))
        {
            return await contexto.Clientes.AsNoTracking().
            FirstOrDefaultAsync(c => c.ClienteNombres.ToLower().Equals(nombre.ToLower()));
        }
        return null;
    }

    public async Task<bool> ExisteNombreCliente(string rnc, string nombre, int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AnyAsync(c =>
            (c.Rnc == rnc || c.ClienteNombres == nombre) &&
            c.ClienteId != id
        );
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

         return await contexto.Clientes.AsNoTracking().Include(c =>c.tecnico).Where(criterio).ToListAsync();
            
    }

    public async Task<List<Clientes>> ListarClientes()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AsNoTracking().ToListAsync();
    }

    public async Task<Clientes?> Buscar(string criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (int.TryParse(criterio, out int id))
        {
            return await contexto.Clientes
                .FirstOrDefaultAsync(c => c.ClienteId == id);
        }

        return await contexto.Clientes
            .FirstOrDefaultAsync(c => c.Rnc == criterio || c.ClienteNombres.Contains(criterio));
    }

}

