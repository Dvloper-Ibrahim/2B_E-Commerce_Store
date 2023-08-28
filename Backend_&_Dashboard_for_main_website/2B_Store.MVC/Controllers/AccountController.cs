using _2B_Store.Application.Services;
using _2B_Store.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace _2B_Store.MVC.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserServices _userService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IUserServices userService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin,Sup_Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllCustomers();

            return View(users);
            //if (string.IsNullOrEmpty(roleName))
            //{
            //    var allUsers = await _userService.GetAllUsers();
            //    return View(allUsers);
            //}

            //var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            //var userDtoList = _mapper.Map<List<UserSignUpDto>>(usersInRole);
            //return View(userDtoList);

        }

        // [HttpGet]

        //public IActionResult Index()
        //{
        //    return View();
        //}
        
        public async Task<IActionResult> Login()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserSignInDto userLogin)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userLogin.UserName);
                if (user != null)
                {
                    bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                    bool isSupAdmin = await _userManager.IsInRoleAsync(user, "Sup_Admin");
                    if(isAdmin || isSupAdmin)
                    {
                        SignInResult result = await _signInManager.PasswordSignInAsync(user, userLogin.Password, userLogin.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                        else
                            ModelState.AddModelError("", "Invalid username or password");
                    }
                    else
                        ModelState.AddModelError("", "Invalid username or password");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return View();
        }

        [Authorize(Roles = "Sup_Admin")]
        public IActionResult Register()
        {
            UserSignUpDto userSignUp = new UserSignUpDto();
            
            
            return View(userSignUp);
        }

        [Authorize(Roles = "Sup_Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserSignUpDto userSignUpDto)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<ApplicationUser>(userSignUpDto);
                IdentityResult result = await _userManager.CreateAsync(user, userSignUpDto.Password);
                if (result.Succeeded)
                {
                    // If user creation succeeds, sign in the user and redirect to the home page
                    //await _signInManager.SignInAsync(user, isPersistent: false);//session cookie
                    //return RedirectToAction("Index", "Home");

                    //var id = await _userManager.GetUserIdAsync(user);
                    //var claims = new List<Claim>
                    //    {
                    //        new Claim(ClaimTypes.Name, user.UserName),
                    //        new Claim(ClaimTypes.NameIdentifier,id)
                    //    };

                    await _userManager.AddToRoleAsync(user, "Admin");
                    //await _signInManager.SignInAsync(user,false);
                    //await _signInManager.SignInWithClaimsAsync(user, false, claims);
                    return RedirectToAction("login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(userSignUpDto);
        }

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
                var user = _mapper.Map<ApplicationUser>(userProfileDto);
                //user.Id = id;
                IdentityResult result = await _userManager.UpdateAsync(user);
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
                    return RedirectToAction("index","Home");
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

        [Authorize(Roles = "Admin,Sup_Admin")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        //[HttpGet]
        [Authorize(Roles = "Admin,Sup_Admin")]
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

        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Profile(UserProfileDto userProfileDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        if (user != null)
        //        {
        //            _mapper.Map(userProfileDto, user);
        //            var result = await _userManager.UpdateAsync(user);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("index");
        //            }
        //            else
        //            {
        //                foreach (var error in result.Errors)
        //                {
        //                    ModelState.AddModelError("", error.Description);
        //                }
        //            }
        //        }
        //    }
        //    return View(userProfileDto);
        //}

        [Authorize(Roles = "Admin,Sup_Admin")]
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
                            return RedirectToAction("Profile");
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














        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }
        //        }
        //    }

        //    return View(userSignUpDto);
        //}

        // [HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(UserSignInDto userSignInDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(userSignInDto.Email, userSignInDto.Password,
        //            userSignInDto.RememberMe, lockoutOnFailure: false);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Invalid login attempt.");
        //        }
        //    }

        //    return View(userSignInDto);
        //}

        //[Authorize]

        //[Authorize]
        //public async Task<IActionResult> Details(string id)
        //{
        //    var user = await _userService.GetUserById(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    var user = await _userService.GetUserById(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, UserSignUpDto userDto)
        //{
        //    if (id != userDto.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var updatedUser = await _userService.UpdateUser(id, userDto);
        //        if (updatedUser == null)
        //        {
        //            return NotFound();
        //        }

        //        return RedirectToAction("Index");
        //    }

        //    return View(userDto);
        //}

        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    await _userService.DeleteUser(id);
        //    return RedirectToAction("Index");
        //}






    }


}




//private readonly IUserServices _userServices;

//public UserAccountController(IUserServices userServices)
//{
//    _userServices = userServices;
//}


//public IActionResult Registration()
//{
//    return View();
//}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public IActionResult Registration (UserSignUpDto applicationUser)
//{
//    if (ModelState.IsValid)
//    {

//    }
//    return View();
//}

//public async Task<IActionResult> Index(string role)
//{
//    var users = await _userServices.GetAllUsers();

//    if (!string.IsNullOrEmpty(role) && Enum.TryParse(role, out Role roleEnum))
//    {
//        users = users.Where(u => u.Role == roleEnum).ToList();
//    }

//    return View(users);
//}

//public IActionResult Create()
//{
//    var userDto = new UserSignUpDto();
//    return View(userDto);
//}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Create(UserSignUpDto userDto)
//{
//    if (ModelState.IsValid)
//    {

//        //   string hashedPassword = BCrypt.HashPassword(userDto.Password);

//        var newUser = new UserDTO
//        {
//            FirstNameEN = userDto.FirstNameEN,

//            LastNameEN = userDto.LastNameEN,
//            Email = userDto.Email,
//            PhoneNumber = userDto.PhoneNumber,
//            //   Password = hashedPassword // Save the hashed password
//        };

//        // var addedUser = await _userServices.AddUser(newUser);

//        return RedirectToAction("Index");
//    }

//    return View(userDto);
//}
