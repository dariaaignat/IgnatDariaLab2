using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IgnatDariaLab2.Models
{
    public class Member
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Prenumele trebuie să înceapă cu majusculă (ex. Ana sau Ana-Maria).")]
        [StringLength(30, MinimumLength = 3)]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-z\s]*$", ErrorMessage = "Numele trebuie să înceapă cu majusculă.")]
        [StringLength(30, MinimumLength = 3)]
        public string? LastName { get; set; }

        [StringLength(70)]
        public string? Adress { get; set; }

        public string Email { get; set; }

        [RegularExpression(@"^0([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
            ErrorMessage = "Telefonul trebuie să fie de forma '0722-123-123', '0722.123.123' sau '0722 123 123'.")]
        public string? Phone { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName => $"{FirstName} {LastName}";

        public ICollection<Borrowing>? Borrowings { get; set; }
    }
}