using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<UsuarioDTO> GetUsuario(string ci);
        Task<List<UsuarioDTO>> GetUsuario();
        Task<List<UsuarioDTO>> GetUsuarioBorrados();
        Task<UsuarioDTO> PostUsuario(string ci, string nombre, string? apellido_p, string? apellido_m);
        Task<UsuarioDTO> PutUsuario(string ci, string nombre, string? apellido_p, string? apellido_m);
        Task<UsuarioDTO> DeleteUsuario(string ci);
        Task<UsuarioDTO?> HabilitarUsuario(string ci);
        Task<UsuarioTelefonosDTO?> GetUsuarioTelfCountDTO(string ci);
        Task<UsuarioCorreosDTO?> GetUsuarioConCorreos(string ci);
        Task<UsuarioContactosDTO?> GetUsuarioContactos(string ci);
        Task<List<UsuarioTelfCountDTO>>? GetCantidadTelefonosPorUsuario();
        Task<UsuarioTelfCountDTO>? GetCantidadTelefonosPorUsuario(string ci);
        Task<List<UsuarioTelfSumaDTO>>? GetSumaTelefonosPorUsuario();
        Task<List<UsuarioDTO>>? GetUsuariosSinTelefonos();
    }
}
