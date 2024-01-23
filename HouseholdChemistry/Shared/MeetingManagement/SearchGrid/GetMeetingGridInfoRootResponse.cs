namespace HouseholdChemistry.Shared.MeetingManagement.SearchGrid
{
    public class GetMeetingGridInfoRootResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public string Table { get; set; } = string.Empty;
        public List<GetMeetingGridInfoRowResponse> Rows { get; set; } = new List<GetMeetingGridInfoRowResponse>();
    }
}
