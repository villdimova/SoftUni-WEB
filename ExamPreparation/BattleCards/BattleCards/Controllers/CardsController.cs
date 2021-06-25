namespace BattleCards.Controllers
{
    using BattleCards.Data;
    using BattleCards.Data.Models;
    using BattleCards.Models.Cards;
    using BattleCards.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    [Authorize]
    public class CardsController:Controller
    {
        private readonly IValidator validator;
        private readonly BattleCardsDbContext data;
        public CardsController(IValidator validator, BattleCardsDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }
        public HttpResponse All() 
        {
            var cards = this.data.Cards.Select(c => new AllCardsViewModel
            {
                Id=c.Id,
                Name = c.Name,
                Image = c.ImageUrl,
                Keyword = c.Keyword,
                Attack = c.Attack.ToString(),
                Health = c.Health.ToString()
            })
           .ToList();

            return View(cards);
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(CreateCardViewModel model)
        {
            var modelErrors = this.validator.ValidateCard(model);

            if (modelErrors.Any())
            {

            return Redirect("/Cards/Add");
            }

            if (this.data.Cards.Any(c=>c.Name==model.Name&&c.Description==model.Description))
            {
                return Redirect("/Cards/All");
            }

            var card = new Card
            {
                Name = model.Name,
                Description=model.Description,
                Health=model.Health,
                Attack=model.Attack,
                ImageUrl=model.Image,
                Keyword=model.Keyword,
            };

            this.data.Cards.Add(card);
            this.data.SaveChanges();

            return Redirect("/Cards/All");
        }

       public HttpResponse AddToCollection(string cardId)
        {
            var card = this.data.Cards.FirstOrDefault(c => c.Id == cardId);

            if (card==null)
            {
                return BadRequest();
            }

            var user = this.data.Users.FirstOrDefault(u => u.Id == this.User.Id);

            var userCard = new UserCard
            {
                CardId=cardId,
                UserId=user.Id,
            };
            if (user.Usercards.Any(c=>c.CardId==cardId))
            {
                return Redirect("/Cards/All");
            }
            user.Usercards.Add(userCard);
            this.data.SaveChanges();

            return Redirect("/Cards/All");
        }

        public HttpResponse Collection()
        {
            var cardsId = this.data.UserCards.Where(c => c.UserId == this.User.Id).Select(s=>s.CardId).ToList();

            var cards = this.data.Cards.Where(x=>cardsId.Contains(x.Id)).Select(c => new AllCardsViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Image = c.ImageUrl,
                Keyword = c.Keyword,
                Attack = c.Attack.ToString(),
                Health = c.Health.ToString()
            })
           .ToList();

            return View(cards);
        }

        public HttpResponse RemoveFromCollection(string cardId)
        {
            var card = this.data.UserCards.FirstOrDefault(c => c.CardId == cardId&& c.UserId==this.User.Id);

            if (card==null)
            {
                return BadRequest();
            }

            this.data.UserCards.Remove(card);
            this.data.SaveChanges();

            return Redirect("/Cards/Collection");
        }
    }
}
