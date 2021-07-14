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
    public class AnswerController : ControllerBase
    {
        private readonly AnswerService _AnswerService;

        public AnswerController(AnswerService AnswerService)
        {
            this._AnswerService = AnswerService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Answer> List = await _AnswerService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Answers information in DB");
            return ApiResultHelper.Success(List);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Answer value)
        {
            bool res = await _AnswerService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
