using Excel.Improter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Improter.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<Role> roleManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginInput());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginInput input)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(input.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("UserNotFound", "this User Not Fouunded");
                    return View(input);
                }
                var result = await signInManager.PasswordSignInAsync(user, input.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","ExcelImport");
                }
                else
                {
                    ModelState.AddModelError("LoginFailed", "UserName or Password Failed");
                }

            }
            ModelState.AddModelError("Reqiured","UserName and Password Reqired");
            return View(input);
        }


        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            
            var model = new CreateUserInput
            {
                Roles = roleManager.Roles.Select(i => new CheckRoles
                {
                    Name = i.Name,
                    NormalName = i.FullName,
                    IsSelected = false
                }).ToList(),
                Password = CreatePassword(11)
            };
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "ExcelImport");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserInput input)
        {
            if(ModelState.IsValid)
            {
                var isExistUser = await userManager.FindByNameAsync(input.UserName);
                if (isExistUser != null)
                {
                    ModelState.AddModelError("Exist", "User already exist");
                }
                else
                {
                    AppUser user = new AppUser
                    {
                        UserName = input.UserName,
                        Pass = input.Password
                    };
                    var result = await userManager.CreateAsync(user, input.Password);
                    if (result.Succeeded)
                    {
                        var addeduser = await userManager.FindByNameAsync(input.UserName);
                        var addedRoles = input.Roles.Where(i => i.IsSelected == true).Select(i => i.Name).ToList();
                        await userManager.AddToRolesAsync(addeduser, addedRoles);
                        return RedirectToAction("GetUserList");
                    }
                }
            }
            return View(input);
        }

        public async Task<IActionResult> GetUserList()
        {
            var data = userManager.Users.Select(i => new UserListOutPut
            {
                UserName = i.UserName,
                Password = i.Pass
            }).ToList();

            foreach (var user in data)
            {
                var dataUser = await userManager.FindByNameAsync(user.UserName);
                user.Roles = (await userManager.GetRolesAsync(dataUser)).ToList();
            }
            return View(data);
        }

        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
