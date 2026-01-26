using BEFS.Data;
using BEFS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace BEFS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] DTOs.LoginRequest request)
        {
            byte[] hash, salt;
            PasswordHasher.CreatePasswordHash("123456", out hash, out salt);

      
            // 1️⃣ Validate Captcha
            var sessionCaptcha = HttpContext.Session.GetString("CaptchaCode");
            if (sessionCaptcha == null || request.Captcha != sessionCaptcha)
                return BadRequest("Invalid captcha");

            // 2️⃣ Get user from DB
            var user = _context.Users
                .FirstOrDefault(x => x.UserName == request.LoginId && x.IsActive);

            if (user == null)
                return Unauthorized("Invalid username or password");

            // 3️⃣ Verify password using HMACSHA256
            var isValidPassword = BEFS.Utility.PasswordHasher.VerifyPassword(
                request.Password,
                user.PasswordHash,
                user.PasswordSalt
            );

            if (!isValidPassword)
                return Unauthorized("Invalid username or password");

            // 4️⃣ Success
            return Ok(new
            {
                message = "Login successful"
            });
        }
    }
}
