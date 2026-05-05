namespace BYCS.Core.DTOs
{
    public class RespuestaInformeDTO
    {
        public string codigo_solicitud { get; set; } = null!;
        public string ci { get; set; } = null!;
        public string codigo_informe { get; set; } = null!;
        public DateOnly fecha { get; set; }
    }
}
