using BlogNetCore.DataServices.Interfaces.Admin;
using BlogNetCore.DataServices.Interfaces.Client;
using System;

namespace BlogNetCore.DataServices.Interfaces
{
    public interface IViewModelFactory
    {
        TService GetService<TService>();
    }
}
