using HouseholdChemistry.Shared;
using HouseholdChemistry.Shared.EmailManagement;

namespace HouseholdChemistry.Client.Services.MailService
{
    public interface IEmailService
    {
        Task<ServiceResponse<List<GetDailyDataInfoDTO>>> GetDurationEmailDataObjects(string customerCode, DateTime startDate, DateTime endDate);
        Task<ServiceResponse<List<GetDailyDataInfoDTO>>> GetDailyDataObjects(string customerCode, DateTime requestDate);
    }
}
