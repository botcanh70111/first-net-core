using BlogNetCore.Areas.Admin.Models;
using BlogNetCore.DataServices.Interfaces.Admin;
using Services.Constants;
using Services.Interfaces;

namespace BlogNetCore.DataServices.Implementations.Admin
{
    public class RoleViewModelService : IRoleViewModelService
    {
        private readonly IRoleService _roleService;

        public RoleViewModelService(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public RoleViewModel CreateViewModel(string ownerId, object contentKey = null)
        {
            var viewModel = new RoleViewModel();

            if (contentKey != null)
            {
                var role = _roleService.GetRoleClaims(contentKey.ToString());

                viewModel.Role = role.Role;
                viewModel.AssignedClaims = role.Claims;
            } 
            else
            {
                viewModel.Role = new Services.Models.Role();
            }

            viewModel.AllRoleClaims = PermissionClaims.AllRoleClaims();
            return viewModel;
        }
    }
}
