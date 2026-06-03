namespace TravelPlanner.Models
{
    public class VisaFreeCountry
    {
        public int Id { get; set; }

        public string CountryName { get; set; } = string.Empty;

        public string VisaDuration { get; set; } = string.Empty;

        public string BestCities { get; set; } = string.Empty;

        public string BestSeason { get; set; } = string.Empty;

        public decimal EstimatedBudget { get; set; }
    }
}