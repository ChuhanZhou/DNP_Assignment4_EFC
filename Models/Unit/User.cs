using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DNP_Assignment4_EFC.Models.Unit 
{
    public class User
    {
        [Key,NotNull]
        public string UserName { get; set; }
        [NotNull]
        public string Password { get; set; }

        public void Update(User user)
        {
            if (user.UserName.Equals(UserName))
            {
                Password = user.Password;
            }
        }

        public User Copy()
        {
            return new User
            {
                UserName = UserName,
                Password = Password
            };
        }
    }
}