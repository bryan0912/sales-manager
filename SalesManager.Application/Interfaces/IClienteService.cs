using SalesManager.Application.DTOs.Clientes;

namespace SalesManager.Application.Interfaces;

public interface IClienteService
{
    Task<List<ClienteResponseDto>> ObtenerTodosAsync();
    Task<ClienteResponseDto?> ObtenerPorIdAsync(int id);
    Task<ClienteResponseDto> CrearAsync(CrearClienteDto dto);
    Task<ClienteResponseDto?> ActualizarAsync(int id, ActualizarClienteDto dto);
    Task<bool> EliminarAsync(int id);
}