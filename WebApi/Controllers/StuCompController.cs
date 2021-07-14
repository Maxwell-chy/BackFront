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
    public class StuCompController : ControllerBase
    {
        private readonly StuCompService _StuCompService;

        public StuCompController(StuCompService StuCompService)
        {
            this._StuCompService = StuCompService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<StuComp> List = await _StuCompService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any StuComps information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] StuComp value)
        {
            StuComp data = (await _StuCompService.FindItem(c => c.id == value.id && c.compname == value.compname));
            if (data == null) return ApiResultHelper.Error("This StuComp information does not exist.");

            bool res = await _StuCompService.EditItem(data);
            if (!res) return ApiResultHelper.Error("Failed to modify StuComp information:Server error occurred");
            return ApiResultHelper.Success(res);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] StuComp value)
        {
            bool res = await _StuCompService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }
        [HttpPost("GetByNameAndId")]
        public async Task<ActionResult<ApiResult>> GetByNameAndId([FromBody] Helper value)
        {
            var data = await _StuCompService.FindItemList(c => c.id == value.id && c.compname == value.compname);
            if (data == null)
                return ApiResultHelper.Error("This StuComp information does not exist.");
            return ApiResultHelper.Success(data);
        }
        [HttpPost("GetById")]
        public async Task<ActionResult<ApiResult>> GetById([FromBody] Helper value)
        {
            var data = await _StuCompService.FindItemList(c => c.id == value.id);
            if (data == null)
                return ApiResultHelper.Error("This StuComp information does not exist.");
            return ApiResultHelper.Success(data);
        }
        [HttpDelete("DeleteByName")]
        public async Task<ActionResult<ApiResult>> DeleteByName(Helper value)
        {
            StuComp data = (await _StuCompService.FindItem(c => c.compname == value.compname));
            if (data == null) return ApiResultHelper.Error("This StuComp information does not exist.");

            bool res = await _StuCompService.DeleteItem(c => c.compname == value.compname);
            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }

    }
}
