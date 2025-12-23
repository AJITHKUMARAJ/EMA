using System.ComponentModel.DataAnnotations;

namespace EMA.Models
{
    /// <summary>
    /// Data transfer object used when creating a new employee.
    /// </summary>
    public class EmployeeDto
    {
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

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }
    }
}