using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
using RegistroTecnicos.DAL;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;
public class TecnicoService(IDbContextFactory<Contexto> DbFactory)
{

    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tecnicos.Add(tecnico);

        return await contexto.SaveChangesAsync() > 0;

    }

    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.Update(tecnico);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var EliminarTecnico = await contexto.Tecnicos
            .Where(t => t.TecnicoId==id).ExecuteDeleteAsync();

        return EliminarTecnico > 0;
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Tecnicos.AnyAsync(t => t.TecnicoId==id);
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if(! await Existe(tecnico.TecnicoId))
        {
            return await Insertar(tecnico);
        }
        else
        {
            return await Modificar(tecnico);
        }
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Tecnicos.AsNoTracking().
            FirstOrDefaultAsync(t => t.TecnicoId==id);
    }

    public async Task<bool> ExisteNombreTecnico(string nombre, int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Tecnicos.AnyAsync(t => t.Nombres.ToLower().Equals(nombre.ToLower()) &&t.TecnicoId == id);
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>>? filtro = null)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (filtro != null)
            return await contexto.Tecnicos.AsNoTracking().Where(filtro).ToListAsync();

        return await contexto.Tecnicos.AsNoTracking().ToListAsync();
    }


}

