using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCalendarWebDriver.Models
{
    public class GoogleEventModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? EventStart { get; set; }
        public DateTime? EventEnd { get; set; }
        public string Location { get; set; }
        public bool IsWholeDay { get; set; }

        public string GetCreateButtonFullXPath()
        {
            return "/html/body/div[2]/div[1]/div[1]/div[1]/button";
        }

        public string GetTitleFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[1]/div/div[1]/div/div[1]/input";
        }

        public string GetDateLinkFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[1]/div/div[1]/div/div[1]/div/div/div[2]/div[1]/div/span/span/div[1]/span/span";
        }

        public string GetVacantFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[1]/div";
        }

        public string GetIsWholeDayFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[1]/div/div[1]/div/div[2]/div/div[2]/div[2]/div[1]/div/label/div";
        }

        public string GetStartDateFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[1]/div/div[1]/div/div[2]/div/div[1]/div/div[2]/div/div[1]/div[1]/div/label/div[1]/div/input";
        }

        public string GetEndDateFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[1]/div/div[1]/div/div[2]/div/div[1]/div/div[2]/div/div[3]/div[1]/div/label/div[1]/div/input";
        }

        public string GetStartTimeFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[1]/div/div[1]/div/div[2]/div/div[1]/div/div[2]/div/div[2]/div[1]/div[1]/div/label/div[1]/div/input";
        }

        public string GetEndTimeFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[1]/div/div[1]/div/div[2]/div/div[1]/div/div[2]/div/div[2]/div[2]/div[1]/div/label/div[1]/div/input";
        }

        public string GetLocationLinkFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[1]/div/div[5]/div[1]/div/div[2]/div/div/span/span/span";
        }

        public string GetLocationBoxFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[1]/div/div[5]/div[2]/div/div[2]/div[2]/div/div[2]/div/div/div[1]/span/div/div[1]/div[2]/div[1]/div/div[1]/input";
        }

        public string GetDescriptionLinkFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[1]/div/div[6]/div[1]/div/div[2]/div/div/span/span/span[1]";
        }

        public string GetDescriptionBoxFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[1]/div[2]/div[2]/span[1]/div/div[6]/div[2]/div/div/div[1]/div[2]/div/div[2]/div/div/div/div[1]/div[2]/div[2]";
        }

        public string GetSaveButtonFullXPath()
        {
            return "/html/body/div[4]/div/div/div[2]/span/div/div[1]/div[3]/div[2]/div[2]";
        }
    }
}
