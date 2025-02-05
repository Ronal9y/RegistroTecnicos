using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;

namespace RegistroTecnicos.Services;

public class SistemaService(IDbContextFactory<Contexto> DbFactory)
{

    private async  Task<bool> Insertar(Sistemas Sistemas)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Sistemas.Add(Sistemas);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Sistemas Sistemas)
    {
        await using var contexto =await DbFactory.CreateDbContextAsync();
        contexto.Update(Sistemas);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var EliminarSistema = await contexto.Sistemas.
        Where(s => s.SistemaId == id).ExecuteDeleteAsync();
        return EliminarSistema > 0;
    }

    public async Task<bool> Existe(int sistemaId)
    {
        await using var contexto=await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AnyAsync(s => s.SistemaId == sistemaId);
    }

    public async Task<bool> Guardar(Sistemas Sistemas)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if(!await Existe(Sistemas.SistemaId))
        {
            return await Insertar(Sistemas);
        }
        else
        {
            return await Modificar(Sistemas);
        }
    }

    public async Task<Sistemas?> Buscar(int id=0, string? descripcion = null)
    {
        await using var contexto=await DbFactory.CreateDbContextAsync();
        if (id > 0) 
        {
            return await contexto.Sistemas.AsNoTracking()
                .FirstOrDefaultAsync(s => s.SistemaId == id);
        }

        else if (!string.IsNullOrEmpty(descripcion))
        {
            return await contexto.Sistemas.AsNoTracking()
                .FirstOrDefaultAsync(s => s.Descripcion == descripcion);

        }
        return null;

    }

    public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas,bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AsNoTracking().Where(criterio).ToListAsync();
    }
    public async Task<List<Sistemas>> ListarSistemas()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AsNoTracking().ToListAsync();
    }

    public async Task<bool> ExisteSistema(int id,string? descripcion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AnyAsync(s => s.SistemaId == id || s.Descripcion == descripcion);
    }
}
