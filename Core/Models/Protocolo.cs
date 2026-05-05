using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BYCS.Core.Models
{
    [Index(nameof(codigo), IsUnique = true)]
    public class Protocolo
    {
        [Key]
        public int id_protocolo { get; set; }
        public string codigo { get; set; } = null!;
        public string titulo { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public string estado { get; set; } = "Activo";
        public ICollection<SolicitudProtocolo>? SolicitudProtocolos { get; set; }
    }
}
