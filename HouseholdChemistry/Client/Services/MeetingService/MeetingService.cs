using HouseholdChemistry.Shared.MeetingManagement.SearchGrid;

namespace HouseholdChemistry.Client.Services.MeetingService
{
    public class MeetingService : IMeetingService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _configuration;

        public MeetingService(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<List<GetMeetingGridInfoDTO>>> GetGridMeetings(string employeeCode, DateTime startDate, DateTime endDate)
        {
            GetMeetingGridInfoRootResponse getMeetingGridInfoRootResponse = new GetMeetingGridInfoRootResponse();
            List<GetMeetingGridInfoDTO> getMeetingGridInfoDTO = null;

            string dayFrom = startDate.Day.ToString().PadLeft(2, '0');
            string monthFrom = startDate.Month.ToString().PadLeft(2, '0');
            string yearFrom = startDate.Year.ToString();
            string dayTo = endDate.Day.ToString().PadLeft(2, '0');
            string monthTo = endDate.Month.ToString().PadLeft(2, '0');
            string yearTo = endDate.Year.ToString();

            string entersoftURL = string.Empty;

            try
            {
                // Αυτό είναι το κλειδί για το Authentication της Entersoft
                _http.DefaultRequestHeaders.Add("X-ESAPIKEY-ECOMCONNECTOR", @_configuration.GetValue<string>("X-ESAPIKEY-ECOMCONNECTOR"));

                // Πρέπει να αφαιρέσω το Header του Authorization γιατι με κόβει το WebAPI της Entersoft
                _http.DefaultRequestHeaders.Authorization = null;

                entersoftURL = String.Format(@"https://api.entersoft.gr/api/rpc/PublicQuery/Web_Scrolls/Web_Future_Programmed_Meetings?SalespersonCode={0}&PlannedStartDate1=ESDateRange(SpecificDate,+%23{1}%2F{2}%2F{3}%23,+SpecificDate,+%23{4}%2F{5}%2F{6}%23)",
                    employeeCode,
                    yearFrom,
                    monthFrom,
                    dayFrom,
                    yearTo,
                    monthTo,
                    dayTo);

                getMeetingGridInfoRootResponse = await _http.GetFromJsonAsync<GetMeetingGridInfoRootResponse>(entersoftURL) ?? new GetMeetingGridInfoRootResponse();

                getMeetingGridInfoDTO = new List<GetMeetingGridInfoDTO>();

                foreach (var meeting in getMeetingGridInfoRootResponse.Rows)
                {
                    getMeetingGridInfoDTO.Add(
                        new GetMeetingGridInfoDTO
                        {
                            TaskGID = meeting.TaskGID, // Αυτό είναι το πραγματικό ID του Meeting
                            fTaskSiteGID = meeting.fTaskSiteGID,
                            fPersonGID = meeting.fPersonGID,
                            fTaskTypeGID = meeting.fTaskTypeGID,
                            Code = meeting.Code, // Αυτό είναι το Readable ID του Meeting (Το δείχνω στο Grid)
                            fParentTaskGID = meeting.fParentTaskGID,
                            SalesPersonCode = meeting.SalesPersonCode, // Ο κωδικός του Employee
                            Name = meeting.Name, // To όνομα του Customer (Το δείχνω στο Grid)
                            Notes = meeting.Notes, // Οι σημειώσεις του Meeting (ToolTip)
                            PlannedStartDate1 = meeting.PlannedStartDate1, // Η ημερομηνία Meeting (Το δείχνω στο Grid)  
                            Katastasi_Synantisis = meeting.Katastasi_Synantisis, // To status του Meeting (Το δείχνω στο Grid)
                            fCityCode = meeting.fCityCode, // H περιοχή του Meeting (Το δείχνω στο Grid)
                            Telephone1 = meeting.Telephone1, // To τηλέφωνο του Customer 
                            fSiteAddress = meeting.fSiteAddress, // H διεύθυνση του Customer
                            FullName = meeting.FullName, // To όνομα του Customer
                            Code_MotherTask = meeting.Code_MotherTask, // Tο Readable Mother ID του Meeting
                            KathgoriaDescription = meeting.KathgoriaDescription, // Αναλυτική περιγραφή κατηγορίας
                            Omada1_Description = meeting.Omada1_Description, // Αναλυτική Κατηγορίας
                            IsProgrammed = (meeting.IsProgrammed == 1 ? true : false), // Προγραμματισμένο? (Το δείχνω στο Grid)
                            IsService = (meeting.IsService == 1 ? true : false), // Έγινε Service? 
                            IsTelephoneCall = (meeting.IsTelephoneCall == 1 ? true : false), // Έγινε Τηλεφωνική Κλήση? ,
                            fSIteCode = meeting.fSiteCode, // O κωδικός του Υποκαταστήματος 
                            fSIteDescription = meeting.fSiteDescription, // H περιγραφή του Υποκαταστήματος 
                            ActualClosedDate = meeting.ActualClosedDate, // Δεν ξέρω τι ημερομηνία είναι αυτή
                            Url = $@"/editmeeting/{meeting.Code}/scheduled"
                        });
                }
                return new ServiceResponse<List<GetMeetingGridInfoDTO>>
                {
                    Data = getMeetingGridInfoDTO,
                    Message = getMeetingGridInfoDTO.Count > 0 ? string.Empty : "Δεν βρέθηκαν αποτελέσματα",
                    Success = true,
                    ErrorCode = getMeetingGridInfoDTO.Count > 0 ? 0 : 404
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<GetMeetingGridInfoDTO>>
                {
                    Data = new List<GetMeetingGridInfoDTO>(),
                    Message = $"Το σύστημα δεν μπόρεσε να ανταποκριθεί. {Environment.NewLine}{ex.Message}",
                    Success = false,
                    ErrorCode = 1500
                };
            }

        }

    }
}
