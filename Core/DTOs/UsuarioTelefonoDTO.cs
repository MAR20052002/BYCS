namespace BYCS.Core.DTOs
{
    public class UsuarioTelefonoDTO
    {
        public string ci { get; set; } = null!;
        public string telf { get; set; } = null!;
        public DateOnly fecha_inicio { get; set; }
        public DateOnly? fecha_fin { get; set; }
    }
}
