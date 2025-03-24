using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace M_08_Hotel.Models
{
        public class Hotel
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
            [Required, StringLength(50)]
            public string Picture { get; set; } = default!;

            public bool HotelRoomAvailable { get; set; }
            public virtual ICollection<Room> Rooms { get; set; } = [];

        }
        public class Room
        {
            public int RoomId { get; set; }
            [Required, StringLength(50)]
            public string RoomType { get; set; } = default!;
            [Required, Column(TypeName = "date")]
            public DateTime? ReservationDate { get; set; }
            [Required, Column(TypeName = "money")]
            public decimal PricePerNight { get; set; }
            [Required, ForeignKey("Hotel")]
            public int HotelId { get; set; }
            public virtual Hotel? Hotels { get; set; }

        }
        public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
        {
            public DbSet<Hotel> Hotels { get; set; }
            public DbSet<Room> Rooms { get; set; }

        }

    }
