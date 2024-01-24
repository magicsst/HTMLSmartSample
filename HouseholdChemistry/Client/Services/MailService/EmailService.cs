using HouseholdChemistry.Shared.EmailManagement;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace HouseholdChemistry.Client.Services.MailService
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _configuration;

        public EmailService(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _configuration = configuration;
        }

        // TODO: Δεν καλείται από πουθενά
        public async Task<ServiceResponse<List<GetDailyDataInfoDTO>>> GetDailyDataObjects(string customerCode, DateTime requestDate)
        {
            GetDailyEmailInfoRootResponse rootDailyEmailsResult = null;
            List<GetDailyDataInfoDTO> getEmailsResponse = null;

            try
            {
                // Αυτό είναι το κλειδί για το Authentication της Entersoft
                _http.DefaultRequestHeaders.Add("X-ESAPIKEY-ECOMCONNECTOR", @_configuration.GetValue<string>("X-ESAPIKEY-ECOMCONNECTOR"));
                // _http.DefaultRequestHeaders.Add("X-ESAPIKEY-ECOMCONNECTOR", @"89dd05d9-b76b-45c9-b1ae-4f761cce2b1c,c8b59b0d-7771-4d25-a161-ae118ee135b8,80100034364");

                // Πρέπει να αφαιρέσω το Header του Authorization γιατι με κόβει το WebAPI της Entersoft
                _http.DefaultRequestHeaders.Authorization = null;

                rootDailyEmailsResult = await _http.GetFromJsonAsync<GetDailyEmailInfoRootResponse>($"https://api.entersoft.gr/api/rpc/PublicQuery/Web_Scrolls/Web_ListOfMeetingsForEmail?SalesPersonCode={customerCode}&PlannedStartDate1={requestDate.Date.ToString("yyyy-MM-dd")}") ?? new GetDailyEmailInfoRootResponse();

                getEmailsResponse = new List<GetDailyDataInfoDTO>();

                foreach (var emailObject in rootDailyEmailsResult.Rows)
                {
                    getEmailsResponse.Add(new GetDailyDataInfoDTO
                    {
                        CustomerName = emailObject.Name,
                        MeetingDateTime = emailObject.PlannedStartDate1,
                        City = emailObject.fCityCode,
                        Phone = emailObject.Telephone1,
                        Address = emailObject.Address1,
                        SharedNotes = emailObject.SharedNotes,
                        Category = emailObject.KathgoriaDescription,
                        TaxRegistrationNumber = emailObject.TaxRegistrationNumber,
                        IsProgrammed = emailObject.IsProgrammed.ToString() == "1" ? true : false,
                        IsService = emailObject.IsService.ToString() == "1" ? true : false,
                        Notes = emailObject.Notes,
                        IsFinalCustomer = emailObject.IsFinalCustomer.ToString() == "1" ? true : false,
                        IsTelephoneCall = emailObject.IsTelephoneCall.ToString() == "1" ? true : false,
                        FinalCustomerName = emailObject.FinalCustomerName
                    });
                }

                return new ServiceResponse<List<GetDailyDataInfoDTO>>
                {
                    Data = getEmailsResponse,
                    Message = getEmailsResponse.Count > 0 ? string.Empty : "Δεν βρέθηκαν αποτελέσματα",
                    Success = true,
                    ErrorCode = getEmailsResponse.Count > 0 ? 0 : 404
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<GetDailyDataInfoDTO>>
                {
                    Data = new List<GetDailyDataInfoDTO>(),
                    Message = $"Το σύστημα δεν μπόρεσε να ανταποκριθεί. {Environment.NewLine}{ex.Message}",
                    Success = false,
                    ErrorCode = 1500
                };
            }
        }

      
        public async Task<ServiceResponse<List<GetDailyDataInfoDTO>>> GetDurationEmailDataObjects(string customerCode, DateTime startDate, DateTime endDate)
        {
            GetDailyEmailInfoRootResponse rootDailyEmailsResult = null;
            List<GetDailyDataInfoDTO> getEmailsResponse = null;

            string dayFrom = startDate.Day.ToString().PadLeft(2, '0');
            string monthFrom = startDate.Month.ToString().PadLeft(2, '0');
            string yearFrom = startDate.Year.ToString();
            string dayTo = endDate.Day.ToString().PadLeft(2, '0');
            string monthTo = endDate.Month.ToString().PadLeft(2, '0');
            string yearTo = endDate.Year.ToString();

            string dateRangeURL = string.Empty;

            int increasedNumber = 0;

            try
            {
                // Αυτό είναι το κλειδί για το Authentication της Entersoft
                _http.DefaultRequestHeaders.Add("X-ESAPIKEY-ECOMCONNECTOR", @_configuration.GetValue<string>("X-ESAPIKEY-ECOMCONNECTOR"));

                // Πρέπει να αφαιρέσω το Header του Authorization γιατι με κόβει το WebAPI της Entersoft
                _http.DefaultRequestHeaders.Authorization = null;

                dateRangeURL = String.Format(@"https://api.entersoft.gr/api/rpc/PublicQuery/Web_Scrolls/Web_ListOfMeetingsForEmail?SalesPersonCode={0}&PlannedStartDate1=ESDateRange(SpecificDate,+%23{1}%2F{2}%2F{3}%23,+SpecificDate,+%23{4}%2F{5}%2F{6}%23)", customerCode, yearFrom, monthFrom, dayFrom, yearTo, monthTo, dayTo);
                // dateRangeURL = String.Format(@"https://api.entersoft.gr/api/rpc/PublicQuery/Web_Scrolls/Web_ListOfMeetingsForEmail?PlannedStartDate1=ESDateRange(SpecificDate,+%23{0}%2F{1}%2F{2}%23,+SpecificDate,+%23{3}%2F{4}%2F{5}%23)", yearFrom, monthFrom, dayFrom, yearTo, monthTo, dayTo);

                rootDailyEmailsResult = await _http.GetFromJsonAsync<GetDailyEmailInfoRootResponse>(dateRangeURL) ?? new GetDailyEmailInfoRootResponse();

                getEmailsResponse = new List<GetDailyDataInfoDTO>();

                foreach (var emailObject in rootDailyEmailsResult.Rows)
                {
                    getEmailsResponse.Add(new GetDailyDataInfoDTO
                    {
                        ANumber = ++increasedNumber,
                        CustomerName = emailObject.Name,
                        MeetingDateTime = emailObject.PlannedStartDate1,
                        MeetingDateTimeConverted = Convert.ToDateTime(emailObject.PlannedStartDate1),
                        City = emailObject.fCityCode,
                        Phone = emailObject.Telephone1,
                        Address = emailObject.Address1,
                        SharedNotes = emailObject.SharedNotes,
                        Category = emailObject.KathgoriaDescription,
                        TaxRegistrationNumber = emailObject.TaxRegistrationNumber,
                        IsProgrammed = emailObject.IsProgrammed.ToString() == "1" ? true : false,
                        IsService = emailObject.IsService.ToString() == "1" ? true : false,
                        Notes = emailObject.Notes,
                        IsFinalCustomer = emailObject.IsFinalCustomer.ToString() == "1" ? true : false,
                        IsTelephoneCall = emailObject.IsTelephoneCall.ToString() == "1" ? true : false,
                        FinalCustomerName = emailObject.FinalCustomerName
                    });
                }

                return new ServiceResponse<List<GetDailyDataInfoDTO>>
                {
                    Data = getEmailsResponse,
                    Message = getEmailsResponse.Count > 0 ? string.Empty : "Δεν βρέθηκαν αποτελέσματα",
                    Success = true,
                    ErrorCode = getEmailsResponse.Count > 0 ? 0 : 404
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<GetDailyDataInfoDTO>>
                {
                    Data = new List<GetDailyDataInfoDTO>(),
                    Message = $"Το σύστημα δεν μπόρεσε να ανταποκριθεί. {Environment.NewLine}{ex.Message}",
                    Success = false,
                    ErrorCode = 1500
                };
            }
        }
  
    }
}
