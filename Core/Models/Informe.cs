using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BYCS.Core.Models
{
    [Index(nameof(codigo_informe), IsUnique = true)]
    public class Informe
    {
        [Key]
        public int id_informe { get; set; }
        public string codigo_informe { get; set; } = null!;
        public string titulo { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public string url { get; set; } = null!;
        public string estado { get; set; } = "Activo";
        public ICollection<RespuestaInforme>? RespuestaInformes { get; set; }
    }
}
