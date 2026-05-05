namespace BYCS.Core.DTOs
{
    public class RespuestaDTO
    {
        public string codigo_solicitud { get; set; } = null!;
        public string ci { get; set; } = null!;
        public bool aprobado { get; set; }
        public DateOnly fecha { get; set; }
    }
}
