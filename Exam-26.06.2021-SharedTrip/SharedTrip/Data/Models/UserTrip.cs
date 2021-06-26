namespace SharedTrip.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class UserTrip
    {
        [MaxLength(IdMaxLength)]
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();

        public User User { get; set; }

        [MaxLength(IdMaxLength)]
        [Key]
        public string TripId { get; set; } = Guid.NewGuid().ToString();

        public Trip Trip { get; set; }
    }
}
