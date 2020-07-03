using BlogNetCore.Client.Models;
using BlogNetCore.DataServices.Interfaces.Client;
using Services.Constants;
using Services.Interfaces;
using System.Linq;

namespace BlogNetCore.DataServices.Implementations.Client
{
    public class LayoutViewModelService : ILayoutViewModelService
    {
        protected readonly ISiteConfigService _siteConfigService;

        public LayoutViewModelService(ISiteConfigService siteConfigService)
        {
            _siteConfigService = siteConfigService;
        }

        public LayoutViewModel CreateViewModel(string ownerId, object contentKey = null)
        {
            var viewModel = new LayoutViewModel();
            viewModel.LogoConfig = _siteConfigService.GetConfigsByTypeAndOwnerId(Constants.SiteConfigTypes.Logo, ownerId)
                .FirstOrDefault(x => x.Type == Constants.SiteConfigTypes.Logo);
            viewModel.SocialConfigs = _siteConfigService.GetConfigsByTypeAndOwnerId(Constants.SiteConfigTypes.Social, ownerId);
            viewModel.FooterConfigs = _siteConfigService.GetConfigsByTypeAndOwnerId(Constants.SiteConfigTypes.Footer, ownerId);

            return viewModel;
        }

        public LayoutViewModel CreateViewModel(string ownerId, string type)
        {
            var viewModel = new LayoutViewModel();
            switch (type)
            {
                case "Logo":
                    viewModel.LogoConfig = _siteConfigService.GetConfigsByTypeAndOwnerId(Constants.SiteConfigTypes.Logo, ownerId)
                        .FirstOrDefault(x => x.Type == Constants.SiteConfigTypes.Logo);
                    break;
                case "Footer":
                    viewModel.FooterConfigs = _siteConfigService.GetConfigsByTypeAndOwnerId(Constants.SiteConfigTypes.Footer, ownerId);
                    break;
                case "RightSide":
                    viewModel.SocialConfigs = _siteConfigService.GetConfigsByTypeAndOwnerId(Constants.SiteConfigTypes.Social, ownerId);
                    break;
                default:
                    viewModel = CreateViewModel(ownerId);
                    break;
            }

            return viewModel;
        }
    }
}
