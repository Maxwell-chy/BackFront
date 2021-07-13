using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Result;
using StuManage.Model;
using StuManage.Service;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StuManage.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthoizeController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<ApiResult> Login([FromBody] Helper value)
        {
            User data = await _userService.FindItem(c => c.Id == value.Id && c.PassWord == value.PassWord);

            if (data != null)
            {
                // 登录成功
                var claims = new Claim[]
                {
                    new Claim("Id", data.Id.ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GBTR-OEUR1-DCS-UYTGR-SDFGTRE-ES"));
                //issuer代表颁发Token的Web应用程序，audience是Token的受理者
                var token = new JwtSecurityToken(
                    issuer: MySetting.Mysetting.issuerURL,
                    audience: MySetting.Mysetting.audienceURL,
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(10),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return ApiResultHelper.Success(jwtToken);

            }
            else
            {
                //登录失败
                return ApiResultHelper.Error("Incorrect account or password");
            }

        }
    }

}
