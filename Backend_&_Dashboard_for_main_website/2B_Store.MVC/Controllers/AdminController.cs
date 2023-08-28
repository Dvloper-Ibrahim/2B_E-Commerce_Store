using _2B_Store.Application.Services;
using _2B_Store.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace _2B_Store.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserServices _userService;
        private readonly IMapper _mapper;

        public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IUserServices userService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Sup_Admin")]
        public async Task<IActionResult> Index()
        {
            var admins = await _userService.GetAllAdmins();
            return View(admins);
        }

        #region Edit Profile

        [Authorize(Roles = "Admin,Sup_Admin")]
        public async Task<IActionResult> Edit(string Id)
        {
            UserSignUpDto userToEdit = await _userService.GetUserById(Id);
            var userProfileDto = _mapper.Map<UserProfileDto>(userToEdit);
            userProfileDto.Id = Id;
            return View(userProfileDto);
        }

        [Authorize(Roles = "Admin,Sup_Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, UserProfileDto userProfileDto)
        {
            if (ModelState.IsValid)
            {

                var existingUser = await _userManager.FindByIdAsync(Id);

                if (existingUser == null)
                {
                    return NotFound();
                }

                _mapper.Map(userProfileDto, existingUser);

                // var user = _mapper.Map<ApplicationUser>(userProfileDto);
                //user.Id = id;
                IdentityResult result = await _userManager.UpdateAsync(existingUser);
                if (result.Succeeded)
                {
                    // If user creation succeeds, sign in the user and redirect to the home page
                    // await _signInManager.SignInAsync(user, isPersistent: false);//session cookie
                    //return RedirectToAction("Index", "Home");

                    //var userId = await _userManager.GetUserIdAsync(user);
                    //var claims = new List<Claim>
                    //    {
                    //        new Claim(ClaimTypes.Name, user.UserName),
                    //        new Claim(ClaimTypes.NameIdentifier,userId)
                    //    };

                    //await _userManager.AddToRoleAsync(user, "Admin");
                    //await _signInManager.SignInAsync(user,false);
                    //await _signInManager.SignInWithClaimsAsync(user, false, claims);
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(userProfileDto);
        }

        #endregion
        [Authorize(Roles = "Sup_Admin")]
        public async Task<IActionResult> Profile(string Id)
        {
            //string usrId
            UserSignUpDto user = await _userService.GetUserById(Id);
            //var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var userProfileDto = _mapper.Map<UserProfileDto>(user);
            userProfileDto.Id = Id;
            return View(userProfileDto);
        }

        [Authorize(Roles = "Admin,Sup_Admin")]
        public async Task<IActionResult> MyProfile()
        {
            //string usrId
            //UserSignUpDto user = await _userService.GetUserById(Id);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var userProfileDto = _mapper.Map<UserProfileDto>(user);
            return View(userProfileDto);
        }

        [Authorize(Roles = "Sup_Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {

                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, role);
                if (!result.Succeeded)
                {
                    // return RedirectToAction("Index");
                }
            }

            var deleteResult = await _userManager.DeleteAsync(user);

            if (deleteResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin,Sup_Admin")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Sup_Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePassword)
        {
            if (ModelState.IsValid)
            {
                string UsrId = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value;
                var user = await _userManager.FindByIdAsync(UsrId);
                if (user != null)
                {
                    var checkpassres = await _userManager.CheckPasswordAsync(user, changePassword.CurrentPassword);
                    if (checkpassres)
                    {
                        var Change = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
                        if (Change.Succeeded)
                        {
                            return RedirectToAction("MyProfile");
                        }
                        else
                        {
                            return View(changePassword);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("CurrentPassword", "Error password !");
                    }
                }
            }
            return View();
        }
    }
}
