using SalesManager.Application.DTOs.Clientes;
using SalesManager.Application.Interfaces;
using SalesManager.Domain.Entities;

namespace SalesManager.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ClienteResponseDto>> ObtenerTodosAsync()
    {
        var clientes = await _repository.ObtenerTodosAsync();
        return clientes.Select(MapearADto).ToList();
    }

    public async Task<ClienteResponseDto?> ObtenerPorIdAsync(int id)
    {
        var cliente = await _repository.ObtenerPorIdAsync(id);
        return cliente is null ? null : MapearADto(cliente);
    }

    public async Task<ClienteResponseDto> CrearAsync(CrearClienteDto dto)
    {
        if (await _repository.ExisteEmailAsync(dto.Email))
        {
            throw new InvalidOperationException($"Ya existe un cliente con el email '{dto.Email}'.");
        }

        var cliente = new Cliente
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            Telefono = dto.Telefono,
            Activo = true
        };

        var creado = await _repository.CrearAsync(cliente);
        return MapearADto(creado);
    }

    public async Task<ClienteResponseDto?> ActualizarAsync(int id, ActualizarClienteDto dto)
    {
        var existente = await _repository.ObtenerPorIdAsync(id);

        if (existente is null)
        {
            return null;
        }

        if (await _repository.ExisteEmailAsync(dto.Email, excluirId: id))
        {
            throw new InvalidOperationException($"Ya existe otro cliente con el email '{dto.Email}'.");
        }

        existente.Nombre = dto.Nombre;
        existente.Email = dto.Email;
        existente.Telefono = dto.Telefono;
        existente.Activo = dto.Activo;

        var actualizado = await _repository.ActualizarAsync(existente);
        return MapearADto(actualizado);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var existente = await _repository.ObtenerPorIdAsync(id);

        if (existente is null)
        {
            return false;
        }

        await _repository.EliminarAsync(existente);
        return true;
    }

    private static ClienteResponseDto MapearADto(Cliente cliente)
    {
        return new ClienteResponseDto
        {
            Id = cliente.Id,
            Nombre = cliente.Nombre,
            Email = cliente.Email,
            Telefono = cliente.Telefono,
            Activo = cliente.Activo
        };
    }
}