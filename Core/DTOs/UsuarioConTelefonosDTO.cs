namespace BYCS.Core.DTOs
{
    public class UsuarioTelfCountDTO
    {
        public string ci { get; set; } = null!;
        public int cantidadTelefonos { get; set; }
        public ICollection<TelefonoDTO>? Telefonos {get; set;}
    }
}
