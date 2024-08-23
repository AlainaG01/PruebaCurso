using Microsoft.EntityFrameworkCore;
using PrimerProyecto.DAL;
using PrimerProyecto.Models;
using System.Linq.Expressions;

namespace PrimerProyecto.Services;

public class EstudianteService
{
    private readonly Contexto _contexto;

    public EstudianteService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Guardar(Estudiantes estudiantes)
    {
        if(! await Existe(estudiantes.EstudianteId))
            return await Insertar(estudiantes);
        else
            return await Modificar(estudiantes);
    }

    private async Task<bool> Modificar(Estudiantes estudiantes)
    {
        _contexto.Update(estudiantes);
        var modificado = await _contexto.SaveChangesAsync() > 0;
        _contexto.Entry(estudiantes).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        return modificado;
    }

    private async Task<bool> Insertar(Estudiantes estudiantes)
    {
        _contexto.Estudiantes.Add(estudiantes);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int estudianteId)
    {
      return await _contexto.Estudiantes
            .AnyAsync(e => e.EstudianteId == estudianteId);

    }

    public async Task<bool> ExisteEstudiante(int estudianteId, string nombre, string matricula)
    {
        return await _contexto.Estudiantes.AnyAsync(e => e.EstudianteId != estudianteId 
        && e.Nombre.ToLower().Equals(nombre.ToLower()) || e.Matricula.Equals(matricula));
    } 
    public async Task<Estudiantes> Buscar(int id)
    {
        return await _contexto.Estudiantes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EstudianteId == id);
    }

    public async Task<bool> Eliminar(Estudiantes estudiante)
    {
        return await _contexto.Estudiantes.AsNoTracking().Where(e => e.EstudianteId == estudiante.EstudianteId).ExecuteDeleteAsync() > 0;
    } 

    public async Task<List<Estudiantes>> Listar(Expression<Func<Estudiantes, bool>> criterio)
    {
        return await _contexto.Estudiantes .AsNoTracking().Where(criterio).ToListAsync();
    }
}
