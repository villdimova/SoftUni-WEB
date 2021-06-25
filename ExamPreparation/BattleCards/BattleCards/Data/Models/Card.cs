namespace BattleCards.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class Card
    {
        public Card()
        {
            this.Usercards = new HashSet<UserCard>();
        }

        [Required]
        [MaxLength(IdMaxLength)]
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(CardNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; }

        public int Attack { get; set; }

        public int Health { get; set; }

        [Required]
        [MaxLength(CardDescriptionMaxLength)]
        public string Description { get; set; }

        public ICollection<UserCard> Usercards { get; set; }

    }
}
