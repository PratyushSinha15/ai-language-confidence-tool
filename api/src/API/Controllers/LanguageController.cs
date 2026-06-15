using System.Security.Claims;
using Business.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.DTOs.Language;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LanguageController : ControllerBase
{
    private readonly ILanguageService _languageService;
    private readonly ILogger<LanguageController> _logger;
    public LanguageController(ILanguageService languageService , ILogger<LanguageController> logger)
    {
        _languageService = languageService;
        _logger = logger;
    }

    [HttpPost("detect")]
    public async Task<IActionResult> Detect([FromBody] DetectRequestDto request)
    {
        try
        {
            var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) {
                return Unauthorized();
            }

            var result = await _languageService.DetectLanguageAsync(userId, request);
            _logger.LogInformation(
                "Sending response: {@result}",
                result
            );
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var result = await _languageService.GetHistoryAsync(userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }
}