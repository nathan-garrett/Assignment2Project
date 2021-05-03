using Assignment2Project.Data;
using Assignment2Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;        

        public UserRolesController(UserManager<ApplicationUserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            
        }
        [Authorize(Roles = "IT_Manager")]
        public async Task<IActionResult> Index(string SearchBy)
        {
            var data = _userManager.Users.Where(x => x.UserName != null);
            if (!String.IsNullOrEmpty(SearchBy))
            {
                ViewData["Search"] = SearchBy;
                data = data.Where(x => x.UserName.Contains(SearchBy) || x.FirstName.Contains(SearchBy) || x.LastName.Contains(SearchBy)); // allows users to search based on UserName, FirstName or LastName
            }

            var users = await data.ToListAsync(); // stores the user data as a list in var users
            var userRolesViewModel = new List<UserRolesModel>(); //creates a new list of UserRolesModel
            foreach (ApplicationUserModel user in users) //loops through the list of data and binds it to variables
            {
                var thisViewModel = new UserRolesModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }

        private async Task<List<string>> GetUserRoles(ApplicationUserModel user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Roles = "IT_Manager")]
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) //if user is null return this error message and view
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesModel>(); //create a new list of ManageUserRolesModel
            foreach (var role in _roleManager.Roles) //Loops thought _roleManager
            {
                var userRolesViewModel = new ManageUserRolesModel //create a new ManageUserRolesModel and assign vairables to the properties
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name)) //if user is assigned a role return selected
                {
                    userRolesViewModel.Selected = true;
                }
                else //else return not selected
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel); // Adds the userRolesViewModel to the model variable
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId); //find the user based on userId
            if (user == null) //if user is null return the view.
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user); //gets roles from the database
            var result = await _userManager.RemoveFromRolesAsync(user, roles); //remove roles from users
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName)); //add roles to users
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "IT_Manager")]
        //Get Delete
        public async Task<IActionResult> Delete(string userId) //returns the delete user view using the users id
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }          
          
            return View(user);


        }


        // POST: UserManager/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) //deletes the user from the database based on the users id
        {
            var data = await _userManager.FindByIdAsync(id);

            await _userManager.DeleteAsync(data);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
