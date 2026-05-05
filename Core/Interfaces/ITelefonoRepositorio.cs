using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface ITelefonoRepositorio
    {
        Task<TelefonoDTO> GetTelefono(string telf);
        Task<List<TelefonoDTO>> GetTelefono();
        Task<List<TelefonoDTO>> GetTelefonoBorrados();
        Task<TelefonoDTO> PostTelefono(string telf);
        Task<TelefonoDTO> PutTelefono(string telf, string telf_nuevo);
        Task<TelefonoDTO> DeleteTelefono(string telf);
        Task<TelefonoDTO?> HabilitarTelefono(string telf);
    }
}
