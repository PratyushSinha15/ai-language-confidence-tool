using System.Threading.Tasks;
using API.Data;
using API.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users= context.Users.ToListAsync();
        return Ok(users);
    }
    [HttpGet("test-db")]
    public async Task<IActionResult> TestDb()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getById(string id)
    {
        var user = await context.Users
            .Include(u => u.AnalysisResults).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound("User not found");

        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return Ok(user);
    }
}