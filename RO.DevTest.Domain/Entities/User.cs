using Microsoft.AspNetCore.Identity;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Domain.Entities;

/// <summary>
/// Represents a <see cref="IdentityUser"/> int the API
/// </summary>
public class User : IdentityUser {
    /// <summary>
    /// Name of the user
    /// </summary>
    public string Name { get; set; } = string.Empty;

    public UserRoles Role { get; set; }

    public User() : base() { }
}
