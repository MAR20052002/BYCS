using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BYCS.Core.Models
{
    [Index(nameof(codigo_protocolo), nameof(codigo_solicitud), nameof(fecha_inicio), IsUnique = true)]
    public class SolicitudProtocolo
    {
        [Key]
        public int id_protocolo { get; set; }
        [Key]
        public int id_solicitud { get; set; }
        public string codigo_protocolo { get; set; } = null!;
        public string codigo_solicitud { get; set; } = null!;
        public DateOnly fecha_inicio { get; set; }
        public DateOnly fecha_fin { get; set; }
        public string estado { get; set; } = "Activo";
        [ForeignKey("id_protocolo")]
        [JsonIgnore]
        public Protocolo Protocolo { get; set; } = null!;
        [ForeignKey("id_solicitud")]
        [JsonIgnore]
        public Solicitud Solicitud { get; set; } = null!;
    }
}
