namespace BYCS.Core.DTOs
{
    public class UsuarioTelefonosDTO
    {
        public ICollection<UsuarioDTO> Usuario { get; set; } = null!;
        public ICollection<TelefonoDTO>? Telefono { get; set; }
    }
}
