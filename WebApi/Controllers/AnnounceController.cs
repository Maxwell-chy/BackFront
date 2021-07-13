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
    public class AnnounceController : ControllerBase
    {
        private readonly AnnounceService _announceService;

        public AnnounceController(AnnounceService announceService)
        {
            this._announceService = announceService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Announce> List = await _announceService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Announces information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpGet("GetByTitle")]
        public async Task<ActionResult<ApiResult>> GetByUserId(string title)
        {
            Announce data = await _announceService.FindItem(c => c.Title ==title);
            if (data == null)
                return ApiResultHelper.Error("This Announce information does not exist.");
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Announce value)
        {

            Announce data = await _announceService.FindItem(c => c.Title == value.Title);
            if (data != null) return ApiResultHelper.Error("The same userId exists");

            bool res = await _announceService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult<ApiResult>> DeleteByUserId(string title)
        {
            bool res = await _announceService.DeleteItem(c => c.Title == title);
            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] Announce value)
        {
            Announce data = (await _announceService.FindItem(c => c.Title == value.Title));
            if (data == null) return ApiResultHelper.Error("This Announce information does not exist.");

            bool res = await _announceService.EditItem(data);
            if (!res) return ApiResultHelper.Error("Failed to modify Announce information:Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
