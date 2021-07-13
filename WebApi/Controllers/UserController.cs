﻿using Microsoft.AspNetCore.Authorization;
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
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _CustomerService;

        public CustomerController(CustomerService CustomerService)
        {
            this._CustomerService = CustomerService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Customer> List = await _CustomerService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Customers information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<ApiResult>> GetByCustomerId(string id)
        {
            Customer data = await _CustomerService.FindItem(c => c.id == id);
            if (data == null)
                return ApiResultHelper.Error("This Customer information does not exist.");
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Customer value)
        {
            bool res = await _CustomerService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fill to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult<ApiResult>> DeleteByCustomerId(Helper value)
        {
            bool res = await _CustomerService.DeleteItem(c => c.id == value.id);
            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] Customer value)
        {
            Customer data = (await _CustomerService.FindItem(c => c.id == value.id));
            if (data == null) return ApiResultHelper.Error("This Customer information does not exist.");

            bool res = await _CustomerService.EditItem(data);
            if (!res) return ApiResultHelper.Error("Failed to modify Customer information:Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
