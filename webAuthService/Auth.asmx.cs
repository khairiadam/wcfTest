using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using webAuthService.DAL;
using webAuthService.Repo;

namespace webAuthService
{
    /// <summary>
    /// Summary description for Auth
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Auth : System.Web.Services.WebService
    {

        UserRepo _repo;

        public Auth()
        {
            _repo = new UserRepo();
        }
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public List<User> GetAll()
        {
            List<User> userList = new List<User>();
            try
            {
                var responce = _repo.GetUsers();
                userList = responce.ItemsList;
                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }





        [WebMethod]
        public Responce<User> SignUp(string FirstName, string LastName, string Email, string Password)
        {
            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
            };

            try
            {
                var result = _repo.AddUser(user);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }



        }



        [WebMethod]
        public Responce<User> SignIn(string email, string password)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password)) return null;

            var result = _repo.Login(email, password);

            return result;
        }

    }
}
