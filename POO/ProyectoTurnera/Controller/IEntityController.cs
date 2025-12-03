using System.Collections.Generic;

public interface IEntityController<T> where T : class, new()
{
    List<T> ObtenerTodos();
    T ObtenerPorId(int id);
    int Crear(T entidad);
    void Actualizar(T entidad);
    void Eliminar(int id);
}