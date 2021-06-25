namespace SharedTrip.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    public class User
    {
        [Required]
        [MaxLength(IdMaxLength)]
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [RegularExpression(UserEmailRegularExpression)]
        public string Email { get; set; }

      
    }
}
