namespace BYCS.Core.DTOs
{
    public class UsuarioCorreoDTO
    {
        public string ci { get; set; } = null!;
        public string correo { get; set; } = null!;
        public DateOnly fecha_inicio { get; set; }
        public DateOnly? fecha_fin { get; set; }
    }
}
