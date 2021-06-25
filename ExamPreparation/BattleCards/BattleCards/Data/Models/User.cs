namespace BattleCards.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class User
    {
        public User()
        {
            this.Usercards = new HashSet<UserCard>();
        }

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

        public ICollection<UserCard> Usercards { get; set; }
    }
}
