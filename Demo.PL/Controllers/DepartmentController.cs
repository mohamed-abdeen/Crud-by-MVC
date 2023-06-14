 using Demo.BLL.Interfaces;
using Demo.BLL.Reposatory;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.PL.Controllers
{
    [Authorize]

    public class DepartmentController : Controller
    {
        private readonly IdepartmentReposatory _departmentReposatory;

        public DepartmentController(IdepartmentReposatory departmentReposatory)
        {
            _departmentReposatory = departmentReposatory;
        }
        // Department/Index
        public IActionResult Index()
        {

            // 1.ViewData
            ViewData["Message"] = "Hello View Data";

            // 2. ViewBag
            ViewBag.Message = "hello view bag";

            var departments = _departmentReposatory.GetAll();
            return View(departments);
        }


        //[HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) // server side validation
            {
                _departmentReposatory.Add(department);
                TempData["Message"] = "Department Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(department);

        }
        // Department / Details 
        public IActionResult Details(int? id, string ViewName = "Details")
        {

            if (id == null)
                return NotFound();
            var department = _departmentReposatory.Get(id.Value);
            if (department == null)
                return NotFound();
            return View(ViewName, department);
        }
        //Department / Edit
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
            ///if (id == null)
            ///    return NotFound();
            ///var department = _departmentReposatory.Get(id.Value);
            ///if (department == null)
            ///    return NotFound();
            ///return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int Id, Department department)
        {
            if (Id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentReposatory.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(department);
                }
            }

            return View(department);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, Department department)
        {

            if (id != department.Id)
                return BadRequest();
            try
            {
                _departmentReposatory.Delete(department);
                return RedirectToAction(nameof(Index)); 
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(department);
            }
        }

    }
}
