using System.ComponentModel.DataAnnotations;

namespace Employee_Mang_application.Models
{
    public class AddEmployeeDto
    {
       // [Required(ErrorMessage = "First Name is required.")]
        public  string? FirstName { get; set; }

       // [Required(ErrorMessage = "Last Name is required.")]
        public  string? LastName { get; set; }


        public  string? Department { get; set; }


       // [Required(ErrorMessage = "Email is required.")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public  string? Email { get; set; }

       // [Required(ErrorMessage = "Date of Joining is required.")]
      //  [RegularExpression(@"^(\d{4})-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Date must be in the format YYYY-MM-DD ")]
        public  DateTime DateOfJoining { get; set; }
    }
}
