using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Result;
using StuManage.Model;
using StuManage.Service;
using Microsoft.AspNetCore.Authorization;

namespace StuManage.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CompetitionController : ControllerBase
    {
        private readonly CompetitionService _competitionService;

        public CompetitionController(CompetitionService competitionService)
        {
            this._competitionService = competitionService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Competition> List = await _competitionService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Competitions information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<ApiResult>> GetByUserId(string id)
        {
            Competition data = await _competitionService.FindItem(c => c.Id == id);
            if (data == null)
                return ApiResultHelper.Error("This Competition information does not exist.");
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Competition value)
        {

            Competition data = await _competitionService.FindItem(c => c.Id == value.Id);
            if (data != null) return ApiResultHelper.Error("The same userId exists");

            bool res = await _competitionService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult<ApiResult>> DeleteByUserId(string id)
        {
            bool res = await _competitionService.DeleteItem(c => c.Id == id);
            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] Competition value)
        {
            Competition data = (await _competitionService.FindItem(c => c.Id == value.Id));
            if (data == null) return ApiResultHelper.Error("This Competition information does not exist.");

            bool res = await _competitionService.EditItem(data);
            if (!res) return ApiResultHelper.Error("Failed to modify Competition information:Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
