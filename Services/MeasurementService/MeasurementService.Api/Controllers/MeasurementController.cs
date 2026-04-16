using Microsoft.AspNetCore.Mvc;
using Common.Models.DTOs;
using MeasurementService.Business.Interfaces;
using Common.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MeasurementService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeasurementController : ControllerBase
{
    private readonly IMeasurementService _service;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public MeasurementController(IMeasurementService service, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _service = service;
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost("convert")]
    public async Task<IActionResult> Convert([FromBody] MeasurementApiRequest req)
    {
        try
        {
            var userId = GetOptionalUserId();
            string requestCategory = req.Category.ToLower();
            object result = null;

            switch (requestCategory)
            {
                case "length":
                    result = _service.Convert<LengthUnit>(req.Value1, req.TargetUnit!, userId);
                    break;
                case "weight":
                    result = _service.Convert<WeightUnit>(req.Value1, req.TargetUnit!, userId);
                    break;
                case "volume":
                    result = _service.Convert<VolumeUnit>(req.Value1, req.TargetUnit!, userId);
                    break;
                case "temperature":
                    result = _service.Convert<TemperatureUnit>(req.Value1, req.TargetUnit!, userId);
                    break;
                default:
                    throw new ArgumentException("Invalid Category. Use Length, Weight, Volume, or Temperature.");
            }

            // Save to history service if user is authenticated
            if (userId.HasValue)
            {
                await SaveToHistory(userId.Value, "Convert", req);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] MeasurementApiRequest req)
    {
        try
        {
            var userId = GetOptionalUserId();
            string requestCategory = req.Category.ToLower();
            object result = null;

            switch (requestCategory)
            {
                case "length":
                    result = _service.Add<LengthUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                    break;
                case "weight":
                    result = _service.Add<WeightUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                    break;
                case "volume":
                    result = _service.Add<VolumeUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                    break;
                case "temperature":
                    result = _service.Add<TemperatureUnit>(req.Value1, req.Value2, req.TargetUnit!, userId);
                    break;
                default:
                    throw new ArgumentException("Invalid Category.");
            }

            if (userId.HasValue)
            {
                await SaveToHistory(userId.Value, "Add", req);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("subtract")]
    public async Task<IActionResult> Subtract([FromBody] MeasurementApiRequest req)
    {
        try
        {
            var userId = GetOptionalUserId();
            string requestCategory = req.Category.ToLower();
            object result = null;

            switch (requestCategory)
            {
                case "length":
                    result = _service.Subtract<LengthUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                    break;
                case "weight":
                    result = _service.Subtract<WeightUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                    break;
                case "volume":
                    result = _service.Subtract<VolumeUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                    break;
                case "temperature":
                    result = _service.Subtract<TemperatureUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                    break;
                default:
                    throw new ArgumentException("Invalid Category. Use Length, Weight, Volume, or Temperature.");
            }

            if (userId.HasValue)
            {
                await SaveToHistory(userId.Value, "Subtract", req);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("divide")]
    public async Task<IActionResult> Divide([FromBody] MeasurementApiRequest req)
    {
        try
        {
            var userId = GetOptionalUserId();
            string requestCategory = req.Category.ToLower();
            double result = 0;

            switch (requestCategory)
            {
                case "length":
                    result = _service.Divide<LengthUnit>(req.Value1, req.Value2!, userId);
                    break;
                case "weight":
                    result = _service.Divide<WeightUnit>(req.Value1, req.Value2!, userId);
                    break;
                case "volume":
                    result = _service.Divide<VolumeUnit>(req.Value1, req.Value2!, userId);
                    break;
                case "temperature":
                    throw new InvalidOperationException("Temperature does not support division.");
                default:
                    throw new ArgumentException("Invalid Category.");
            }

            if (userId.HasValue)
            {
                await SaveToHistory(userId.Value, "Divide", req);
            }

            return Ok(new { Result = result });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("compare")]
    public async Task<IActionResult> Compare([FromBody] MeasurementApiRequest req)
    {
        try
        {
            var userId = GetOptionalUserId();
            string requestCategory = req.Category.ToLower();
            bool isEqual = false;

            switch (requestCategory)
            {
                case "length":
                    isEqual = _service.Compare<LengthUnit>(req.Value1, req.Value2!, userId);
                    break;
                case "weight":
                    isEqual = _service.Compare<WeightUnit>(req.Value1, req.Value2!, userId);
                    break;
                case "volume":
                    isEqual = _service.Compare<VolumeUnit>(req.Value1, req.Value2!, userId);
                    break;
                case "temperature":
                    isEqual = _service.Compare<TemperatureUnit>(req.Value1, req.Value2!, userId);
                    break;
                default:
                    throw new ArgumentException("Invalid Category.");
            }

            if (userId.HasValue)
            {
                await SaveToHistory(userId.Value, "Compare", req);
            }

            return Ok(new { AreEqual = isEqual });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { service = "MeasurementService", status = "Running", timestamp = DateTime.UtcNow });
    }

    private int? GetOptionalUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim != null && int.TryParse(claim.Value, out int userId))
        {
            return userId;
        }
        return null;
    }

    private async Task SaveToHistory(int userId, string operation, MeasurementApiRequest request)
    {
        try
        {
            var historyServiceUrl = _configuration["Services:HistoryServiceUrl"] ?? "http://localhost:5003";
            var historyData = new
            {
                UserId = userId,
                Operation = operation,
                Category = request.Category,
                Value1 = request.Value1,
                Value2 = request.Value2,
                TargetUnit = request.TargetUnit,
                Timestamp = DateTime.UtcNow
            };

            await _httpClient.PostAsJsonAsync($"{historyServiceUrl}/api/history", historyData);
        }
        catch
        {
            // Log error but don't fail the request
        }
    }
}