using FinalExam_Amoeba.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalExam_Amoeba.Areas.Admin.ViewModels
{
    public class EmployeeUpdateVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name can contain minimum 3 characters")]
        [MaxLength(25, ErrorMessage = "Name can contain maximum 25 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [MinLength(3, ErrorMessage = "Surname can contain minimum 3 characters")]
        [MaxLength(25, ErrorMessage = "Surname can contain maximum 25 characters")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [MinLength(5, ErrorMessage = "Description can contain minimum 5 characters")]
        [MaxLength(300, ErrorMessage = "Description can contain maximum 300 characters")]
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
        public string? ImageURL { get; set; }
        public string? Twitter { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public int? PositionId { get; set; }
        public List<Position>? Positions { get; set; }
    }
}
