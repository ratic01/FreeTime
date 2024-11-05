using Agencija.Models;

namespace Agencija.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
      
        public UserType UserType { get; set; }
        public DateTime DateCreated { get; set; }

      
    }
}
