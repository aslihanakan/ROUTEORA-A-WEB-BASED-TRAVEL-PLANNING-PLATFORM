using System.ComponentModel.DataAnnotations;

namespace TravelPlanner.Models
{
    public class Destination
    {
        public int Id { get; set; }

        [Required]
        public string CountryName { get; set; } = string.Empty;

        [Required]
        public string CityName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? BestSeason { get; set; }

        public decimal EstimatedBudget { get; set; }
    }
}