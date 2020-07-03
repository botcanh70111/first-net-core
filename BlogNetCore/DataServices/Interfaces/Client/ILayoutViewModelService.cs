using BlogNetCore.Client.Models;

namespace BlogNetCore.DataServices.Interfaces.Client
{
    public interface ILayoutViewModelService : IViewModelService<LayoutViewModel>
    {
        LayoutViewModel CreateViewModel(string ownerId, string type);
    }
}
