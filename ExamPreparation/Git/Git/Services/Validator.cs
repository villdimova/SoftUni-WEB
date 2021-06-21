namespace Git.Services
{
    using Git.ViewModels;
    using Git.ViewModels.Commits;
    using Git.ViewModels.Repositories;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using static Data.DataConstants;
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCommitInput(CreateCommitViewModel model)
        {
            var errors = new List<string>();
            if (model.Description.Length < CommitDescriptionMinLength)
            {
                errors.Add($"Description '{model.Description}' is not valid. It must be minimum {RepositoryNameMinLength} characters long.");
            }

            return errors;
        }

        public ICollection<string> ValidateRepositoriesInput(CreateRepositoryViewModel model)
        {
            var errors = new List<string>();
            if (model.Name.Length<RepositoryNameMinLength|| model.Name.Length>RepositoryNameMaxLength)
            {
                errors.Add($"Name '{model.Name}' is not valid. It must be between {RepositoryNameMinLength} and {RepositoryNameMaxLength} characters long.");
            }
            if (model.RepositoryType!="Public" && model.RepositoryType!="Private")
            {
                errors.Add("Not valid repository type!");
            }

            return errors;
        }

        public ICollection<string> ValidateUserRegistration(RegisterUserViewModel model)
        {
            var errors = new List<string>();
            if (model.Username.Length<UserMinUsername|| model.Username.Length>UserDefaultMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UserMinUsername} and {UserDefaultMaxLength} characters long.");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email {model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length < UserMinPassword || model.Password.Length > UserDefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {UserMinPassword} and {UserDefaultMaxLength} characters long.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and its confirmation are different.");
            }

            return errors;
        }
    }
}
