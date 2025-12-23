using System.ComponentModel.DataAnnotations;

namespace EMA.Models.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        [MaxLength(1)]
        public string Gender { get; set; } = "M";

        public decimal Salary { get; set; }
    }
}
