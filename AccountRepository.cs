using GoogleCalendarWebDriver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoogleCalendarWebDriver
{
    public class AccountRepository
    {
        private readonly Func<string> _inputProvider;
        private readonly Action<string> _outputProvider;
        public AccountRepository(Func<string> inputProvider, Action<string> outputProvider)
        {
            _inputProvider = inputProvider;
            _outputProvider = outputProvider;
        }
        public UserAccountModel GetDefault()
        {
            return new UserAccountModel { Email = "jhay.webdriver@gmail.com", Password = "P@55w0rd123" };
        }

        public UserAccountModel GetAccountFromUser()
        {
            string email = GetEmail();
            if (email == null) return null;

            string pass = GetPassword();
            if (pass == null) return null;

            return new UserAccountModel { Email = email, Password = pass };
        }

        private string GetEmail()
        {
            var email = "";
            while (email == "")
            {
                _outputProvider("Enter email address (X to go back):");
                email = _inputProvider();

                if (email == "X") return null;

                if (IsValidEmail(email, out string msg) == false)
                {
                    _outputProvider(msg);
                    email = "";
                }
            }
            return email;
        }

        private string GetPassword()
        {
            var pass = "";
            while (pass == "")
            {
                _outputProvider("Enter password (X to go back):");
                pass = _inputProvider();

                if (pass == "X") return null;

                if (IsValidPassword(pass, out string msg) == false)
                {
                    _outputProvider(msg);
                    pass = "";
                }
            }
            return pass;
        }

        private bool IsValidEmail(string email, out string message)
        {
            message = "";
            if (string.IsNullOrEmpty(email))
            {
                message = "Email is required";
                return false;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                message = "Email is required";
                return false;
            }
            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                 + "@"
                                 + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

            if (regex.IsMatch(email) == false)
            {
                message = "Invalid email address";
                return false;
            }

            return true;
        }

        private bool IsValidPassword(string pass, out string message)
        {
            message = "";
            if (string.IsNullOrEmpty(pass))
            {
                message = "Email is required";
                return false;
            }
            if (string.IsNullOrWhiteSpace(pass))
            {
                message = "Email is required";
                return false;
            }
            return true;
        }
    }
}
