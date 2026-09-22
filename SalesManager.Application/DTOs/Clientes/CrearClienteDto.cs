using System.ComponentModel.DataAnnotations;

namespace SalesManager.Application.DTOs.Clientes;

public class CrearClienteDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
    [StringLength(150, ErrorMessage = "El email no puede tener más de 150 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "El teléfono no puede tener más de 20 caracteres.")]
    public string Telefono { get; set; } = string.Empty;
}