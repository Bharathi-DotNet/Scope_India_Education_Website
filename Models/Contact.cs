using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace scope_project_2.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string? Message { get; set; }

    }
}
