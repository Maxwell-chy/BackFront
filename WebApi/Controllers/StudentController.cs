using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Result;
using Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _StudentService;

        public StudentController(StudentService StudentService)
        {
            this._StudentService = StudentService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Student> List = await _StudentService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Students information in DB");
            return ApiResultHelper.Success(List);
        }

       
        [HttpPost("GetById")]
        public async Task<ActionResult<ApiResult>> GetById([FromBody] Helper value)
        {
            var data = await _StudentService.FindItemList(c => c.id == value.id);
            if (data == null)
                return ApiResultHelper.Error("This Student information does not exist.");
            data.Sort();
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Student value)
        {
            bool res = await _StudentService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
