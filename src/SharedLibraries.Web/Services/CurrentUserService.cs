﻿namespace SharedLibraries.Web.Services;

using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

using Application.Contracts;

public class CurrentUserService : ICurrentUser
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null)
        {
            throw new InvalidOperationException("This request does not have an authenticated user.");
        }

        this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public string UserId { get; }
}
