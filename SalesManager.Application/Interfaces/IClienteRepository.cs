using SalesManager.Domain.Entities;

namespace SalesManager.Application.Interfaces;

public interface IClienteRepository
{
    Task<List<Cliente>> ObtenerTodosAsync();
    Task<Cliente?> ObtenerPorIdAsync(int id);
    Task<Cliente> CrearAsync(Cliente cliente);
    Task<Cliente> ActualizarAsync(Cliente cliente);
    Task EliminarAsync(Cliente cliente);
    Task<bool> ExisteEmailAsync(string email, int? excluirId = null);
}