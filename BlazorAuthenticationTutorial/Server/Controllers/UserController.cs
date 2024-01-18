using BlazorAuthenticationTutorial.Server.Data;
using BlazorAuthenticationTutorial.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAuthenticationTutorial.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public UserController(DataContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<SystemUser>>> GetSystemUsers()
        {
            try
            {
                _logger.LogInformation("Here Logo Message : UserContorller");
                var systemUsers = await _context.SystemUsers.ToListAsync();
                return Ok(systemUsers);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<List<SystemUser>> GetDbSystemUsers()
        {
            return await _context.SystemUsers.ToListAsync();
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<UserRole>>> GetRoles()
        {
            var userRoles = await _context.UserRoles.ToListAsync();
            return Ok(userRoles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SystemUser>> GetSingleUser(int id)
        {
            var hero = await _context.SystemUsers
                .FirstOrDefaultAsync(h => h.Id == id);
            if (hero == null)
            {
                return NotFound("Sorry, no hero here. :/");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SystemUser>>> CreateSystemUser(SystemUser user)
        {
            try {
                _context.SystemUsers.Add(user);
                await _context.SaveChangesAsync();

                return Ok(await GetDbSystemUsers());
            }
            catch (Exception ex) {
                return Ok(ex);
            }
             
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SystemUser>>> UpdateSystemUser(SystemUser user, int id)
        {
            var dbUser = await _context.SystemUsers
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbUser == null)
                return NotFound("Sorry, but no hero for you. :/");

            dbUser.Name = user.Name;
            dbUser.Email = user.Email;
            //dbUser.RoleId = user.RoleId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbSystemUsers());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SystemUser>>> DeleteSystemUser(int id)
        {
            var dbUser = await _context.SystemUsers
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbUser == null)
                return NotFound("Sorry, but no hero for you. :/");

            _context.SystemUsers.Remove(dbUser);
            await _context.SaveChangesAsync();

            return Ok(await GetDbSystemUsers());
        }

        


    }
}
