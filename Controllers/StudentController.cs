using MyProject1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace MyProject1.Controllers
{
    public class StudentController : ApiController
    {
        public StudentController()
        {
        }
        List<StudentViewModel> students = new List<StudentViewModel>(){
         new StudentViewModel {
             Id = 1,
             FirstName = "Neha",
             LastName = "Sinha",
             JoiningDate = DateTime.Parse(DateTime.Today.ToString()),
             Address=new AddressViewModel()
             {
                 StudentId=1,
                 Address1="jgjk",
                 Address2="hjgjh",
                 City="kjkj",
                 State="bgjhgjh"
             } },
         new StudentViewModel {
             Id = 2,
             FirstName = "Priya",
             LastName = "Singh",
             JoiningDate = DateTime.Parse(DateTime.Today.ToString()),
             Address=new AddressViewModel()
             {
                 StudentId=1,
                 Address1="bokaro",
                 Address2="steel",
                 City="bokaro",
                 State="jharkhand"
             } }
            };

        [Authorize]    //those who have authorization
        [HttpGet]
        [Route("api/Student/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello" + identity.Name);
        }

        [Authorize(Roles="admin")]    //for admin users
        [HttpGet]
        [Route("api/Student/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
            return Ok("Hello" + identity.Name + "Role: " + string.Join(",", roles.ToList()));
        }


        [AllowAnonymous]    //Anyone can access
        [HttpGet]
        [Route("api/Student/forall")]
        public IHttpActionResult GetStudentAll(bool address = true) {
            if (address)
            {
                return Ok(students);
            }
            else {
                //bye nai hoga lgta h :p
                 var tempadd = new StudentViewModel
                 {
                     Id = students[1].Id,
                     FirstName = students[1].FirstName,
                     LastName = students[1].LastName,
                     JoiningDate = students[1].JoiningDate
                 };
                 return Ok(tempadd);
            }

        }

        public IHttpActionResult GetStudentById(int id)
        {
            StudentViewModel student = students.Where(x => x.Id == id).FirstOrDefault<StudentViewModel>();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        public IHttpActionResult GetStudentByName(string name)
        {
            StudentViewModel student = students.Where(x => x.FirstName == name).FirstOrDefault<StudentViewModel>();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

    
        public IHttpActionResult PostStudents(StudentViewModel stud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            students.Add(stud);
            return CreatedAtRoute("DefaultApi", new { id = stud.Id }, stud);
            
        }

    }

  
}

