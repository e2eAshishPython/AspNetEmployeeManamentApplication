using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetEmployeeManamentApplication.Models;
using AspNetEmployeeManamentApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetEmployeeManamentApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, 
            IHostingEnvironment hostingEnvironment )
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
         
        [AllowAnonymous]
        public ViewResult Index()
        {
            var model =  _employeeRepository.GetAllEmployees();
            return View(model);
        }

        [AllowAnonymous]
        public ViewResult Details(int? ID)
        {
            
            //ViewData["Employee"] = model;
            //ViewData["Title"] = "Ashish Title"; //This Property also we can use but this will not tell any spaling mistake 
            //ViewBag.Employee = model;// We don't Use this also becose of inteligence are not showing 
            //ViewBag.title = "Employee Management";

            Employee employee = _employeeRepository.GetEmployee(ID.Value);
            if (employee == null)
            {

                Response.StatusCode = 404;
                return View("EmployeeErrorPage",ID.Value);
            }


            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Management System"
            };
            return View(homeDetailsViewModel);

        }

        [HttpGet]
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }


        

        [HttpGet]
        [Authorize]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                ID = employee.ID,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if(model.Photos != null)
                {
                    uniqueFileName = ProcessUploadedFile(model);
                }

                Employee NewEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(NewEmployee);
                return RedirectToAction("Details", new { ID = NewEmployee.ID });
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.ID);

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;


                if (model.Photos != null)
                {
                    

                    if (model.ExistingPhotoPath != null)
                    {
                        string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(UploadsFolder);

                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }
              
                _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photos != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photos.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photos.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

    }
}
