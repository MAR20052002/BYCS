namespace BYCS.Core.DTOs
{
    public class UsuarioCorreosDTO
    {
        public ICollection<UsuarioDTO> Usuario { get; set; } = null!;
        public ICollection<CorreoDTO>? Correo { get; set; }
    }
}
