using GateEntryExit_Umbraco.Dtos.Gate;

namespace GateEntryExit_Umbraco.Services.Interfaces
{
    public interface IGateService
    {
        Task<GetAllGatesDto> GetAllAsync(int pageNumber = 1);
    }
}
