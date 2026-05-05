using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface ICorreoRepositorio
    {
        Task<CorreoDTO> GetCorreo(string correo);
        Task<List<CorreoDTO>> GetCorreo();
        Task<List<CorreoDTO>> GetCorreoBorrados();
        Task<CorreoDTO> PostCorreo(string correo);
        Task<CorreoDTO> PutCorreo(string correo, string correo_nuevo);
        Task<CorreoDTO> DeleteCorreo(string correo);
        Task<CorreoDTO?> HabilitarCorreo(string correo);
    }
}
