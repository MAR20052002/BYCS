using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BYCS.Core.Models
{
    [Index(nameof(telf), IsUnique = true)]
    public class Telefono
    {
        [Key]
        public int id_telefono { get; set; }
        public string telf { get; set; } = null!;
        public string estado { get; set; } = "Activo";
        public ICollection<UsuarioTelefono>? UsuarioTelefonos { get; set; }
    }
}
