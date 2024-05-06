namespace GateEntryExit_Umbraco.Dtos.Gate
{
    public class GetAllDto
    {
        public int MaxResultCount { get; set; }

        public int SkipCount { get; set; }

        public string Sorting { get; set; }
    }
}
