namespace Git.Services
{
    using Git.ViewModels;
    using Git.ViewModels.Commits;
    using Git.ViewModels.Repositories;
    using System.Collections.Generic;

    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserViewModel model);

        ICollection<string> ValidateRepositoriesInput(CreateRepositoryViewModel model);

        ICollection<string> ValidateCommitInput(CreateCommitViewModel model);
    }
}
