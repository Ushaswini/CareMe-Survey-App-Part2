using Homework_04.DTOs;
using Homework_04.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Homework_04.Controllers
{
    [Authorize(Roles ="Admin")]
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private ApplicationRoleManager _AppRoleManager = null;

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }
        // GET: api/Users

        public List<UserDTO> GetAllUsers()
        {
            string roleName = "User";            
            var role =  AppRoleManager.Roles.Single(r => r.Name == roleName);
            /*var users = from user in UserManager.Users
            where user.Roles.Any(r => r.RoleId == role.Id)
            select user;
            var usersInRoles = users.Include("StudyGroup");
            var usersDTO = usersInRoles
                                       .Select(u => new UserDTO
                                       {
                                           Id = u.Id,
                                           UserName = u.UserName,
                                           Email = u.Email,
                                           StudyGroupId = u.StudyGroupId,
                                           StudyGroupName = u.StudyGroup.StudyName
                                       }).ToList();

            return usersDTO;*/

            // Find the users in that role
            var roleUsers = UserManager.Users
                .Include(u => u.StudyGroup)
                .Where(u => u.Roles.Any(r => r.RoleId == role.Id))
                
                .Select(u => new UserDTO {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                StudyGroupId = u.StudyGroupId,
                StudyGroupName = u.StudyGroup.StudyName
            }).ToList();

            return roleUsers;
        }
    }
}
