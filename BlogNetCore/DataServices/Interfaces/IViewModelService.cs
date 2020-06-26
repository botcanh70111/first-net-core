namespace BlogNetCore.DataServices.Interfaces
{
    public interface IViewModelService<TViewModel>
    {
        TViewModel CreateViewModel(object contentKey = null);
    }
}
