namespace BattleCards.Data
{
    using BattleCards.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BattleCardsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserCard> UserCards { get; set; }

        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=BattleCards;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserCard>()
                 .HasKey(k => new { k.CardId, k.UserId });
        }
    }
}
