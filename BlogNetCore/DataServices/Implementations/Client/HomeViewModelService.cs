using BlogNetCore.Client.Models;
using BlogNetCore.DataServices.Interfaces.Client;

namespace BlogNetCore.DataServices.Implementations.Client
{
    public class HomeViewModelService : IHomeViewModelService
    {
        public HomeViewModelService()
        {
        }

        public HomeViewModel CreateViewModel(string ownerId, object contentKey = null)
        {
            var viewModel = new HomeViewModel();
            return viewModel;
        }
    }
}
