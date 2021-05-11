using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAuthService.DAL;

namespace webAuthService.Repo
{
    public class UserRepo
    {
        AuthServiceDBEntities _db;
        Responce<User> _msg;


        public UserRepo()
        {
            _db = new AuthServiceDBEntities();
            _msg = new Responce<User>();
        }


        public Responce<User> AddUser(User user)
        {
            if (user is null)
            {
                _msg.Msg = "Failed !";

                return _msg;
            }
            user.Id = Guid.NewGuid().ToString();

            user.CreationDate = DateTime.Now;
            user.IsValid = false;

            var savedUser = _db.Users.Add(user);

            var res = _db.SaveChanges();

            if (res <= 0)
            {
                _msg.Msg = "Somethins went Wrong! Try Again.";

                return _msg;
            }

            _msg.Msg = "Success";
            _msg.Item = savedUser;

            return _msg;

        }



        public Responce<User> GetUsers()
        {

            _msg.ItemsList = _db.Users.ToList();

            if (_msg.ItemsList.Count < 0)
            {
                _msg.Msg = "Nothing To Show!";

            };

            return _msg;
        }



        public Responce<User> Login(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Password))
            {
                _msg.Msg = "Please Insert Email and Password";
                return _msg;
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == Email);
            if (user is null || user.Password != Password)
            {
                _msg.Msg = "Invalid Email Or Password";
                return _msg;
            }
            user.IsValid = true;
            _msg.Msg = "Success";
            _msg.Item = user;
            return _msg;
        }

    }
}
