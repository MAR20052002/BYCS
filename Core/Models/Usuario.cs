using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace BYCS.Core.Models
{
    [Index(nameof(ci), IsUnique = true)]
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }
        public string ci { get; set; } = null!;
        public string nombre { get; set; }  = null!;
        public string? apellido_p { get; set; } = null;
        public string? apellido_m { get; set; } = null;
        public string estado { get; set; } = "Activo";
        public ICollection<UsuarioTelefono>? UsuarioTelefonos { get; set; }
        public ICollection<Solicitud>? Solicitudes { get; set; }
        public ICollection<Respuesta>? Respuestas { get; set; }
    }
}
