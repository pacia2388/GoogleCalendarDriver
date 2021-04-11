using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCalendarWebDriver.Models
{
    public class GoogleReminderModel
    {
        public string Title { get; set; }
        public DateTime? DateTime { get; set; }
        public bool IsWholeDay { get; set; }

        public string GetCreateButtonFullXPath()
        {
            return "/html/body/div[2]/div[1]/div[1]/div[1]/button";
        }

        public string GetReminderLinkFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[1]/div[3]";
        }

        public string GetTitleBoxFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[1]/div/div[1]/div/div[1]/input";
        }

        public string GetDateBoxFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[3]/div/div[1]/div[2]/div/div[1]/div[1]/div[1]/div/label/div[1]/div/input";
        }

        public string GetTimeBoxFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[3]/div/div[1]/div[2]/div/div[1]/div[2]/div[1]/div/label/div[1]/div/input";
        }

        public string GetIsWholeDayFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[3]/div/div[2]/div[2]/label/div";
        }

        public string GetSaveButtonFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[2]/div[2]";
        }
    }
}
