namespace BattleCards.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class UserCard
    {
        [Required]
        [MaxLength(IdMaxLength)]
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();

        public User User { get; set; }

        [Required]
        [MaxLength(IdMaxLength)]
        [Key]
        public string CardId { get; set; } = Guid.NewGuid().ToString();

        public Card Card { get; set; }

    }
}
