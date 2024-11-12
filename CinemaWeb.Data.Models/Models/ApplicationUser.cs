using Microsoft.AspNetCore.Identity;

namespace CinemaWeb.Models;

public class ApplicationUser : IdentityUser
{
    public  string? BillingInformation { get; set; }
}
