namespace BattleCards.Services
{
    using BattleCards.Models.Cards;
    using BattleCards.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
   

    using static Data.DataConstants;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateCard(CreateCardViewModel model)
        {
            var errors = new List<string>();

            if (model.Name == null || model.Name.Length < CardNameMinLength || model.Name.Length > CardNameMaxLength)
            {
                errors.Add($"Name '{model.Name}' is not valid. It must be between {CardNameMinLength} and {CardNameMaxLength} characters long.");
            }

            if (model.Keyword==null)
            {
                errors.Add($"Keyword can't be null.");
            }

            if (model.Health<0)
            {
                errors.Add("Health can't be negative value.");
            }

            if (model.Attack<0)
            {
                errors.Add("Attack can't be negative value.");
            }

            if (model.Description==null )
            {
                errors.Add("Description can't be negative value.");
            }

            if (model.Description.Length>CardDescriptionMaxLength)
            {
                errors.Add($"Description must be maximum {CardDescriptionMaxLength} character long");
            }
            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserViewModel user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < UsernameMinLength || user.Username.Length > UsernameMaxLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UsernameMinLength} and {UsernameMaxLength} characters long.");
            }

            if (user.Email == null || !Regex.IsMatch(user.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email '{user.Email}' is not a valid e-mail address.");
            }

            if (user.Password == null || user.Password.Length < PasswordMinLength || user.Password.Length > PasswordMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {PasswordMaxLength} characters long.");
            }

            if (user.Password != null && user.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (user.Password != user.ConfirmPassword)
            {
                errors.Add("Password and its confirmation are different.");
            }

            return errors;
        }

       
    }
}