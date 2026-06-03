using System.ComponentModel.DataAnnotations;

namespace TravelPlanner.Models
{
    public class Trip
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DestinationId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public decimal Budget { get; set; }
    public string? TravelStyle { get; set; }
    public string? RouteName { get; set; }   // ← YENİ
    public string? Notes { get; set; }

    public User? User { get; set; }
    public Destination? Destination { get; set; }
}
}