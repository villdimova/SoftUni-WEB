namespace BattleCards.Services
{
    using System.Collections.Generic;
    using BattleCards.Models.Cards;
    using BattleCards.ViewModels.Users;
 
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserViewModel model);

        ICollection<string> ValidateCard(CreateCardViewModel model);

        //ICollection<string> ValidateIssue(AddIssueFormModel model);
    }
}