using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdChemistry.Shared.EmailManagement
{
    public class GetDailyEmailInfoRowResponse
    {
        public string TaskGID { get; set; }
        public string fPersonGID { get; set; }
        public string fTaskTypeGID { get; set; }
        public string Code { get; set; } 
        public string fParentTaskGID { get; set; } 
        public string SalesPersonCode { get; set; } 
        public string Name { get; set; } 
        public string Notes { get; set; } 
        public DateTime PlannedStartDate1 { get; set; } 
        public string Code3 { get; set; } 
        public string DateField1 { get; set; }
        public string fCityCode { get; set; } 
        public string Telephone1 { get; set; } 
        public string Status { get; set; }
        public string StringField4 { get; set; }
        public string Address1 { get; set; } 
        public string FullName { get; set; } 
        public string Code_MotherTask { get; set; } 
        public string Level { get; set; } 
        public string Zones { get; set; }
        public string Periodos_leit { get; set; } 
        public string Episkepsimothta { get; set; } 
        public string Xondremporos { get; set; }
        public string SharedNotes { get; set; } 
        public string Notes1 { get; set; } 
        public string PlannedStartDate { get; set; }
        public string KathgoriaDescription { get; set; } 
        public string TaxRegistrationNumber { get; set; } 
        public string Omada1_Description { get; set; } 
        public int IsProgrammed { get; set; } 
        public int IsService { get; set; } 
        public int IsTelephoneCall { get; set; }
        public int IsFinalCustomer { get; set; }
        public string FinalCustomerName { get; set; }
    }
}
