using BlogNetCore.Client.Models;
using BlogNetCore.DataServices.Interfaces.Client;
using Services.Interfaces;

namespace BlogNetCore.DataServices.Implementations.Client
{
    public class HomeViewModelService : LayoutViewModelService, IHomeViewModelService
    {
        public HomeViewModelService(ISiteConfigService siteConfigService) : base(siteConfigService)
        {
        }

        public HomeViewModel CreateViewModel(string ownerId, object contentKey = null)
        {
            var viewModel = new HomeViewModel();
            CreateLayoutViewModel(viewModel, ownerId);
            
            return viewModel;
        }
    }
}
