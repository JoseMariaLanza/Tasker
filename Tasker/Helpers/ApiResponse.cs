using Microsoft.AspNetCore.Mvc;

namespace Tasker.Helpers
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public static ApiResponse CreateResponse(bool success, string? message, object? data = null, string key = null)
        {
            object responseData;
            if (key is not null)
            {
                var dictionary = new Dictionary<string, object> { { key, data } };
                responseData = dictionary;
            }
            else
            {
                responseData = data;
            }

            return new ApiResponse { Success = success, Message = message, Data = responseData };
        }

        public static IActionResult Ok(string message, string key = null, object data = null)
        {
            object responseData;

            if (key != null)
            {
                var dictionary = new Dictionary<string, object> { { key, data } };
                responseData = dictionary;
            }
            else
            {
                responseData = data;
            }

            return new OkObjectResult(new ApiResponse
            {
                Success = true,
                Message = message,
                Data = responseData
            });
        }

        public static IActionResult Created(string message, string key = null, object data = null)
        {
            object responseData;

            if (key != null)
            {
                var dictionary = new Dictionary<string, object> { { key, data } };
                responseData = dictionary;
            }
            else
            {
                responseData = data;
            }

            return new CreatedResult("", new ApiResponse
            {
                Success = true,
                Message = message,
                Data = responseData
            });
        }

        public static IActionResult BadRequest(string message)
        {
            return new BadRequestObjectResult(new ApiResponse
            {
                Success = false,
                Message = message
            });
        }

        public static IActionResult Unauthorized(string message)
        {
            return new UnauthorizedObjectResult(new ApiResponse
            {
                Success = false,
                Message = message
            });
        }

        public static IActionResult Forbidden(string message)
        {
            return new ObjectResult(new ApiResponse
            {
                Success = false,
                Message = message
            })
            { StatusCode = 403 };
        }

        public static IActionResult NotFound(string message)
        {
            return new NotFoundObjectResult(new ApiResponse
            {
                Success = false,
                Message = message
            });
        }

        public static IActionResult NoContent()
        {
            return new NoContentResult();
        }

        public static IActionResult UnprocessableEntity(string message)
        {
            return new ObjectResult(new ApiResponse
            {
                Success = false,
                Message = message
            })
            { StatusCode = 422 };
        }

        public static IActionResult TooManyRequests(string message = "Too many requests.")
        {
            return new ObjectResult(new ApiResponse
            {
                Success = false,
                Message = message
            })
            { StatusCode = 429 };
        }

        public static IActionResult ServiceUnavailable(string message = "Service is currently unavailable.")
        {
            return new ObjectResult(new ApiResponse
            {
                Success = false,
                Message = message
            })
            { StatusCode = 503 };
        }


        public static IActionResult ServerError(string message = "Server error")
        {
            return new ObjectResult(new ApiResponse
            {
                Success = false,
                Message = message
            })
            { StatusCode = 500 };
        }
    }
}
