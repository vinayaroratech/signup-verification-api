﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VA.Identity.Application.Jwt;
using VA.Identity.Infrastructure.Security.Authorization;

namespace VA.Identity.API.Controllers
{
    [Authorize]
    [Route("api/sample")]
    public class SampleController : MainController
    {
        private readonly ICurrentUserContext _user;

        public SampleController(ICurrentUserContext user)
        {
            _user = user;
        }

        [HttpGet("read")]
        [CustomAuthorize("Sample", "Read")]
        public IActionResult SampleActionRead()
        {
            return CustomResponse($"The user {_user.GetUserEmail()} have permission to get this!");
        }

        [HttpGet("list")]
        [CustomAuthorize("Sample", "List")]
        public IActionResult SampleActionList()
        {
            return CustomResponse($"The user {_user.GetUserEmail()} have permission to get this!");
        }
    }
}