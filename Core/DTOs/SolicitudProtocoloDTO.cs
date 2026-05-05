namespace BYCS.Core.DTOs
{
    public class SolicitudProtocoloDTO
    {
        public string codigo_protocolo { get; set; } = null!;
        public string codigo_solicitud { get; set; } = null!;
        public DateOnly fecha_inicio { get; set; }
        public DateOnly fecha_fin { get; set; }
    }
}
