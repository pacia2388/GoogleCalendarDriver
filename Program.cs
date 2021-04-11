using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GoogleCalendarWebDriver.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace GoogleCalendarWebDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionHelper = new OptionHelper(Console.ReadLine, Console.WriteLine);

            int navigationLevel = 1;
            BrowserType browserType = BrowserType.Undefined;
            UserAccountModel account = null;
            object googleCalendarObj = null;

            // test only
            //browserType = BrowserType.GoogleChrome;
            //account = new UserAccountModel { Email = "jhay.webdriver@gmail.com", Password = "P@55w0rd123" };
            //googleCalendarObj = new GoogleEventModel
            //{
            //    Title = "Title",
            //    Description = "Description",
            //    EventStart = DateTime.Now.AddDays(5),
            //    EventEnd = DateTime.Now.AddDays(5),
            //    IsWholeDay = true,
            //    Location = "Mandaue"
            //};

            //googleCalendarObj = new GoogleTaskModel
            //{
            //    Title = "Title",
            //    Description = "Description",
            //    DateTime = DateTime.Now.AddDays(10),
            //    IsWholeDay = true
            //};

            //googleCalendarObj = new GoogleReminderModel
            //{
            //    Title = "Title",
            //    DateTime = DateTime.Now.AddDays(10),
            //    IsWholeDay = true
            //};

            //navigationLevel = 4;

            while (navigationLevel > 0 && navigationLevel < 4)
            {
                if (navigationLevel == 1)
                {
                    browserType = GetBrwoser(optionHelper);
                    if (browserType == BrowserType.Undefined) { navigationLevel -= 1; }
                    else { navigationLevel += 1; }
                }
                else if (navigationLevel == 2)
                {
                    account = GetAccount(optionHelper);
                    if (account == null) { navigationLevel -= 1; }
                    else { navigationLevel += 1; }
                }
                else if (navigationLevel == 3)
                {
                    googleCalendarObj = GetEntry(optionHelper);
                    if (googleCalendarObj == null) { navigationLevel -= 1; }
                    else { navigationLevel += 1; }
                }
            }

            Console.WriteLine("Initializing web driver...");
            var factory = new WebDriverFactory();
            var webDriver = factory.GetDriver(browserType);
            if (webDriver == null)
            {
                Console.WriteLine("Web driver not found. Press ctrl + x to exit");
                Console.ReadLine();
                Environment.Exit(0);
            }

            var driverHelper = new WebDriverHelper(webDriver, Console.WriteLine);
            while (true)
            {
                var loginResult = Task.Run(async () => await driverHelper.LoginToGoogleAccount(account)).Result;
                if (loginResult == "SUCCESS") break;

                Console.WriteLine(loginResult);
                var userInput = optionHelper.AskUserInput("", "Login failed. Do you want to try again?", "1. Yes", "2. No");
                if (userInput == 1) continue;
                else
                {
                    Console.WriteLine("Press ctrl + x to exit");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }

            navigationLevel = 1;
            while (navigationLevel > 0 && navigationLevel < 4)
            {
                if (navigationLevel == 1)
                {
                    var createCalendarObjResult = Task.Run(async () => await driverHelper.CreateGoogleCalendarObject(googleCalendarObj)).Result;
                    if (createCalendarObjResult == "SUCCESS") { navigationLevel += 1; continue; }

                    Console.WriteLine(createCalendarObjResult);
                    var userInput = optionHelper.AskUserInput("", "Calendar object createion failed. Do you want to try again?", "1. Yes", "2. No");
                    if (userInput == 1) continue;
                    else { navigationLevel -= 1; }
                }
                else if (navigationLevel == 2)
                {
                    var userInput = optionHelper.AskUserInput("", "Do you want to add new calendar object?", "1. Yes", "2. No");
                    if (userInput == 1)
                    {
                        navigationLevel += 1;
                    }
                    else if (userInput == 2)
                    {
                        navigationLevel = 0;
                    }
                }
                else if (navigationLevel == 3)
                {
                    googleCalendarObj = GetEntry(optionHelper);
                    if (googleCalendarObj == null) { navigationLevel = 0; }
                    else { navigationLevel = 1; }
                }
            }

            Console.WriteLine("All operation completed successfully. Press ctrl + x to exit");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private static BrowserType GetBrwoser(OptionHelper optionHelper)
        {
            var userInput = 0;
            while (userInput == 0)
            {
                //userInput = optionHelper.AskUserInput("Welcome to Google calendar driver.",
                //    "Choose the browser that you want to use:",
                //      "1. Google Chrome", "2. MS Edge (Chromium) (not tested)", "3. FireFox (not tested)", "4. Exit");

                userInput = optionHelper.AskUserInput("Welcome to Google calendar driver.",
                  "Choose the browser that you want to use:",
                   "1. Google Chrome", "2. Exit");
            }

            if (userInput == 4) return BrowserType.Undefined;

            return (BrowserType)userInput;
        }

        private static UserAccountModel GetAccount(OptionHelper optionHelper)
        {
            var userInput = 0;
            while (userInput == 0)
            {
                userInput = optionHelper.AskUserInput("",
                    "Choose account to use",
                     "1. Default (user: jhay.webdriver@gmail.com pass: P@55w0rd123)", "2. Custom", "3. Exit");
            }

            if (userInput == 3) return null;

            AccountRepository accountRepo = new AccountRepository(Console.ReadLine, Console.WriteLine);
            if (userInput == 1) // deafult
            {
                return accountRepo.GetDefault();
            }
            else
            {
                return accountRepo.GetAccountFromUser();
            }
        }

        private static object GetEntry(OptionHelper optionHelper)
        {
            var userInput = 0;
            var googleCalendarRepo = new GoogleCalendarRepository(Console.ReadLine, Console.WriteLine);
            var calendarObjs = googleCalendarRepo.GetCalendarObjects();
            var option = (from calendarObj in calendarObjs
                          select calendarObj).ToList();
            option.Add($"{calendarObjs.Count + 1}. Exit");
            while (userInput == 0)
            {
                userInput = optionHelper.AskUserInput("", "Choose item to add:", option.ToArray());

                if (userInput < 4)
                {
                    return googleCalendarRepo.CreateCalendarObject(userInput);
                }
            }
            return null;
        }
    }
}
