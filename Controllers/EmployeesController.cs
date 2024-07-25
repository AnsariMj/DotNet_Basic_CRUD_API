﻿using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    //localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get All Employees
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var allEmployes = dbContext.Employees.ToList();
            return Ok(allEmployes);
        }

        //Get Employee by ID
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        //Add New Employess
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Address = addEmployeeDto.Address,
                Salary = addEmployeeDto.Salary,
            };
            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            return Ok(employeeEntity);
        }


        //Update Employee Details
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Address = updateEmployeeDto.Address;
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();
            return Ok(employee);
        }


        //Delete Employee Details
        [HttpPost]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok(); 
        }
    }
}
