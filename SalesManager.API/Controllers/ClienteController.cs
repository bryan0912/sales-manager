using Microsoft.AspNetCore.Mvc;
using SalesManager.Application.DTOs.Clientes;
using SalesManager.Application.Interfaces;

namespace SalesManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClienteResponseDto>>> ObtenerTodos()
    {
        var clientes = await _clienteService.ObtenerTodosAsync();
        return Ok(clientes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClienteResponseDto>> ObtenerPorId(int id)
    {
        var cliente = await _clienteService.ObtenerPorIdAsync(id);

        if (cliente is null)
        {
            return NotFound(new { mensaje = $"No se encontró el cliente con id {id}." });
        }

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteResponseDto>> Crear([FromBody] CrearClienteDto dto)
    {
        try
        {
            var creado = await _clienteService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ClienteResponseDto>> Actualizar(int id, [FromBody] ActualizarClienteDto dto)
    {
        try
        {
            var actualizado = await _clienteService.ActualizarAsync(id, dto);

            if (actualizado is null)
            {
                return NotFound(new { mensaje = $"No se encontró el cliente con id {id}." });
            }

            return Ok(actualizado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _clienteService.EliminarAsync(id);

        if (!eliminado)
        {
            return NotFound(new { mensaje = $"No se encontró el cliente con id {id}." });
        }

        return NoContent();
    }
}