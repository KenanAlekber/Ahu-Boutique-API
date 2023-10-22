using Ahu.API.Services;
using Ahu.Business.DTOs.UserDtos;
using Ahu.Business.Exceptions;
using Ahu.Business.Helper;
using Ahu.Business.Services.Interfaces;
using Ahu.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ahu.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtService _jwtService;
    private readonly IEmailSender _emailSender;
    private readonly TokenEncoderDecoder _tokenEncDec;
    private readonly IConfiguration _configuration;

    public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, JwtService jwtService, IEmailSender emailSender, TokenEncoderDecoder tokenEncDec, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtService = jwtService;
        _emailSender = emailSender;
        _tokenEncDec = tokenEncDec;
        _configuration = configuration;
    }

    [HttpGet("UserData/{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get(string id)
    {
        AppUser user = await _userManager.FindByIdAsync(id);

        if (user is null)
            return NotFound();

        UserGetDto userGetDto = new UserGetDto();
        userGetDto.Id = user.Id;
        userGetDto.FullName = user.FullName;
        userGetDto.UserName = user.UserName;
        userGetDto.Address = user.Address;
        userGetDto.Phone = user.Phone;
        userGetDto.Email = user.Email;
        userGetDto.EmailConfirm = user.EmailConfirmed;
        userGetDto.IsAdmin = user.IsAdmin;

        return Ok(userGetDto);
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(string id)
    {
        AppUser user = await _userManager.FindByIdAsync(id);

        if (user is null)
            return NotFound();

        UserGetDto userGetDto = new UserGetDto();
        userGetDto.Id = user.Id;
        userGetDto.FullName = user.FullName;
        userGetDto.UserName = user.UserName;
        userGetDto.Address = user.Address;
        userGetDto.Phone = user.Phone;
        userGetDto.Email = user.Email;
        userGetDto.EmailConfirm = user.EmailConfirmed;
        userGetDto.IsAdmin = user.IsAdmin;

        return Ok(userGetDto);
    }

    [HttpGet("AllUsers")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    //[Authorize(Roles = "Admin")]
    public IActionResult GetAllUsers()
    {
        var users = _userManager.Users;

        if (users is null)
            return NotFound();

        List<UserGetDto> userDtos = new List<UserGetDto>();

        foreach (var user in users)
        {
            UserGetDto userGetDto = new UserGetDto
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Address = user.Address,
                Phone = user.Phone,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                EmailConfirm = user.EmailConfirmed
            };

            userDtos.Add(userGetDto);
        }

        return Ok(userDtos);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterDto registerDto)
    {
        if (_userManager.Users.Any(x => x.UserName == registerDto.Username))
            return Conflict("Username is alredy taken");

        if (_userManager.Users.Any(x => x.Email == registerDto.Email))
            return Conflict("Email is alredy taken");

        AppUser user = new AppUser
        {
            UserName = registerDto.Username,
            FullName = registerDto.Fullname,
            Email = registerDto.Email,
            Address = registerDto.Address,
            Phone = registerDto.Phone,
            IsAdmin = false
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            List<RestExceptionError> errors = new List<RestExceptionError>();

            foreach (var resultError in result.Errors)
            {
                RestExceptionError error = new RestExceptionError("Error", resultError.Description);
                errors.Add(error);
            }
            RestException exceptionError = new RestException(System.Net.HttpStatusCode.BadRequest, errors);

            return NotFound(exceptionError);
        }
        await _userManager.AddToRoleAsync(user, "Member");

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtService.GenerateToken(user, roles);
        string encodedToken = _tokenEncDec.EncodeToken(token);
        var reactAppUrl = _configuration["FrontUrl:BaseUrl"] + $"confirm-email?token={encodedToken}&email={user.Email}";

        _emailSender.Send(user.Email, "Email Confirme", $"Click <a href=\"{reactAppUrl}\">here</a> to verification your email");

        return Ok();
    }

    [HttpPut("UserEdit/{id}")]
    //[Authorize]
    public async Task<IActionResult> Edit(UserPutDto userPutDto, string id)
    {
        AppUser user = await _userManager.FindByIdAsync(id);

        user.FullName = userPutDto.FullName;
        user.Email = userPutDto.Email;
        user.UserName = userPutDto.UserName;
        user.Address = userPutDto.Address;
        user.Phone = userPutDto.Phone;

        if (!await _userManager.CheckPasswordAsync(user, userPutDto.Password))
            return BadRequest("Password is not correct");

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
            return Ok("User Data Changed");

        return BadRequest();
    }

    [HttpPut("ChangePassword")]
    //[Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto passwordDto, string id)
    {
        AppUser user = await _userManager.FindByIdAsync(id);

        if (!await _userManager.CheckPasswordAsync(user, passwordDto.Password))
            return BadRequest();

        var newPass = await _userManager.ChangePasswordAsync(user, passwordDto.Password, passwordDto.NewPassword);

        if (!newPass.Succeeded)
            return BadRequest("test");

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto loginDto)
    {
        AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null)
            return Unauthorized();

        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);

        var responseData = new
        {
            token = _jwtService.GenerateToken(user, roles),
            user
        };

        return Ok(responseData);
    }

    [HttpPost("loginadmin")]
    public async Task<IActionResult> LoginAsAdmin(UserLoginDto loginDto)
    {
        AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null)
            return Unauthorized();

        if (!user.IsAdmin)
            return Unauthorized();

        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);

        var responseData = new
        {
            token = _jwtService.GenerateToken(user, roles),
            user
        };

        return Ok(responseData);
    }

    [HttpGet("Logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpDelete("{email}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (!user.IsAdmin)
            await _userManager.DeleteAsync(user);

        return Ok();
    }

    [HttpGet("Role")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateRole()
    {
        await _roleManager.CreateAsync(new IdentityRole("Member"));
        await _roleManager.CreateAsync(new IdentityRole("Moderator"));
        await _roleManager.CreateAsync(new IdentityRole("Admin"));

        return Ok();
    }

    [HttpGet("CreateModerator")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateModerator()
    {
        var user = new AppUser
        {
            UserName = "Moderator",
            FullName = "Moderator",
            Email = "moderator@gmail.com",
            IsAdmin = true
        };

        await _userManager.CreateAsync(user, "Moderator123!");
        await _userManager.AddToRoleAsync(user, "Moderator");

        return Ok();
    }

    //[HttpGet("CreateAdmin")]
    //public async Task<IActionResult> CreateAdmin()
    //{
    //    var user = new AppUser
    //    {
    //        UserName = "Admin",
    //        FullName = "Admin Admmin",
    //        Email = "admin@gmail.com",
    //        IsAdmin = true
    //    };

    //    await _userManager.CreateAsync(user, "Admin123!");
    //    await _userManager.AddToRoleAsync(user, "Admin");

    //    return Ok();
    //}
}