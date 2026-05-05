using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BYCS.Core.Models
{
    [Index(nameof(codigo_solicitud), nameof(ci), IsUnique = true)]
    public class Respuesta
    {
        [Key]
        public int id_solicitud { get; set; }
        public int id_usuario { get; set; }
        public string codigo_solicitud { get; set; } = null!;
        public string ci { get; set; } = null!;
        public bool aprobado { get; set; }
        public DateOnly fecha { get; set; }
        public string estado { get; set; } = "Activo";
        [ForeignKey("id_solicitud")]
        [JsonIgnore]
        public Solicitud? Solicitud { get; set; } = null!;
        [ForeignKey("id_usuario")]
        [JsonIgnore]
        public Usuario? Usuario { get; set; } = null!;
        public ICollection<RespuestaInforme>? RespuestaInformes { get; set; }
    }
}
