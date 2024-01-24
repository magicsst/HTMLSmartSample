using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdChemistry.Shared.EmailManagement
{
    public class GetDailyDataInfoDTO
    {
        [Range(0, int.MaxValue, ErrorMessage = "Το Id πρέπει να είναι μεγαλύτερο ή ίσο με το 0.")]
        public int ANumber { get; set; }
        public string CustomerName { get; set; } 
        public DateTime MeetingDateTime { get; set; } 
        public DateTime MeetingDateTimeConverted { get; set; } 
        public string City { get; set; } 
        public string Phone { get; set; } 
        public string Address { get; set; } 
        public string SharedNotes { get; set; } 
        public string Category { get; set; } 
        public string TaxRegistrationNumber { get; set; } 
        public bool IsProgrammed { get; set; } 
        public bool IsService { get; set; } 
        public bool IsFinalCustomer { get; set; } 
        public bool IsTelephoneCall { get; set; }
        public string Notes { get; set; }
        public string FinalCustomerName { get; set; }
    }
}
