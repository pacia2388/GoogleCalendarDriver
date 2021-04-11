using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCalendarWebDriver.Models
{
    public class GoogleTaskModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DateTime { get; set; }
        public bool IsWholeDay { get; set; }
        public string GetCreateButtonFullXPath()
        {
            return "/html/body/div[2]/div[1]/div[1]/div[1]/button";
        }
        public string GetTaskLinkFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[1]/div[2]";
        }

        public string GetTitleFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[1]/div/div[1]/div/div[1]/input";
        }

        public string GetDateBoxFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[2]/div/div[1]/div[2]/div[1]/div/div[1]/div[1]/div[1]/div/label/div[1]/div/input";
        }

        public string GetTimeBoxFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[2]/div/div[1]/div[2]/div[1]/div/div[1]/div[2]/div[1]/div/label/div[1]/div/input";
        }

        public string GetIsWholeDayFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[2]/div/div[1]/div[2]/div[2]/label/div/div[2]";
        }

        public string GetDescriptionBoxFUllXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[2]/div/div[2]/div[2]/div/label/div[2]/textarea";
        }

        public string GetSaveButtonFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[2]/div[2]";
        }
    }
}
