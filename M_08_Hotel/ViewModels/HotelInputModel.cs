using M_08_Hotel.Models;
using System.ComponentModel.DataAnnotations;

namespace M_08_Hotel.ViewModels
{
    public class HotelInputModel
    {
        public int HotelId { get; set; }
        [Required, StringLength(50)]
        public string HotelName { get; set; } = default!;
        [Required, StringLength(50)]
        public string Location { get; set; } = default!;
        [Required, StringLength(50)]
        public string Rating { get; set; } = default!;
        [Required, StringLength(50)]
        public string ContactInfo { get; set; } = default!;
        [Required]
        public IFormFile Picture { get; set; } = default!;

        public bool HotelRoomAvailable { get; set; }
        public virtual List<Room> Rooms { get; set; } = [];
    }
}
