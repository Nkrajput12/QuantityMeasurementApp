using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HistoryService.Business.Interfaces;
using System.Security.Claims;
using System.Text.Json;

namespace HistoryService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoryController : ControllerBase
{
    private readonly IHistoryService _historyService;

    public HistoryController(IHistoryService historyService)
    {
        _historyService = historyService;
    }

    [HttpPost]
    public IActionResult SaveHistory([FromBody] JsonElement data)
    {
        try
        {
            var userId = data.GetProperty("UserId").GetInt32();
            var operation = data.GetProperty("Operation").GetString() ?? "Unknown";
            var category = data.GetProperty("Category").GetString() ?? "Unknown";

            _historyService.SaveHistory(userId, operation, category, data);
            return Ok(new { message = "History saved successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetHistory()
    {
        try
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var history = _historyService.GetHistoryByUserId(userId);
            return Ok(history);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize]
    [HttpGet("operation/{operation}")]
    public IActionResult GetHistoryByOperation(string operation)
    {
        try
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var history = _historyService.GetHistoryByOperation(operation, userId);
            return Ok(history);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize]
    [HttpGet("count")]
    public IActionResult GetOperationCount()
    {
        try
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            int count = _historyService.GetOperationCount(userId);
            return Ok(new { count });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { service = "HistoryService", status = "Running", timestamp = DateTime.UtcNow });
    }
}