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
    public class ScoreController : ControllerBase
    {
        private readonly ScoreService _ScoreService;

        public ScoreController(ScoreService ScoreService)
        {
            this._ScoreService = ScoreService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Score> List = await _ScoreService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Scores information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpPost("GetByExamid")]
        public async Task<ActionResult<ApiResult>> GetByExamid([FromBody] Helper value)
        {
            var data = await _ScoreService.FindItemList(c => c.id == value.id && c.examid == value.examid);
            if (data == null)
                return ApiResultHelper.Error("This Score information does not exist.");
            return ApiResultHelper.Success(data);
        }
        [HttpPost("GetById")]
        public async Task<ActionResult<ApiResult>> GetById([FromBody] Helper value)
        {
            var data = await _ScoreService.FindItemList(c => c.id == value.id);
            if (data == null)
                return ApiResultHelper.Error("This Score information does not exist.");
            data.Sort();
            return ApiResultHelper.Success(data);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Score value)
        {
            bool res = await _ScoreService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
