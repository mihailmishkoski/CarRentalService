using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarRentalService.Domain.Models.Identity;
using CarRentalService.Domain.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<Customer> _userManager;
    private readonly SignInManager<Customer> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<Customer> userManager, SignInManager<Customer> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    // POST: api/Account/Register
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - model.DateOfBirth.Year;

            if (model.DateOfBirth > today.AddYears(-age))
            {
                age--;
            }

            // Check if the user is under 18 years old
            if (age < 18)
            {
                return BadRequest(new { message = "You must be 18 years or older to register." });
            }
            var user = new Customer
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return Ok(new { message = "Registration successful" });
            }

            return BadRequest(result.Errors);
        }

        return BadRequest(ModelState);
    }

    // POST: api/Account/Login
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    var token = GenerateJwtToken(user);
                    return Ok(new { message = "Login successful", token });
                }
            }
            return Unauthorized(new { message = "Invalid login attempt" });
        }
        return BadRequest(ModelState);
    }
    private string GenerateJwtToken(Customer user)
    {
        var secretKey = _configuration["Jwt:Secret"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.FirstName)
        ]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = credentials,
            
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.CreateEncodedJwt(tokenDescriptor);
    }



}
