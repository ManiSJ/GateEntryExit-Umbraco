namespace GateEntryExit_Umbraco.Dtos.Gate
{
    public class GetAllGatesDto
    {
        public int TotalCount { get; set; }

        public List<GateDetailsDto> Items { get; set; }
    }
}
