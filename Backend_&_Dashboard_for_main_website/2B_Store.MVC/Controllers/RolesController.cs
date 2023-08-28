using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using _2B_Store.DTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
// _2B_Store.MVC.ViewModel;

namespace _2B_Store.MVC.Controllers
{
    [Authorize(Roles = "Sup_Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;

        }
        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole rolemodel = new IdentityRole();
                rolemodel.Name = role.Name;
                IdentityResult result = await roleManager.CreateAsync(rolemodel);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            
            }
            return View(role);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var model = new RoleDto { Id = role.Id, RoleName = role.Name };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(RoleDto role)
        {
            if (ModelState.IsValid)
            {
                var existingrole = await roleManager.FindByIdAsync(role.Id);
                if (existingrole == null)
                {
                    return NotFound();
                }

                existingrole.Name = role.RoleName;
                var result = await roleManager.UpdateAsync(existingrole);
                if (result.Succeeded)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(role);
        }

        //public async task<iactionresult> addorremoveusers(string id)
        //{
        //    var role = await rolemanager.findbyidasync(id);
        //    if (role == null)
        //    {
        //        return notfound();
        //    }

        //    var model = new addorremoveusersdto
        //    {
        //        roleid = role.id,
        //        rolename = role.name,
        //        usersinrole = mapper.map<list<userdto>>(await getusersinrole(role)),
        //        allusers = mapper.map<list<userdto>>(await getallusers())
        //    };

        //    return view(model);
        //}

        //[httppost]
        //[validateantiforgerytoken]
        //public async task<iactionresult> addorremoveusers(addorremoveusersdto model)
        //{
        //    if (modelstate.isvalid)
        //    {
        //        var role = await rolemanager.findbyidasync(model.roleid);
        //        if (role == null)
        //        {
        //            return notfound();
        //        }

        //        var user = await usermanager.findbyidasync(model.userid);
        //        if (user == null)
        //        {
        //            return notfound();
        //        }

        //        identityresult result;
        //        if (model.addusertorole)
        //        {
        //            result = await usermanager.addtoroleasync(user, role.name);
        //        }
        //        else
        //        {
        //            result = await usermanager.removefromroleasync(user, role.name);
        //        }

        //        if (result.succeeded)
        //        {
        //            return redirecttoaction("addorremoveusers", new { id = role.id });
        //        }
        //        else
        //        {
        //            foreach (var error in result.errors)
        //            {
        //                modelstate.addmodelerror("", error.description);
        //            }
        //        }
        //    }

        //    // if the model is invalid or there's an error, reload the view with the updated data
        //    model.usersinrole = mapper.map<list<userdto>>(await getusersinrole(role));
        //    model.allusers = mapper.map<list<userdto>>(await getallusers());
        //    return view(model);
        //}

        public async Task<IActionResult> Delete(string id)
        {
            var Roles = roleManager.Roles;
            return View(Roles);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(IdentityRole model)
        {

            

                if (ModelState.IsValid)
                {

                    var role = await roleManager.FindByIdAsync(model.Id);

                    var result = await roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }


                }

                return View(model);

        }


        //private async Task<List<ApplicationUser>> GetUsersInRole(IdentityRole role)
        //{
        //    var usersInRole = new List<ApplicationUser>();
        //    foreach (var user in userManager.Users)
        //    {
        //        if (await userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            usersInRole.Add(user);
        //        }
        //    }

            //    return usersInRole;
            //}

            //private async Task<List<ApplicationUser>> GetAllUsers()
            //{
            //    return await userManager.Users.ToListAsync();
            //}

    }
}
