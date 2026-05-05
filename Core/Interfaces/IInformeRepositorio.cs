using BYCS.Core.DTOs;

namespace BYCS.Core.Interfaces
{
    public interface IInformeRepositorio
    {
        Task<InformeDTO> GetInforme(string codigo_informe);
        Task<List<InformeDTO>> GetInforme();
        Task<List<InformeDTO>> GetInformeBorrados();
        Task<InformeDTO> PostInforme(string codigo_informe, string titulo, string descripcion, string url);
        Task<InformeDTO> PutInforme(string codigo_informe, string titulo, string descripcion, string url);
        Task<InformeDTO> DeleteInforme(string codigo_informe);
        Task<InformeDTO?> HabilitarInforme(string codigo_informe);
    }
}
