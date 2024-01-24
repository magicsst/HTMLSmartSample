using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdChemistry.Shared.EmailManagement
{
    public class GetDailyEmailInfoRootResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public string Table { get; set; } = string.Empty;
        public List<GetDailyEmailInfoRowResponse> Rows { get; set; } = new List<GetDailyEmailInfoRowResponse>();
    }
}
