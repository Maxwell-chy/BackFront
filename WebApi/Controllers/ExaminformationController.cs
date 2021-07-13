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
    public class ExaminformationController : ControllerBase
    {
        private readonly ExaminformationService _examinformationService;

        public ExaminformationController(ExaminformationService examinformationService)
        {
            this._examinformationService = examinformationService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Examinformation> List = await _examinformationService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Examinformations information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<ApiResult>> GetByUserId(string id)
        {
            Examinformation data = await _examinformationService.FindItem(c => c.Studentid == id);
            if (data == null)
                return ApiResultHelper.Error("This Examinformation information does not exist.");
            return ApiResultHelper.Success(data);
        }
        [HttpPost("GetByName")]
        public async Task<ActionResult<ApiResult>> GetByName([FromBody] Helper value)
        {
            var data = await _examinformationService.FindItemList(c => c.Examname == value.Examname);
            if (data == null)
                return ApiResultHelper.Error("This Examinformation information does not exist.");
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Examinformation value)
        {
            bool res = await _examinformationService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult<ApiResult>> DeleteByUserId(string id)
        {
            bool res = await _examinformationService.DeleteItem(c => c.Studentid == id);
            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] Examinformation value)
        {
            Examinformation data = (await _examinformationService.FindItem(c => c.Studentid == value.Studentid));
            if (data == null) return ApiResultHelper.Error("This Examinformation information does not exist.");

            bool res = await _examinformationService.EditItem(data);
            if (!res) return ApiResultHelper.Error("Failed to modify Examinformation information:Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
