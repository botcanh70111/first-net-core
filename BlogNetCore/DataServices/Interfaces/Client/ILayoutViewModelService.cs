using BlogNetCore.Client.Models;

namespace BlogNetCore.DataServices.Interfaces.Client
{
    public interface ILayoutViewModelService : IViewModelService<LayoutViewModel>
    {
        LayoutViewModel CreateViewModelByType(string ownerId, string type);
    }
}
