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

        public LayoutViewModel CreateLayoutViewModel(LayoutViewModel viewModel)
        {
            viewModel.LogoConfig = _siteConfigService.GetConfigsByType(Constants.SiteConfigTypes.Logo)
                .FirstOrDefault(x => x.Type == Constants.SiteConfigTypes.Logo);
            viewModel.SocialConfigs = _siteConfigService.GetConfigsByType(Constants.SiteConfigTypes.Social);
            viewModel.FooterConfigs = _siteConfigService.GetConfigsByType(Constants.SiteConfigTypes.Footer);

            return viewModel;
        }
    }
}
