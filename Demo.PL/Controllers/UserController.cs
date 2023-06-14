using Demo.DAL.Entity;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Demo.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<APPUser> _userManager;

        public UserController(UserManager<APPUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            var users = Enumerable.Empty<APPUser>().ToList();
            if (string.IsNullOrEmpty(SearchValue))

                users.AddRange(_userManager.Users);


            else

                users.Add(await _userManager.FindByEmailAsync(SearchValue));

            return View(users);


        }


        ////[HttpGet]
        ////Employee /Create
        //public async Task<IActionResult> Create()
        //{

        //    ViewBag.Departments = await _unitOfWork._DepartmentReposatory.GetAll();
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(EmployeeViewModel EmployeeVm)
        //{
        //    if (ModelState.IsValid) // server side validation
        //    {

        //        ///Manual Mapping
        //        ///    var mappedEmployee = new Employee()
        //        ///    {
        //        ///    Name= EmployeeVm.Name,
        //        ///    Adress= EmployeeVm.Adress,
        //        ///    Salary= EmployeeVm.Salary,
        //        ///    Age= EmployeeVm.Age,
        //        ///    PhoneNumber= EmployeeVm.PhoneNumber,
        //        ///    Email= EmployeeVm.Email,
        //        ///    DepartmentId= EmployeeVm.DepartmentId,  
        //        ///    IsActive= EmployeeVm.IsActive
        //        ///    };
        //        ///    

        //        EmployeeVm.ImageName = DocumentSettings.UploadFile(EmployeeVm.Image, "image");

        //        var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVm);
        //        await _unitOfWork._EmployeeRepository.Add(mappedEmployee);
        //        return RedirectToAction(nameof(Index));
        //    }



        //    return View(EmployeeVm);

        //}
        // Employee / Details 
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {

            if (id == null)
                return NotFound();
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
                return NotFound();
            return View(ViewName, User);
        }
        //User / Edit
        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");
            ///if (id == null)
            ///    return NotFound();
            ///var Employee = _EmployeeReposatory.Get(id.Value);
            ///if (Employee == null)
            ///    return NotFound();
            ///return View(Employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string Id, APPUser UpdateUser)
        {
            if (Id != UpdateUser.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(Id); 

                    user.UserName=UpdateUser.UserName;  
                    user.PhoneNumber=UpdateUser.PhoneNumber;


                    await _userManager.UpdateAsync(user);    
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(UpdateUser);
                }
            }

            return View(UpdateUser);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, APPUser DeletedUser)
        {

            if (id != DeletedUser.Id)
                return BadRequest();
            try
            {
                var user = await _userManager.FindByIdAsync(id);


                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            { 

                ModelState.AddModelError("", ex.Message);
                return View(DeletedUser);
            }
        }
    }
}
