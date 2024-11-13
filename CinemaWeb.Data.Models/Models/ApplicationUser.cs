using CinemaWeb.Data.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace CinemaWeb.Models;

public class ApplicationUser : IdentityUser
{
    public  string? BillingInformation { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
