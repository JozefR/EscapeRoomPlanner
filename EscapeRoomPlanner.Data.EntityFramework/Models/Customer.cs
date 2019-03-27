using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EscapeRoomPlanner.Data.EntityFramework.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's First name.")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter customer's Second name.")]
        [StringLength(255)]
        public string SecondName { get; set; }

        public string Email { get; set; }

        [Required]
        [RegularExpression("^\\+\\d{3}-\\d{3}-\\d{3}-\\d{3}$")]
        public string PhoneNumber { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}