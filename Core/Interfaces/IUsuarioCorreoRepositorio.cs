using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface IUsuarioCorreoRepositorio
    {
        Task<List<UsuarioCorreoDTO>> GetUsuarioCorreo(string ci, string correo);
        Task<List<UsuarioCorreoDTO>> GetUsuarioCorreo();
        Task<List<UsuarioCorreoDTO>> GetUsuarioCorreoBorrados();
        Task<UsuarioCorreoDTO> PostUsuarioCorreo(string ci, string correo, string fecha_inicio, string? fecha_fin);
        Task<UsuarioCorreoDTO> PutUsuarioCorreo(string ci, string correo, string fecha_inicio, string? fecha_fin);
        Task<UsuarioCorreoDTO> DeleteUsuarioCorreo(string ci, string correo);
        Task<UsuarioCorreoDTO?> HabilitarUsuarioCorreo(string ci, string correo);
    }
}
