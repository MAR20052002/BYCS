using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BYCS.Core.Models
{
    [Index(nameof(codigo_solicitud), nameof(ci), nameof(codigo_informe), IsUnique = true)]
    public class RespuestaInforme
    {
        [Key]
        public int id_solicitud { get; set; }
        public int id_usuario { get; set; }
        public int id_informe { get; set; }
        public string codigo_solicitud { get; set; } = null!;
        public string ci { get; set; } = null!;
        public string codigo_informe { get; set; } = null!;
        public DateOnly fecha { get; set; }
        public string estado { get; set; } = "Activo";
    }
}
