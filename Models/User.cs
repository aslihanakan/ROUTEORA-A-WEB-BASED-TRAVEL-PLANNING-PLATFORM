using System.ComponentModel.DataAnnotations;

namespace TravelPlanner.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}
