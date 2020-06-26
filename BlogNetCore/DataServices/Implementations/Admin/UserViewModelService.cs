﻿using BlogNetCore.Areas.Admin.Models;
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

        public UserViewModel CreateViewModel(object contentKey = null)
        {
            var user = _userService.GetProfile((string)contentKey);
            var viewModel = new UserViewModel();
            viewModel.User = user.User;
            viewModel.Roles = user.Roles.Select(x => x.Role);
            viewModel.UserClaims = user.UserClaims;
            viewModel.AllRoleClaims = PermissionClaims.AllRoleClaims();
            viewModel.AllRoles = _roleService.GetAll();
            return viewModel;
        }
    }
}
