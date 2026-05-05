using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface IUsuarioTelefonoRepositorio
    {
        Task<List<UsuarioTelefonoDTO>> GetUsuarioTelefono(string ci, string telf);
        Task<List<UsuarioTelefonoDTO>> GetUsuarioTelefono();
        Task<List<UsuarioTelefonoDTO>> GetUsuarioTelefonoBorrados();
        Task<UsuarioTelefonoDTO> PostUsuarioTelefono(string ci, string telf, string fecha_inicio, string? fecha_fin);
        Task<UsuarioTelefonoDTO> PutUsuarioTelefono(string ci, string telf, string fecha_inicio, string? fecha_fin);
        Task<UsuarioTelefonoDTO> DeleteUsuarioTelefono(string ci, string telf);
        Task<UsuarioTelefonoDTO?> HabilitarUsuarioTelefono(string ci, string telf);
    }
}
