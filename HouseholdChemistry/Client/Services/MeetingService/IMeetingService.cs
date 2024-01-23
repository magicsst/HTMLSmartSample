using HouseholdChemistry.Shared;
using HouseholdChemistry.Shared.MeetingManagement;
using HouseholdChemistry.Shared.MeetingManagement.SearchGrid;

namespace HouseholdChemistry.Client.Services.MeetingService
{
    public interface IMeetingService
    {
        Task<ServiceResponse<List<GetMeetingGridInfoDTO>>> GetGridMeetings(string employeeCode, DateTime startDate, DateTime endDate);
    }
}
