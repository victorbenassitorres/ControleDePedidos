using ControleDePedidos.Business.BusinessException;
using ControleDePedidos.Business.BusinessExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ControleDePedidos.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NullException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";

        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response?.StatusCode = StatusCodes.Status400BadRequest;
            context.Response?.ContentType = "application/json";
        }
        catch (ClienteExisteException ex) 
        {
            _logger.LogError(ex, ex.Message);
            context.Response?.StatusCode = StatusCodes.Status409Conflict;
            context.Response?.ContentType = "application/json";
        }
        catch (ClientePossuiPedidosException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response?.StatusCode = StatusCodes.Status409Conflict;
            context.Response? .ContentType = "application/json";
        }
        catch (ClienteIdNomeException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response?.StatusCode = StatusCodes.Status409Conflict;
            context.Response?.ContentType = "application/json";
        }
    }
}
