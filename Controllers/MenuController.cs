using BEFS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BEFS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetMenus")]
        public IActionResult GetMenus()
        {
            var menus = _context.Menus
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new {
                    x.MenuName,
                    x.Route,
                    x.font,
                    x.ischildren
                })
                .ToList();

            return Ok(menus);
        }
    }
}
