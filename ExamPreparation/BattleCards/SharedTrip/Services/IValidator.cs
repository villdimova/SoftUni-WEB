namespace SharedTrip.Services
{
    using SharedTrip.Models.Trips;
    using SharedTrip.ViewModels.Users;
    using System.Collections.Generic;

    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserViewModel model);
        ICollection<string> ValidateTrip(AddTripViewModel model);

       
    }
}
