namespace BYCS.Core.DTOs
{
    public class UsuarioTelfSumaDTO
    {
        public string ci { get; set; } = null!;
        public int cantidadTelefonos { get; set; }
        public int sumaCoeficientes { get; set; }
        public ICollection<TelefonoDTO>? Telefonos { get; set; }
    }
}
