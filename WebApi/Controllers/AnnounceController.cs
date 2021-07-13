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
    public class AnnounceController : ControllerBase
    {
        private readonly AnnounceService _AnnounceService;

        public AnnounceController(AnnounceService AnnounceService)
        {
            this._AnnounceService = AnnounceService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Announce> List = await _AnnounceService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Announces information in DB");
            List.Sort();
            return ApiResultHelper.Success(List);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Announce value)
        {
            bool res = await _AnnounceService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
