using Microsoft.AspNetCore.Identity;

namespace ExordiumGames.Models
{
    public class UserVM : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Source { get; set; }
    }
}
