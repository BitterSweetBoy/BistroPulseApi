using Microsoft.AspNetCore.Identity;
namespace Module.Core.Entities;

public class User : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public virtual ICollection<SessionTicket> SessionTickets { get; set; } = new List<SessionTicket>();
}

public enum Gender
{
    Male = 1,
    Female = 2,
    Other = 3
}



