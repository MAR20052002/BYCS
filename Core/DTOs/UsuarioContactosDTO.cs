namespace BYCS.Core.DTOs
{
    public class UsuarioContactosDTO
    {
        public ICollection<UsuarioDTO> Usuario { get; set; } = null!;
        public ICollection<TelefonoDTO>? Telefono { get; set; }
        public ICollection<CorreoDTO>? Correo { get; set; }
    }
}
