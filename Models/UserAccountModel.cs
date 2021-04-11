using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCalendarWebDriver.Models
{
    public class UserAccountModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string GetEmailBoxFullXPath()
        {
            return "/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/div/div[1]/div/div[1]/input";
        }

        public string GetPasswordBoxFullXPath()
        {
            return "/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/div[1]/div/div/div/div/div[1]/div/div[1]/input";
        }

        public string GetSubmitButtonFullXPath()
        {
            return "/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[2]/div/div[1]/div/div/button";
        }

        public string GetEmailValidationMessageFullXPath()
        {
            return "/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/div/div[2]/div[2]/div";
        }

        public string GetPasswordValidationMessageFullXPath()
        {
            return "/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/div[2]/div[2]/span";
        }

        public string GetCatchaBoxFullXPath()
        {
            return "/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[4]/div/div/div[1]/div/div[1]/input";
        }

        public string GetVerificationListFullXPath()
        {
            return "/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div/ul";
        }
    }
}
