﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpoyeeDataAccess;

namespace EmpoyeeService.Controllers
{
    public class EmployeesController : ApiController
    {

        //Get Method
        public IEnumerable<Employee> Get()
        {
            using(EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }

        //Get Method By ID
        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity =  entities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + id.ToString() + " not found");
                }
            }
        }

        //Post Method
        public HttpResponseMessage Post([FromBody] Employee employee)
        {

            try
            {

            
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                entities.Employees.Add(employee);
                entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, employee);

                message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());

                    return message;
            }
            }
            catch (Exception ex)
            {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }
    }
}
