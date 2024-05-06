using GateEntryExit_Umbraco.Dtos.GateEmployee;

namespace GateEntryExit_Umbraco.Services.Interfaces
{
    public interface IGateEmployeeService
    {
        Task<AllGateEmployeeDto> GetAllAsync(int pageNumber = 0);
    }
}
