using FinalProject.Core.Models;
using FinalProject.Core;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Core.Dtos.EmployeeDots;
using FinalProject.Core.Dtos.EmployeeDtos;


namespace FinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/Employee/Create_Employee
        [HttpPost("/Create_Employee")]
        public async Task<ActionResult> Create(EmployeeCreateDto employeeDto)
        {
            var employee = new Employee()
            {
                ArabicName = employeeDto.ArabicName,
                EnglishName = employeeDto.EnglishName,
                Email = employeeDto.Email,
                Password = employeeDto.Password,
                ArabicJob_Title = employeeDto.ArabicJob_Title,
                EnglishJob_Title = employeeDto.EnglishJob_Title,

                //Resume =  _unitOfWork.Employees.UploadEmployeeCV(employeeDto.Resume , null).ToString(),
                //DepartmentId = employeeDto.DepartmentId
                //Department =await _unitOfWork.Departments.GetByIdAsync(d => d.DepartmentId == employeeDto.DepartmentId)
            };
            employee.Resume = _unitOfWork.Employees.UploadEmployeeCV(employeeDto.Resume, employee);
            employee.Image = _unitOfWork.Employees.UploadImage(employeeDto.Image, employee);

            var AddEmployee = await _unitOfWork.Employees.AddAsync(employee);
            if (AddEmployee == null) return BadRequest("Employee creation failed");

            await _unitOfWork.CompleteAsync();
            return Ok(employee);
        }

        [HttpPost("/Upload_CV")]
        public async Task<ActionResult> UploadCV( int employeeId , IFormFile CVFile)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(e => e.EmployeeId == employeeId);
            if (employee == null) return NotFound("Employee not found");

            //var uploadDirectory = _hostEnvironment.WebRootPath ??
            //    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" ,"uploads") ;

            //Directory.CreateDirectory(uploadDirectory);

            if( _unitOfWork.Employees.UploadEmployeeCV( CVFile , employeeId) == null)
                return BadRequest("CV Upload failed");

            await _unitOfWork.CompleteAsync();

            return Ok("CV Uploaded Successfully");
            
            
                
        }

        //// GET: api/Employee/Get_Employee_By_Id/{id}
        //[HttpGet("/Get_Employee_By_Id/{id}")]
        //public async Task<ActionResult> Get(int id)
        //{
        //    Employee employee =await _unitOfWork.Employees.GetByIdAsync(e => e.EmployeeId == id, new[] { "Department" , "Unit" });

        //    if (employee == null)
        //        return NotFound("Epmloyee not found");

        //    return Ok(employee);
        //}

        // GET: api/Employee/Get_Employee_By_Id/{id}
        [HttpGet("/Get_Employee_By_Id/{id}")]
        public async Task<ActionResult> Get(int id , string lang)
        {
            Employee employee = await _unitOfWork.Employees.GetByIdAsync(e => e.EmployeeId == id, new[] { "Department", "Unit" });

            if (employee == null)
                return NotFound("Epmloyee not found");

            switch (lang)
            {
                case "Ar":
                    {
                        var arabicEmp = new ArabicEmployeeDto()
                        {
                            EmployeeId = employee.EmployeeId,
                            ArabicName = employee.ArabicName,
                            ArabicJob_Title = employee.ArabicJob_Title,
                            Email = employee.Email,
                            Password = employee.Password,
                            Image = employee.Image,
                            Resume = employee.Resume,
                        };
                        return Ok(arabicEmp);
                        
                    }
                case "En":
                    {
                        var englishEmp = new EnglishEmployeeDto()
                        {
                            EmployeeId = employee.EmployeeId,
                            EnglishName = employee.EnglishName,
                            EnglishJob_Title = employee.EnglishJob_Title,
                            Email = employee.Email,
                            Password = employee.Password,
                            Image = employee.Image,
                            Resume = employee.Resume,
                        };
                        return Ok(englishEmp);

                    }
            }


            return BadRequest();
        }

        // GET: api/Employee/Get_All_Employees
        //[HttpGet("/Get_All_Employees")]
        //public async Task<ActionResult> GetAll()
        //{
        //    IEnumerable<Employee> employees =await _unitOfWork.Employees.GetAllAsync(null,new[] { "Department" , "Unit" });

        //    if (employees == null) return NotFound("There is no employee added yet.");

        //    return Ok(employees);
        //}

        [HttpGet("/Get_All_Employees")]
        public async Task<ActionResult> GetAll(string lang)
        {
            IEnumerable<Employee> employees = await _unitOfWork.Employees.GetAllAsync(null, new[] { "Department", "Unit" });

            if (employees == null) return NotFound("There is no employee added yet.");

            switch (lang)
            {
                case "Ar":
                    {
                        IEnumerable<ArabicEmployeeDto> arabicEmployees = new List<ArabicEmployeeDto>() { };
                        foreach (var employee in employees)
                        {
                            arabicEmployees.Append(new ArabicEmployeeDto()
                            {
                                EmployeeId = employee.EmployeeId,
                                ArabicName = employee.ArabicName,
                                ArabicJob_Title = employee.ArabicJob_Title,
                                Email = employee.Email,
                                Password = employee.Password,
                                Image = employee.Image,
                                Resume = employee.Resume,
                            });
                        }
                        return Ok(arabicEmployees);
                    }

                case "En":
                    {
                        IEnumerable<EnglishEmployeeDto> englishEmployees = new List<EnglishEmployeeDto>() { };
                        foreach (var employee in employees)
                        {
                            englishEmployees.Append(new EnglishEmployeeDto()
                            {
                                EmployeeId = employee.EmployeeId,
                                EnglishName = employee.EnglishName,
                                EnglishJob_Title = employee.EnglishJob_Title,
                                Email = employee.Email,
                                Password = employee.Password,
                                Image = employee.Image,
                                Resume = employee.Resume,
                            });
                        }
                        return Ok(englishEmployees);
                    }

            }

            return BadRequest();
        }


        // PUT: api/Employee/Update_Employee
        [HttpPut("/Update_Employee")]
        public async Task<ActionResult> Update(EmployeeUpdateDto employeeDto)
        {
            Employee employee =await _unitOfWork.Employees.GetByIdAsync(e => e.EmployeeId == employeeDto.EmployeeId, new[] { "Department" });
            if (employee == null) return NotFound("Employee not found");

            employee = new Employee()
            {
                EmployeeId = employeeDto.EmployeeId,
                ArabicName = employeeDto.ArabicName,
                EnglishName = employeeDto.EnglishName,
                Email = employeeDto.Email,
                Password = employeeDto.Password,
                ArabicJob_Title = employeeDto.ArabicJob_Title,
                EnglishJob_Title = employeeDto.EnglishJob_Title,
                Resume = _unitOfWork.Employees.UploadEmployeeCV(employeeDto.Resume , employeeDto.EmployeeId),
                //DepartmentId = employeeDto.DepartmentId,
                Image = _unitOfWork.Employees.UploadImage(employeeDto.Image, employeeDto.EmployeeId),

                Department = await _unitOfWork.Departments.GetByIdAsync(d=> d.DepartmentId == employeeDto.DepartmentId)
            };


            

            var updatedEmployee =await _unitOfWork.Employees.UpdateAsync(employee);
            if(updatedEmployee == null) return BadRequest("Employee update operation failed");

            await _unitOfWork.CompleteAsync();
            return Ok(employee);
        }

        // DELETE: api/Employee/Delete_Employee/{id}
        [HttpDelete("/Delete_Employee/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Employee employee =await _unitOfWork.Employees.GetByIdAsync(e => e.EmployeeId == id, new[] { "Department" });

            if (employee == null)
                return NotFound("Employee Not Found");

            var DeleteEmployee = await _unitOfWork.Employees.DeleteAsync(id);
            if (DeleteEmployee == null) return BadRequest("Delete Employee operation failed");
            await _unitOfWork.CompleteAsync();
            return Ok(employee);
        }
    }
}
