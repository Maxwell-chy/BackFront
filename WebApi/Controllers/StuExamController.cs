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
    //[Authorize]
    public class StuExamController : ControllerBase
    {
        private readonly StuExamService _StuExamService;

        public StuExamController(StuExamService StuExamService)
        {
            this._StuExamService = StuExamService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<StuExam> List = await _StuExamService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any StuExams information in DB");
            return ApiResultHelper.Success(List);
        }
        [HttpPost("GetById")]
        public async Task<ActionResult<ApiResult>> GetById([FromBody] StuExam value)
        {
            List<StuExam> List = await _StuExamService.FindItemList(c=>c.id==value.id);
            if (List == null)
                return ApiResultHelper.Error("There is no any StuExams information in DB");
            return ApiResultHelper.Success(List);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] StuExam value)
        {
            bool res = await _StuExamService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
