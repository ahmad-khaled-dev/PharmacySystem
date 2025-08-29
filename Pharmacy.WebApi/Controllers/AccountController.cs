using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.DTO;
using Pharmacy.Core.IServiceContracts;
using System.Security.Claims;
using System.Web;


namespace Pharmacy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
 //       private readonly IEmailService _emailService;

        public AccountController(RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            IJwtService jwtService)

        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
    //        _emailService = emailService;
        }


        /// <summary>
        /// Use this method to login 
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns>it will return AuthorizationResponse include :token ,refresh token</returns>

         
            [HttpPost("login")]
       
        public async Task<IActionResult> login([FromBody]LoginDTO loginDTO)
        {
            if (User.Identity.IsAuthenticated)
            {
                return BadRequest("you are already authenticated");
            }


            ApplicationUser? user =await _userManager.FindByNameAsync(loginDTO.UserName);


            if (user == null)
            {
                return NotFound();
            }

            if (!(loginDTO.UserName == user.UserName && await _userManager.CheckPasswordAsync(user, loginDTO.Password)))
            {
                return NotFound("Invalid username or password");
            }


            var result =
           await _signInManager.PasswordSignInAsync(loginDTO.UserName,loginDTO.Password, isPersistent: false, lockoutOnFailure: true);




            if (result.Succeeded)
            {
                 
                AuthenticationResponse response = await _jwtService.createJwtToken(user);


                user.RefreshToken = response.RefreshToken;
                user.RefershTokenExpiration = response.RefreshExpiration;
                response.id=user.Id;
                response.UserName=user.UserName;


                await
                _userManager.UpdateAsync(user);

                return Ok(response);
            }
            else if (result.IsLockedOut)
            {
                return Unauthorized(new { message = "Your account is locked. Please try again later." });
            }
            else
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        /// <summary>
        /// It use to generate new Token
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns>it will return a new token as response</returns>
        [HttpPost]

        public async Task<IActionResult> GenerateNewAccessToken (TokenModelDTO tokenModel)
        {

            if(tokenModel == null)
            {
                return BadRequest("invalid client request");
            }



            ClaimsPrincipal? principal = _jwtService.GetPrincipleFromJwtToken(tokenModel.Token);

            if(principal == null)
            {
                return BadRequest("invalid jwt access token");
            }

            string? userName = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            ApplicationUser? user = await _userManager.FindByNameAsync(userName);

            if (user == null || !(user.RefreshToken == tokenModel.RefreshToken) || user.RefershTokenExpiration <=DateTime.UtcNow)
            {
                return BadRequest("invalid refresh token ");
            }


            AuthenticationResponse authenticationResponse = await _jwtService.createJwtToken(user);

            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefershTokenExpiration = authenticationResponse.RefreshExpiration;

            await _userManager.UpdateAsync(user);

            return Ok(authenticationResponse);
           }



        /// <summary>
        /// Use when you forget password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>It will return code to your email</returns>


     /*
        // 1. طلب إعادة تعيين كلمة المرور (إرسال إيميل مع التوكن)
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("لا يوجد مستخدم بهذا البريد.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"{Request.Scheme}://{Request.Host}/reset-password?email={dto.Email}&token={Uri.EscapeDataString(token)}";

            var emailBody = $"<p>اضغط على الرابط التالي لإعادة تعيين كلمة المرور:</p><p><a href='{resetLink}'>إعادة تعيين كلمة المرور</a></p>";

            await _emailService.SendEmailAsync(dto.Email, "إعادة تعيين كلمة المرور", emailBody);

            return Ok("تم إرسال رابط إعادة تعيين كلمة المرور إلى بريدك.");
        }
     */
        /// <summary>
        /// Use to reset password and put a new password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /*
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ForgotPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("لا يوجد مستخدم بهذا البريد.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var param = new Dictionary<string, string?> 
            {

                {"token",token},
                {"email",dto.Email},
            };

            var callback = QueryHelpers.AddQueryString(dto.ClientURl!, param);

            var message = new Message();

            return Ok("تم إعادة تعيين كلمة المرور بنجاح.");
        }
   
    */
    }


}
 
