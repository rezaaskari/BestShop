﻿using Microsoft.AspNetCore.Identity;

namespace BestShop.User.Infrastructure.Models;

public class ApplicationUser: IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
