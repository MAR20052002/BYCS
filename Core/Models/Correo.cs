using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYCS.Core.Models
{
    [Index(nameof(correo), IsUnique = true)]
    public class Correo
    {
        [Key]
        public int id_correo { get; set; }
        public string correo { get; set; } = null!;
        public string estado { get; set; } = "Activo";
        public ICollection<UsuarioCorreo>? UsuarioCorreos { get; set; }
    }
}
