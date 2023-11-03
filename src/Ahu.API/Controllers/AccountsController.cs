using Ahu.Business.DTOs.UserDtos;
using Ahu.Business.Helper;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ahu.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly TokenEncoderDecoder _tokenEncDec;
    private readonly IConfiguration _configuration;
    public AccountsController(UserManager<AppUser> userManager, IEmailSender emailSender, TokenEncoderDecoder tokenEncDec, IConfiguration configuration)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _tokenEncDec = tokenEncDec;
        _configuration = configuration;
    }

    [HttpGet("SendConfirmEmailToken")]
    [Authorize]
    public async Task<IActionResult> CreateToken()
    {
        string userName = User.Identity.Name;

        AppUser user = await _userManager.FindByNameAsync(userName);

        if (user is null)
            return NotFound();

        if (user.EmailConfirmed == true)
            return Ok("Your Email Alredy Confirmed");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        string encodedToken = _tokenEncDec.EncodeToken(token);

        var reactAppUrl = _configuration["FrontUrl:BaseUrl"] + $"confirm-email?token={encodedToken}&email={user.Email}";

        _emailSender.Send(user.Email, "Email Confirme", $"Click <a href=\"{reactAppUrl}\">here</a> to verification your email");

        return Ok();
    }

    [HttpPost("ConfirmEmail")]
    [Authorize]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmailDto)
    {
        string token = _tokenEncDec.DecodeToken(confirmEmailDto.Token);

        if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(confirmEmailDto.Email))
            return BadRequest("Token and email are required.");

        var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);

        if (user is null) return NotFound("User not found.");

        var result = await _userManager.ConfirmEmailAsync(user, token);

        return Ok("Email confirmed successfully.");
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgetPassword(ForgotPasswordDto passwordDto)
    {
        AppUser user = await _userManager.FindByEmailAsync(passwordDto.Email);

        if (user == null || user.IsAdmin)
            return BadRequest("Email is not correct");

        string token = await _userManager.GeneratePasswordResetTokenAsync(user);

        string encodedToken = _tokenEncDec.EncodeToken(token);
        string resetPasswordUrl = $"{Request.Scheme}://{Request.Host}/api/accounts/resetpassword?encodedToken={encodedToken}&email={passwordDto.Email}";

        _emailSender.Send(passwordDto.Email, "Reset Password", $"Click <a href=\"{resetPasswordUrl}\">here</a> to reset your password");

        return Ok();
    }

    [HttpGet("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromQuery] string encodedToken, [FromQuery] string email)
    {
        string token = _tokenEncDec.DecodeToken(encodedToken);

        if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(email))
            return BadRequest("Token and email are required.");

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return NotFound("User not found.");

        var result = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);

        if (result)
        {
            var reactAppUrl = _configuration["FrontUrl:BaseUrl"] + $"reset-password?token={encodedToken}&email={email}";
            return Redirect(reactAppUrl);
        }

        return BadRequest("error");
    }

    [HttpPost("ResetPasswordChange")]
    public async Task<IActionResult> ResetPassword(ResetPasswordPostDto resetPassword)
    {
        string token = _tokenEncDec.DecodeToken(resetPassword.Token);

        if (resetPassword.Password != resetPassword.ConfirmPassword)
            return BadRequest("Password is don't match");

        var user = await _userManager.FindByEmailAsync(resetPassword.Email);

        if (user is null || user.IsAdmin)
            return BadRequest("Email is not correct");

        var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.Password);

        if (!result.Succeeded)
            return BadRequest("Password reset failed");

        return Ok("Password reset successful");
    }
}