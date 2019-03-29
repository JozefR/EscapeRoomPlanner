using System.ComponentModel.DataAnnotations;
using EscapeRoomPlanner.Data.EntityFramework.Models;

namespace EscapeRoomPlanner.ViewModel
{
    public class NewReservationVM
    {
        public int RoomId { get; set; }

        public string RoomName { get; set; }

        public string SelectedDate { get; set; }

        public int SelectedOpenTime { get; set; }

        public int SelectedCloseTime { get; set; }

        [Required(ErrorMessage = "Please enter customer's First name.")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter customer's Second name.")]
        [StringLength(255)]
        public string SecondName { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public string Email { get; set; }

        [Required]
        [RegularExpression("^\\+\\d{3}-\\d{3}-\\d{3}$")]
        public string PhoneNumber { get; set; }
    }
}