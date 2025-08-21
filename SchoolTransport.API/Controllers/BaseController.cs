using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SchoolTransport.API.Controllers
{


    public class BaseController : ControllerBase
    {
        protected string GetTenantId()
        {
            return HttpContext.User.FindFirst("tenantId")?.Value
                ?? throw new UnauthorizedAccessException("TenantId not found in token");
        }

        protected int GetUserId()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
                return userId;
            throw new UnauthorizedAccessException("UserId not found in token");
        }

        protected string GetUserName()
        {
            return HttpContext.User.FindFirst(ClaimTypes.Name)?.Value
                ?? throw new UnauthorizedAccessException("UserName not found in token");
        }

        protected void SetNoCacheHeaders()
        {
            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate, private");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
        }
    }
}
