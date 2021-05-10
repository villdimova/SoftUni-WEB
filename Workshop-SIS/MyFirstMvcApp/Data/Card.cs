using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Data
{
   public class Card
    {

        public Card()
        {
            this.Users = new HashSet<UserCard>();
        }
        public int Id { get; set; }

        [MaxLength(15)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; }


        public int Attack { get; set; }
        public int Health { get; set; }

        [MaxLength(200)]
        [Required]
        public string Description { get; set; }
        public virtual ICollection<UserCard> Users { get; set; }
    }
}
