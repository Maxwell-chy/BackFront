using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Result;
using StuManage.Model;
using StuManage.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuManage.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _UserService;

        public UserController(UserService UserService)
        {
            this._UserService = UserService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<User> List = await _UserService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Users information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<ApiResult>> GetByUserId(string id)
        {
            User data = await _UserService.FindItem(c => c.Id == id);
            if (data == null)
                return ApiResultHelper.Error("This User information does not exist.");
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] User value)
        {
            bool res = await _UserService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult<ApiResult>> DeleteByUserId(string id)
        {
            bool res = await _UserService.DeleteItem(c => c.Id == id);
            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] User value)
        {
            User data = (await _UserService.FindItem(c => c.Id == value.Id));
            if (data == null) return ApiResultHelper.Error("This User information does not exist.");

            bool res = await _UserService.EditItem(data);
            if (!res) return ApiResultHelper.Error("Failed to modify User information:Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
