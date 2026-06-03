using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPlanner.Models
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        // Destination wishlist
        public int? DestinationId { get; set; }

        // Route wishlist - sadece isim saklanır, FK yok
        public string? RouteName { get; set; }

        // VisaFree wishlist
        public string? VisaFreeCountryName { get; set; }

        // "Destination" | "Route" | "VisaFree"
        public string ItemType { get; set; } = "Destination";

        // Navigation properties
        public User? User { get; set; }
        public Destination? Destination { get; set; }
    }
}
