using System.ComponentModel.DataAnnotations;

namespace FinalExam_Amoeba.Areas.Admin.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name can contain minimum 3 characters")]
        [MaxLength(25, ErrorMessage = "Name can contain maximum 25 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [MinLength(3, ErrorMessage = "Surname can contain minimum 3 characters")]
        [MaxLength(25, ErrorMessage = "Surname can contain maximum 25 characters")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Username can contain minimum 3 characters")]
        [MaxLength(25, ErrorMessage = "Username can contain maximum 25 characters")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Email is required")]
        [MinLength(5, ErrorMessage = "Username can contain minimum 5 characters")]
        [MaxLength(50, ErrorMessage = "Username can contain maximum 50 characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(50,ErrorMessage ="Password can contain maximum 50 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
