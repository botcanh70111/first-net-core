using BlogNetCore.Client.Models;
using Services.Constants;
using Services.Interfaces;
using System.Linq;

namespace BlogNetCore.DataServices.Implementations.Client
{
    public class LayoutViewModelService
    {
        protected readonly ISiteConfigService _siteConfigService;

        public LayoutViewModelService(ISiteConfigService siteConfigService)
        {
            _siteConfigService = siteConfigService;
        }

        public LayoutViewModel CreateLayoutViewModel(LayoutViewModel viewModel, string ownerId)
        {
            viewModel.LogoConfig = _siteConfigService.GetConfigsByTypeAndOwnerId(Constants.SiteConfigTypes.Logo, ownerId)
                .FirstOrDefault(x => x.Type == Constants.SiteConfigTypes.Logo);
            viewModel.SocialConfigs = _siteConfigService.GetConfigsByTypeAndOwnerId(Constants.SiteConfigTypes.Social, ownerId);
            viewModel.FooterConfigs = _siteConfigService.GetConfigsByTypeAndOwnerId(Constants.SiteConfigTypes.Footer, ownerId);

            return viewModel;
        }
    }
}
