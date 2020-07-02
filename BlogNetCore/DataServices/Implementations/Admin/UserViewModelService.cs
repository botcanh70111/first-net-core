using BlogNetCore.Areas.Admin.Models;
using BlogNetCore.DataServices.Interfaces.Admin;
using Services.Constants;
using Services.Interfaces;
using System.Linq;

namespace BlogNetCore.DataServices.Implementations.Admin
{
    public class UserViewModelService : IUserViewModelService
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserViewModelService(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public UserViewModel CreateViewModel(string ownerId, object contentKey = null)
        {
            var viewModel = new UserViewModel();
            viewModel.AllRoleClaims = PermissionClaims.AllRoleBloggerClaims();
            viewModel.AllRoles = _roleService.GetAll();
            if (contentKey != null)
            {
                var user = _userService.GetProfile((string)contentKey);
                viewModel.User = user.User;
                viewModel.Roles = user.Roles.Select(x => x.Role);
                viewModel.UserClaims = user.UserClaims;
            }

            return viewModel;
        }
    }
}
