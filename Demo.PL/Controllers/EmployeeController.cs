using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Reposatory;
using Demo.DAL.Entity;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Employee/Index
        public async Task<IActionResult> Index(string SearchValue)
        {
            var Employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(SearchValue))

                Employees = await _unitOfWork._EmployeeRepository.GetAll();


            else

                Employees = _unitOfWork._EmployeeRepository.GetEmployeeByName(SearchValue);

            var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
            return View(mappedEmployees);


        }


        //[HttpGet]
        //Employee /Create
        public async Task<IActionResult> Create()
        {

            ViewBag.Departments = await _unitOfWork._DepartmentReposatory.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel EmployeeVm)
        {
            if (ModelState.IsValid) // server side validation
            {

                ///Manual Mapping
                ///    var mappedEmployee = new Employee()
                ///    {
                ///    Name= EmployeeVm.Name,
                ///    Adress= EmployeeVm.Adress,
                ///    Salary= EmployeeVm.Salary,
                ///    Age= EmployeeVm.Age,
                ///    PhoneNumber= EmployeeVm.PhoneNumber,
                ///    Email= EmployeeVm.Email,
                ///    DepartmentId= EmployeeVm.DepartmentId,  
                ///    IsActive= EmployeeVm.IsActive
                ///    };
                ///    

                EmployeeVm.ImageName = DocumentSettings.UploadFile(EmployeeVm.Image, "image");

                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVm);
                await _unitOfWork._EmployeeRepository.Add(mappedEmployee);
                return RedirectToAction(nameof(Index));
            }



            return View(EmployeeVm);

        }
        // Employee / Details 
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {

            if (id == null)
                return NotFound();
            var Employee = await _unitOfWork._EmployeeRepository.Get(id.Value);
            if (Employee == null)
                return NotFound();
            var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(Employee);
            return View(ViewName, mappedEmployee);
        }
        //Employee / Edit
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments = await _unitOfWork._DepartmentReposatory.GetAll();

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
        public async Task<IActionResult> Edit([FromRoute] int Id, EmployeeViewModel EmployeeVm)
        {
            //if (Id != Employee.Id)
            //    return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVm);
                    await _unitOfWork._EmployeeRepository.Update(mappedEmployee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(EmployeeVm);
                }
            }

            return View(EmployeeVm);
        }

        public async Task <IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete(int? id, EmployeeViewModel EmployeeVM)
        {


            try
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                int count =await _unitOfWork._EmployeeRepository.Delete(mappedEmployee);
                if (count > 0)
                    DocumentSettings.DeleteFile(EmployeeVM.ImageName, "Images");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(EmployeeVM);
            }
        }

    }
}
