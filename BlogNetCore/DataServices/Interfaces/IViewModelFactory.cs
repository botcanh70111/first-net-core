using BlogNetCore.DataServices.Interfaces.Admin;
using BlogNetCore.DataServices.Interfaces.Client;
using System;

namespace BlogNetCore.DataServices.Interfaces
{
    public interface IViewModelFactory
    {
        Lazy<IHomeViewModelService> HomeViewModelService { get; set; }
        Lazy<IRoleViewModelService> RoleViewModelService { get; set; }
        Lazy<IUserViewModelService> UserViewModelService { get; set; }
    }
}
