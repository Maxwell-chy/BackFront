using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Result;
using StuManage.Model;
using StuManage.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StuManage.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ScoreController : ControllerBase
    {
        private readonly ScoreService _scoreService;

        public ScoreController(ScoreService scoreService)
        {
            this._scoreService = scoreService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Score> List = await _scoreService.FindItemList();
            List.Sort();
            if (List == null)
                return ApiResultHelper.Error("There is no any Scores information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<ApiResult>> GetByUserId(string id)
        {
            Score data = await _scoreService.FindItem(c => c.Id == id);
            if (data == null)
                return ApiResultHelper.Error("This Score information does not exist.");
            return ApiResultHelper.Success(data);
        }

        [HttpPost("GetByName")]
        public async Task<ActionResult<ApiResult>> GetByName([FromBody] Helper value)
        {
            var data = await _scoreService.FindItemList(c => c.ExamName == value.Examname);
            if (data == null)
                return ApiResultHelper.Error("This Score information does not exist.");
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Score value)
        {
            bool res = await _scoreService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult<ApiResult>> DeleteByUserId(string id)
        {
            bool res = await _scoreService.DeleteItem(c => c.Id == id);
            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] Score value)
        {
            Score data = (await _scoreService.FindItem(c => c.Id == value.Id));
            if (data == null) return ApiResultHelper.Error("This Score information does not exist.");

            bool res = await _scoreService.EditItem(data);
            if (!res) return ApiResultHelper.Error("Failed to modify Score information:Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
