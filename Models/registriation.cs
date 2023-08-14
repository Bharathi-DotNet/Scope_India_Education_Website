using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace scope_project_2.Models
{
    public class registriation
    {
        [Required(ErrorMessage = "Please enter the first name.")]
        public string F_N { set; get; }

        [Required(ErrorMessage = "Please enter the last name.")]
        public string L_N { set; get; }

        [Required(ErrorMessage = "Please enter the gender.")]
        public string gender { set; get; }

        [Required(ErrorMessage = "Please enter the date of birth.")]
        public string DOB { set; get; }

        [Required(ErrorMessage = "Please enter the email.")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Please enter the phone number.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { set; get; }

        [Required(ErrorMessage = "Please enter the country.")]
        public string Country { set; get; }

        [Required(ErrorMessage = "Please enter the state.")]
        public string State { set; get; }

        [Required(ErrorMessage = "Please enter the city.")]
        public string City { set; get; }

        public string Read { set; get; }
        public string Play { set; get; }
        public string Cook { set; get; }
        [Required(ErrorMessage = "Please Upload Your Image")]
        public List<IFormFile> imag { get; set; }
    }
}
