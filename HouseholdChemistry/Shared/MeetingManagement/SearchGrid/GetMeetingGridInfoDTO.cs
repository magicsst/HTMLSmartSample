namespace HouseholdChemistry.Shared.MeetingManagement.SearchGrid
{
    public class GetMeetingGridInfoDTO
    {
        public string TaskGID { get; set; }
        public string fTaskSiteGID { get; set; }
        public string fPersonGID { get; set; }
        public string fTaskTypeGID { get; set; }
        public string Code { get; set; }
        public string fParentTaskGID { get; set; }
        public string SalesPersonCode { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime PlannedStartDate1 { get; set; }
        public string Katastasi_Synantisis { get; set; }
        public string fCityCode { get; set; }
        public string Telephone1 { get; set; }
        public string fSiteAddress { get; set; }
        public string FullName { get; set; }
        public string Code_MotherTask { get; set; }
        public string KathgoriaDescription { get; set; }
        public string Omada1_Description { get; set; }
        public bool IsProgrammed { get; set; }
        public bool IsService { get; set; }
        public bool IsTelephoneCall { get; set; }
        public string fSIteCode { get; set; }
        public string fSIteDescription { get; set; }
        public DateTime ActualClosedDate { get; set; }
        public string Url { get; set; }
    }
}
