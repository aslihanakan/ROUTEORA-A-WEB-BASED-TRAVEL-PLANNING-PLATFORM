namespace TravelPlanner.Models
{
    public class SuggestedRoute
    {
        public int Id { get; set; }

        public string RouteName { get; set; } = string.Empty;

        public string Countries { get; set; } = string.Empty;

        public string Duration { get; set; } = string.Empty;

        public string BudgetLevel { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}