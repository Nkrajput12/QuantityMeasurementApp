using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

namespace QuantityMeasurementApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly IQuantityMeasurementService Service;

        public MeasurementController(IQuantityMeasurementService service)
        {
            Service = service;
        }

        [HttpGet("history")]
        public IActionResult GetHistoryById()
        {
            try
            {
                var UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var history = Service.GetHistoryById(UserId);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("history/operation")]
        public IActionResult GetHistoryByOperation(string operation)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var history = Service.GetHistoryByOperation(operation, userId);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count")]
        public IActionResult GetOperationCount()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                int count = Service.GetOperationCount(userId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("convert")]
        public IActionResult Convert([FromBody] MeasurementApiRequest req)
        {
            try
            {
                var UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string requestCategory = req.Category.ToLower();
                object result = null;

                switch (requestCategory)
                {
                    case "length":
                        result = Service.Convert<LengthUnit>(req.Value1, req.TargetUnit!, UserId);
                        break;

                    case "weight":
                        result = Service.Convert<WeightUnit>(req.Value1, req.TargetUnit!, UserId);
                        break;

                    case "volume":
                        result = Service.Convert<VolumeUnit>(req.Value1, req.TargetUnit!, UserId);
                        break;

                    case "temperature":
                        result = Service.Convert<TemperatureUnit>(req.Value1, req.TargetUnit!, UserId);
                        break;

                    default:
                        throw new ArgumentException("Invalid Category. Use Length, Weight, Volume, or Temperature.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult Add(MeasurementApiRequest req)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string requestCategory = req.Category.ToLower();
                object result = null;

                switch (requestCategory)
                {
                    case "length":
                        result = Service.Add<LengthUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                        break;

                    case "weight":
                        result = Service.Add<WeightUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                        break;

                    case "volume":
                        result = Service.Add<VolumeUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                        break;

                    case "temperature":
                        result = Service.Add<TemperatureUnit>(req.Value1, req.Value2, req.TargetUnit!, userId);
                        break;

                    default:
                        throw new ArgumentException("Invalid Category.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("subtract")]
        public IActionResult Subtract(MeasurementApiRequest req)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string requestCategory = req.Category.ToLower();
                object result = null;

                switch (requestCategory)
                {
                    case "length":
                        result = Service.Subtract<LengthUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                        break;

                    case "weight":
                        result = Service.Subtract<WeightUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                        break;

                    case "volume":
                        result = Service.Subtract<VolumeUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                        break;

                    case "temperature":
                        result = Service.Subtract<TemperatureUnit>(req.Value1, req.Value2!, req.TargetUnit!, userId);
                        break;

                    default:
                        throw new ArgumentException("Invalid Category. Use Length, Weight, Volume, or Temperature.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("divide")]
        public IActionResult Divide([FromBody] MeasurementApiRequest req)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string requestCategory = req.Category.ToLower();
                double result = 0;

                switch (requestCategory)
                {
                    case "length":
                        result = Service.Divide<LengthUnit>(req.Value1, req.Value2!, userId);
                        break;

                    case "weight":
                        result = Service.Divide<WeightUnit>(req.Value1, req.Value2!, userId);
                        break;

                    case "volume":
                        result = Service.Divide<VolumeUnit>(req.Value1, req.Value2!, userId);
                        break;

                    case "temperature":
                        throw new InvalidOperationException("Temperature does not support division.");

                    default:
                        throw new ArgumentException("Invalid Category.");
                }

                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("compare")]
        public IActionResult Compare(MeasurementApiRequest req)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string requestCategory = req.Category.ToLower();
                bool isEqual = false;

                switch (requestCategory)
                {
                    case "length":
                        isEqual = Service.Compare<LengthUnit>(req.Value1, req.Value2!, userId);
                        break;

                    case "weight":
                        isEqual = Service.Compare<WeightUnit>(req.Value1, req.Value2!, userId);
                        break;

                    case "volume":
                        isEqual = Service.Compare<VolumeUnit>(req.Value1, req.Value2!, userId);
                        break;

                    case "temperature":
                        isEqual = Service.Compare<TemperatureUnit>(req.Value1, req.Value2!, userId);
                        break;

                    default:
                        throw new ArgumentException("Invalid Category.");
                }

                return Ok(new { AreEqual = isEqual });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}