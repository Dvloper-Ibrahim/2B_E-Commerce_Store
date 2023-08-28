using _2B_Store.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _2B_Store.WepApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(
            UserManager<ApplicationUser>userManager,
            SignInManager<ApplicationUser>signInManager,
            IMapper mapper,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
         //   _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }


        #region MyRegister
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserSignUpDto userSignUpDto)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<ApplicationUser>(userSignUpDto);
                IdentityResult result = await _userManager.CreateAsync(user, userSignUpDto.Password);
                if (result.Succeeded)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                    };

                    await _userManager.AddClaimsAsync(user, claims);

                    await _userManager.AddToRoleAsync(user, "Customer");

                    return Ok(new { message = "User registered successfully" });
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }

            return BadRequest(ModelState);
        }
        #endregion

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok(new { message = "logged out successfully" });
        }

        private async Task<ApplicationUser> GetUserByEmailOrUserName(string emailOrUserName)
        {
            var userByEmail = await _userManager.FindByEmailAsync(emailOrUserName);
            if (userByEmail != null)
            {
                return userByEmail;
            }

            var userByUserName = await _userManager.FindByNameAsync(emailOrUserName);
            return userByUserName;
        }


        #region MyLogin
        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(UserSignInDto userSignIn)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByNameAsync(userSignIn.UserName);
        //        if (user != null)
        //        {
        //            var result = await _signInManager.PasswordSignInAsync(user, userSignIn.Password, false, false);
        //            if (result.Succeeded)
        //            {
        //                var claims = new List<Claim>
        //                {
        //                    new Claim(ClaimTypes.Name, user.UserName),
        //                    new Claim(ClaimTypes.Email, user.Email),
        //                };

        //                var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie");
        //                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        //                await HttpContext.SignInAsync("Identity.Application", claimsPrincipal);
        //                bool isCustomer = await _userManager.IsInRoleAsync(user, "Customer");

        //                return isCustomer ?
        //                    Ok(new { message = "logged in successfully" }) :
        //                    BadRequest("Invalid login attempt");
        //                //return Ok(new { message = "logged in successfully" });
        //            }
        //            else
        //            {
        //             //   Handle later for passwod ......
        //                //foreach (var error in result.Errors)
        //                //{
        //                //    ModelState.AddModelError("", error.Description);
        //                //}
        //                //  return BadRequest(ModelState.AddModelError(PasswordValidator,"incorrect");
        //                return BadRequest("Invalid username or password");
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest("Invalid login attempt");
        //        }
        //    }

        //    return BadRequest(ModelState);
        //} 
        #endregion
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserSignInDto userSignIn)
        {
            if (ModelState.IsValid)
            {
                var user = await GetUserByEmailOrUserName(userSignIn.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userSignIn.Password, true, false);
                    if (result.Succeeded)
                    {
                        bool isCustomer = await _userManager.IsInRoleAsync(user, "Customer");

                        if(!isCustomer)
                            return BadRequest("Invalid login attempt");

                        string token = await GenerateToken(user);
                        return Ok(new { message = "logged in successfully", myToken = token });
                    }
                    else
                    {
                        return BadRequest("Invalid login attempt");
                    }
                }
                else
                {
                    return BadRequest("Invalid login attempt");
                }
            }
            return BadRequest(ModelState);
        }

        private Task<string> GenerateToken(ApplicationUser user)
        {
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return null;
            //}

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("SecretKey").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, "Customer")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            //var response = new
            //{
            //    token = tokenString
            //};
            return Task.FromResult(tokenString);
        }


        [HttpGet("CheckUserName")]
        public async Task<IActionResult> CheckUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                return Ok(new { userNameExists = true });
            }
            else
            {
                return Ok(new { userNameExists = false });
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new { message = "Password changed successfully." });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        #region Delete Account 

        [Authorize(Roles = "Customer")]
        [HttpDelete("DeleteAccount/{userId}")]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in userClaims)
            {
                await _userManager.RemoveClaimAsync(user, claim);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return Ok(new { message = "Account deleted successfully." });
            }

            else
            {
                return BadRequest(result.Errors);
            }
        }

        #endregion
    }
}
