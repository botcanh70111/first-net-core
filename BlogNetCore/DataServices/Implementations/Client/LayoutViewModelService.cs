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
            var siteConfigs = _siteConfigService.GetAll();
            viewModel.LogoConfig = siteConfigs.FirstOrDefault(x => x.Type == Constants.SiteConfigTypes.Logo);
            viewModel.SocialConfigs = siteConfigs.Where(x => x.Type == Constants.SiteConfigTypes.Social);
            viewModel.FooterConfigs = siteConfigs.Where(x => x.Type == Constants.SiteConfigTypes.Footer);

            return viewModel;
        }
    }
}
