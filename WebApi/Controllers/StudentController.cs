using Microsoft.AspNetCore.Mvc;
using Result;
using StuManage.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StuManage.Service;
using Microsoft.AspNetCore.Authorization;

namespace StuManage.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            this._studentService = studentService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Student> List = await _studentService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any students information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<ApiResult>> GetByUserId(string id)
        {
            Student data = await _studentService.FindItem(c => c.Id == id);
            if (data == null)
                return ApiResultHelper.Error("This student information does not exist.");
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Student value)
        {

            Student data = await _studentService.FindItem(c => c.Id == value.Id);
            if (data != null) return ApiResultHelper.Error("The same userId exists");

            bool res = await _studentService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult<ApiResult>> DeleteByUserId(string id)
        {
            bool res = await _studentService.DeleteItem(c => c.Id == id);
            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] Student value)
        {
            Student data = (await _studentService.FindItem(c => c.Id == value.Id));
            if (data == null) return ApiResultHelper.Error("This student information does not exist.");

            bool res = await _studentService.EditItem(data);
            if (!res) return ApiResultHelper.Error("Failed to modify student information:Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
