using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModels.Commits;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Linq;

namespace Git.Controllers
{
   public class CommitsController: Controller
    {
        private readonly IValidator validator;
        private readonly GitDbContext data;
        public CommitsController(IValidator validator, GitDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }
        [Authorize]
        public HttpResponse All()
        {
            var commits = this.data.Commits
                .Where(c => c.Creator.Id == this.User.Id)
                .Select(x => new AllCommitsViewModel
                {
                    Id=x.Id,
                    Repository=x.Repository.Name,
                    Description=x.Description,
                    CreatedOn=x.CreatedOn,
                }).ToList();
            return View(commits);
        }

        [Authorize]
        public HttpResponse Create(string id)
        {
            var repo = this.data.Repositories.FirstOrDefault(r => r.Id == id);
            if (repo == null)
            {
                return Error("The repository does nor exist.");
            }
            return View(repo);
        }



        [Authorize]
        [HttpPost]
        public HttpResponse Create(string id,CreateCommitViewModel input)
        {
            var errors = this.validator.ValidateCommitInput(input);

            var repo = this.data.Repositories.FirstOrDefault(r => r.Id == id);

            if (repo==null)
            {
                errors.Add("Not valid repository");
            }

            if (errors.Any())
            {
                return View("./Error", errors);
            }

            var commit = new Commit
            {
                Description = input.Description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = this.User.Id,
                RepositoryId = repo.Id
            };

            this.data.Commits.Add(commit);
            this.data.SaveChanges();

            return Redirect("/Repositories/All");
        }

        [Authorize]
        public HttpResponse Delete(string id)
        {
            var commit = this.data.Commits
                .FirstOrDefault(c => c.Id == id && c.CreatorId==this.User.Id);

            if (commit==null)
            {
                return BadRequest();
            }

            this.data.Commits.Remove(commit);
            this.data.SaveChanges();

            return Redirect("/Commits/All");
        }


    }
}
