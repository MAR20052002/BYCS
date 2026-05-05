using BYCS.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYCS.Core.DTOs
{
    public class SolicitudDTO
    {
        public string codigo { get; set; } = null!;
        public string ci { get; set; } = null!;
        public string descripcion { get; set; } = null!;
    }
}
