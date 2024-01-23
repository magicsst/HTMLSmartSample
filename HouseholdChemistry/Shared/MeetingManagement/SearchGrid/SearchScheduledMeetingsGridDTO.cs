using System.ComponentModel.DataAnnotations;

namespace HouseholdChemistry.Shared.MeetingManagement.SearchGrid
{
    public class SearchScheduledMeetingsGridDTO : IValidatableObject
    {
        public DateTime DateFrom { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        public DateTime DateTo { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1);


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateFrom > DateTo)
            {
                yield return new ValidationResult("Η ημερομηνία έναρξης δεν μπορεί να είναι μεγαλύτερη από την ημερομηνία λήξης.", new string[] { nameof(DateFrom) });
            }

        }
    }
}
