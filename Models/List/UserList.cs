using System.Collections.Generic;
using DNP_Assignment4_EFC.Models.Unit;

namespace DNP_Assignment4_EFC.Models.List
{
    public class UserList
    {
        public List<User> Users { get; set; }

        public UserList()
        {
            Users = new List<User>();
        }

        public string AddUser(User newUser)
        {
            foreach (var user in Users)
            {
                if (user.UserName.Equals(newUser.UserName))
                {
                    return "This user name is used.";
                }
            }
            Users.Add(newUser);
            return null;
        }

        public UserList GetAllUser()
        {
            UserList copy = new UserList();
            foreach (var user in Users)
            {
                copy.AddUser(user.Copy());
            }
            return copy;
        }

        public bool CheckPassword(User user)
        {
            foreach (var x in Users)
            {
                if (x.UserName.Equals(user.UserName))
                {
                    return x.Password.Equals(user.Password);
                }
            }
            return false;
        }

        public User GetUserByUserName(string userName)
        {
            foreach (var user in Users)
            {
                if (user.UserName.Equals(userName))
                {
                    return user;
                }
            }
            return null;
        }

        public string UpdatePassword(User oldUser,User newUser)
        {
            string result;
            if (CheckPassword(oldUser))
            {
                GetUserByUserName(oldUser.UserName).Update(newUser);
                result = null;
            }
            else
            {
                result = "Wrong password";
            }
            return result;
        }

        public void RemoveUser(User user)
        {
            if (CheckPassword(user))
            {
                Users.Remove(GetUserByUserName(user.UserName));
            }
        }
    }
}