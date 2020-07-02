namespace BlogNetCore.DataServices.Interfaces
{
    public interface IViewModelService<TViewModel>
    {
        TViewModel CreateViewModel(string ownerId, object contentKey = null);
    }
}
