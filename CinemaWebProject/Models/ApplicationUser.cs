using Microsoft.AspNetCore.Identity;

namespace CinemaWebProject.Models;

public class ApplicationUser : IdentityUser
{
    public  string? BillingInformation { get; set; }
}
