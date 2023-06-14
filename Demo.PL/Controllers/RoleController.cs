using Demo.DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            var roles = Enumerable.Empty<IdentityRole>().ToList();
            if (string.IsNullOrEmpty(SearchValue))

                roles.AddRange(_roleManager.Roles);


            else

                roles.Add(await _roleManager.FindByNameAsync(SearchValue));

            return View(roles);


        }


        ////[HttpGet]
        //Role /Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid) // server side validation
            {
                await _roleManager.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
        // Role / Details 
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {

            if (id == null)
                return NotFound();
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
                return NotFound();
            return View(ViewName, Role);
        }
        //Role / Edit
        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");
       
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string Id, IdentityRole UpdateRole)
        {
            if (Id != UpdateRole.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _roleManager.FindByIdAsync(Id);

                    user.Name = UpdateRole.Name;


                    await _roleManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(UpdateRole);
                }
            }

            return View(UpdateRole);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IdentityRole DeletedRole)
        {

            if (id != DeletedRole.Id)
                return BadRequest();
            try
            {
                var user = await _roleManager.FindByIdAsync(id);


                await _roleManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(DeletedRole);
            }
        }
    }
}
