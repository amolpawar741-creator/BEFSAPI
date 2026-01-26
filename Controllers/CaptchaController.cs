using BEFS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BEFS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        [HttpGet("generate")]
        public IActionResult GenerateCaptcha()
        {
            var captchaText = new Random().Next(100000, 999999).ToString();

            HttpContext.Session.SetString("CaptchaCode", captchaText);

            var imageBytes = CaptchaGenerator.GenerateCaptchaImage(captchaText);
            var base64Image = Convert.ToBase64String(imageBytes);

            return Ok(new
            {
                captchaImage = $"data:image/png;base64,{base64Image}"
            });
        }
    }
}
