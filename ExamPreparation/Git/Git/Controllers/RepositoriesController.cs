using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModels.Repositories;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IValidator validator;
        private readonly GitDbContext data;

        public RepositoriesController(IValidator validator, GitDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }
        public HttpResponse All()
        {
            List<AllRepositoriesViewModel> repositories;
            if (this.User.IsAuthenticated)
            {
                repositories = this.data.Repositories
                  .Where(x => x.IsPublic|| x.OwnerId==this.User.Id)
                  .Select(r => new AllRepositoriesViewModel
                  {
                      Id = r.Id,
                      Name = r.Name,
                      CreatedOn = r.CreatedOn,
                      CommitsCount = r.Commits.Count,
                      Owner = r.Owner.Username,
                  }).ToList();
            }
            else
            {
                repositories = this.data.Repositories
                  .Where(x => x.IsPublic)
                  .Select(r => new AllRepositoriesViewModel
                  {
                      Id = r.Id,
                      Name = r.Name,
                      CreatedOn = r.CreatedOn,
                      CommitsCount = r.Commits.Count,
                      Owner = r.Owner.Username,
                  }).ToList();
            }
              

            return View(repositories);
        }

        public HttpResponse Create()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateRepositoryViewModel model)
        {
            var errors = this.validator.ValidateRepositoriesInput(model);
            if (errors.Any())
            {
                return Error(errors);
            }
           
            var repository = new Repository
            {
                Name = model.Name,
                CreatedOn = DateTime.UtcNow,
                IsPublic = model.RepositoryType == "Public" ? true : false,
                OwnerId=this.User.Id,

            };

            this.data.Repositories.Add(repository);
            this.data.SaveChanges();
            return Redirect("/Repositories/All");
        }

    }
}
