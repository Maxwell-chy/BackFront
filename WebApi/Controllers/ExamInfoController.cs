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
    public class ExamInfoController : ControllerBase
    {
        private readonly ExamInfoService _ExamInfoService;

        public ExamInfoController(ExamInfoService ExamInfoService)
        {
            this._ExamInfoService = ExamInfoService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<ExamInfo> List = await _ExamInfoService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any ExamInfos information in DB");
            return ApiResultHelper.Success(List);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] ExamInfo value)
        {
            bool res = await _ExamInfoService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
