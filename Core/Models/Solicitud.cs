using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BYCS.Core.Models
{
    [Index(nameof(codigo), IsUnique = true)]
    public class Solicitud
    {
        [Key]
        public int id_solicitud { get; set; }
        public int id_usuario { get; set; }
        public string codigo { get; set; } = null!;
        public string ci { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public string estado { get; set; } = "Activo";
        [ForeignKey("id_usuario")]
        [JsonIgnore]
        public Usuario Usuario { get; set; } = null!;
        public ICollection<SolicitudProtocolo>? SolicitudProtocolos { get; set; }
        public List<Respuesta>? Respuestas { get; set; } = null!;
    }
}
