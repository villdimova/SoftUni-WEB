using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.ViewModels.Issues
{
  public  class CarIssuesViewModel
    {
        public CarIssuesViewModel()
        {
            this.Issues = new HashSet<IssueViewModel>();
        }
        public string Id { get; set; }

        public int Year { get; set; }

        public string Model { get; set; }

        public IEnumerable<IssueViewModel> Issues { get; set; }
    }
}
