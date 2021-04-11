using GoogleCalendarWebDriver.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleCalendarWebDriver
{
    class WebDriverHelper
    {
        private string _googleCalendar_Uri = "https://calendar.google.com/";

        private readonly IWebDriver _driver;
        private Action<string> _outputProvider;
        public WebDriverHelper(IWebDriver driver, Action<string> outputProvider)
        {
            _driver = driver;
            _outputProvider = outputProvider;
        }

        public Task<string> LoginToGoogleAccount(UserAccountModel account)
        {
            try
            {
                _driver.Url = _googleCalendar_Uri;

                WaitAndSendKeys(By.XPath(account.GetEmailBoxFullXPath()), account.Email);
                WaitAndClick(By.XPath(account.GetSubmitButtonFullXPath()));
                _outputProvider("Sumit email address");

                _outputProvider("Checking the presence of email validation message");
                var emailValidationMsg = GetElement(By.XPath(account.GetEmailValidationMessageFullXPath()), 3);
                if (emailValidationMsg != null)
                {
                    return Task.FromResult("Login failed. " + emailValidationMsg.Text);
                }

                _outputProvider("Checking the presence of captcha");
                var catchaBoxElement = GetElement(By.XPath(account.GetCatchaBoxFullXPath()), 3);
                if (catchaBoxElement != null)
                {
                    return Task.FromResult("Login failed captcha is required. Try again later.");
                }

                _outputProvider("Checking the presence of verification list");
                var verificationList = GetElement(By.XPath(account.GetVerificationListFullXPath()), 3);
                if (verificationList != null)
                {
                    return Task.FromResult("Login failed, verification is required. Try again later.");
                }

                WaitAndSendKeys(By.XPath(account.GetPasswordBoxFullXPath()), account.Password);
                WaitAndClick(By.XPath(account.GetSubmitButtonFullXPath()));
                _outputProvider("Submit password");

                _outputProvider("Checking the presence of password validation message");
                var passwordValidationMsg = GetElement(By.XPath(account.GetPasswordValidationMessageFullXPath()), 5);
                if (passwordValidationMsg != null)
                {
                    return Task.FromResult("Login failed. " + passwordValidationMsg.Text);
                }

                _outputProvider("Checking the presence of verification list");
                verificationList = GetElement(By.XPath(account.GetVerificationListFullXPath()), 3);
                if (verificationList != null)
                {
                    return Task.FromResult("Login failed, verification is required. Try again later.");
                }

                return Task.FromResult("SUCCESS");
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        public Task<string> CreateGoogleCalendarObject(object obj)
        {
            _driver.Url = _googleCalendar_Uri;

            if (obj is GoogleEventModel) return CreateNewCalendarEventEntry(obj as GoogleEventModel);
            if (obj is GoogleTaskModel) return CreateNewCalendarTaskEntry(obj as GoogleTaskModel);
            if (obj is GoogleReminderModel) return CreateNewCalendarReminderEntry(obj as GoogleReminderModel);

            return Task.FromResult($"Google obbject {obj.GetType().Name} is not yet implemented.");
        }
        private Task<string> CreateNewCalendarEventEntry(GoogleEventModel _event)
        {
            try
            {
                // click create button
                _outputProvider("Click create");
                WaitAndClick(By.XPath(_event.GetCreateButtonFullXPath()));

                // send keys to title box
                _outputProvider("Input title");
                WaitAndSendKeys(By.XPath(_event.GetTitleFullXPath()), _event.Title);

                // click date link
                _outputProvider("Click date link");
                WaitAndClick(By.XPath(_event.GetDateLinkFullXPath()));

                // click outside of entry form to close any popout
                WaitAndClick(By.XPath(_event.GetVacantFullXPath()));

                if (_event.IsWholeDay)
                {
                    // tick is whole day if true
                    _outputProvider("Click is whole day");
                    WaitAndClick(By.XPath(_event.GetIsWholeDayFullXPath()));

                    // send keys to start date box
                    _outputProvider("Input start date");
                    WaitAndSendKeys(By.XPath(_event.GetStartDateFullXPath()), _event.EventStart.Value.ToString("yyyy-MM-dd"), true);

                    // send keys to end date box
                    _outputProvider("Input end date");
                    WaitAndSendKeys(By.XPath(_event.GetEndDateFullXPath()), _event.EventEnd.Value.ToString("yyyy-MM-dd"), true);
                }
                else
                {
                    // send keys to start date box
                    _outputProvider("Input start date");
                    WaitAndSendKeys(By.XPath(_event.GetStartDateFullXPath()), _event.EventStart.Value.ToString("yyyy-MM-dd"), true);

                    // send keys to start time box
                    _outputProvider("Input start time");
                    WaitAndSendKeys(By.XPath(_event.GetStartTimeFullXPath()), _event.EventStart.Value.ToString("HH:mm:ss"), true);

                    // send keys to end time box
                    _outputProvider("Input end time");
                    WaitAndSendKeys(By.XPath(_event.GetEndTimeFullXPath()), _event.EventEnd.Value.ToString("HH:mm:ss"), true);
                }

                // click location link
                _outputProvider("Click location link");
                WaitAndClick(By.XPath(_event.GetLocationLinkFullXPath()));

                // send keys to location box
                _outputProvider("Input location");
                WaitAndSendKeys(By.XPath(_event.GetLocationBoxFullXPath()), _event.Location);

                // click description link   
                _outputProvider("Click description link");
                WaitAndClick(By.XPath(_event.GetDescriptionLinkFullXPath()));


                // send keys to location box
                _outputProvider("Input location");
                WaitAndSendKeys(By.XPath(_event.GetDescriptionBoxFullXPath()), _event.Description);

                _outputProvider("Submit form");
                WaitAndClick(By.XPath(_event.GetSaveButtonFullXPath()));
                return Task.FromResult("SUCCESS");
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }
        private Task<string> CreateNewCalendarTaskEntry(GoogleTaskModel task)
        {
            try
            {
                // click create button
                _outputProvider("Click create");
                WaitAndClick(By.XPath(task.GetCreateButtonFullXPath()));

                // click task link
                _outputProvider("Click task link");
                WaitAndClick(By.XPath(task.GetTaskLinkFullXPath()));

                // send keys to title box
                _outputProvider("Input title");
                WaitAndSendKeys(By.XPath(task.GetTitleFullXPath()), task.Title);

                if (task.IsWholeDay)
                {
                    // click is whole day
                    _outputProvider("Click is whole day");
                    WaitAndClick(By.XPath(task.GetIsWholeDayFullXPath()));

                    // send keys to date box
                    _outputProvider("Input date");
                    WaitAndSendKeys(By.XPath(task.GetDateBoxFullXPath()), task.DateTime.Value.ToString("yyyy-MM-dd"), true);
                }
                else
                {
                    // send keys to date box
                    _outputProvider("Input date");
                    WaitAndSendKeys(By.XPath(task.GetDateBoxFullXPath()), task.DateTime.Value.ToString("yyyy-MM-dd"), true);

                    // send keys to time box
                    _outputProvider("Input time");
                    WaitAndSendKeys(By.XPath(task.GetTimeBoxFullXPath()), task.DateTime.Value.ToString("HH:mm:ss"), true);
                }

                // send keys to description box
                _outputProvider("Input description");
                WaitAndSendKeys(By.XPath(task.GetDescriptionBoxFUllXPath()), task.Description);

                // click submit
                _outputProvider("Submit form");
                WaitAndClick(By.XPath(task.GetSaveButtonFullXPath()));
                return Task.FromResult("SUCCESS");
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        private Task<string> CreateNewCalendarReminderEntry(GoogleReminderModel reminder)
        {
            try
            {
                // click create button
                _outputProvider("Click create");
                WaitAndClick(By.XPath(reminder.GetCreateButtonFullXPath()));

                // click reminder link
                _outputProvider("Click reminder link");
                WaitAndClick(By.XPath(reminder.GetReminderLinkFullXPath()));

                // send keys to title box
                _outputProvider("Input title");
                WaitAndSendKeys(By.XPath(reminder.GetTitleBoxFullXPath()), reminder.Title);

                if (reminder.IsWholeDay)
                {
                    // click is whole day
                    _outputProvider("Click is whole day");
                    WaitAndClick(By.XPath(reminder.GetIsWholeDayFullXPath()));

                    // send keys to date box
                    _outputProvider("Input date");
                    WaitAndSendKeys(By.XPath(reminder.GetDateBoxFullXPath()), reminder.DateTime.Value.ToString("yyyy-MM-dd"), true);
                }
                else
                {
                    // send keys to date box
                    _outputProvider("Input date");
                    WaitAndSendKeys(By.XPath(reminder.GetDateBoxFullXPath()), reminder.DateTime.Value.ToString("yyyy-MM-dd"), true);

                    // send keys to time box
                    _outputProvider("Input time");
                    WaitAndSendKeys(By.XPath(reminder.GetTimeBoxFullXPath()), reminder.DateTime.Value.ToString("HH:mm:ss"), true);
                }

                // click submit
                _outputProvider("Submit form");
                WaitAndClick(By.XPath(reminder.GetSaveButtonFullXPath()));
                return Task.FromResult("SUCCESS");
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        private void WaitAndSendKeys(By by, string keys, bool clear = false)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

            wait.IgnoreExceptionTypes(typeof(TargetInvocationException), typeof(NoSuchElementException), typeof(InvalidOperationException));
            wait.Until(drv =>
            {
                try
                {
                    var elem = drv.FindElement(by);
                    Thread.Sleep(500);
                    if (clear)
                    {
                        elem.SendKeys(Keys.Control + "a" + Keys.Backspace);
                        Thread.Sleep(500);
                    }
                    elem.SendKeys(keys);
                }
                catch (Exception ex)
                {
                    Type exType = ex.GetType();
                    if (exType == typeof(TargetInvocationException) ||
                        exType == typeof(NoSuchElementException) ||
                        exType == typeof(InvalidOperationException) ||
                        exType == typeof(StaleElementReferenceException) ||
                        exType == typeof(ElementNotInteractableException))
                    {
                        return false; //By returning false, wait will still rerun the func.
                    }
                    else
                    {
                        throw; //Rethrow exception if it's not ignore type.
                    }
                }
                return true;
            });
        }
        private void WaitAndClick(By by)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

            wait.IgnoreExceptionTypes(typeof(TargetInvocationException), typeof(NoSuchElementException), typeof(InvalidOperationException));
            wait.Until(drv =>
            {
                try
                {
                    var elem = drv.FindElement(by);
                    elem.Click();
                }
                catch (Exception ex)
                {
                    Type exType = ex.GetType();
                    if (exType == typeof(TargetInvocationException) ||
                        exType == typeof(NoSuchElementException) ||
                        exType == typeof(InvalidOperationException) ||
                        exType == typeof(StaleElementReferenceException) ||
                        exType == typeof(ElementNotInteractableException) ||
                        exType == typeof(ElementClickInterceptedException))
                    {
                        return false; //By returning false, wait will still rerun the func.
                    }
                    else
                    {
                        throw; //Rethrow exception if it's not ignore type.
                    }
                }
                return true;
            });
        }

        private IWebElement GetElement(By by, int timeSpanInSeconds)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeSpanInSeconds));
            IWebElement elem = null;
            wait.IgnoreExceptionTypes(typeof(TargetInvocationException), typeof(NoSuchElementException), typeof(InvalidOperationException));
            try
            {
                wait.Until(drv =>
                {
                    try
                    {
                        elem = drv.FindElement(by);
                    }
                    catch (Exception ex)
                    {
                        Type exType = ex.GetType();
                        if (exType == typeof(TargetInvocationException) ||
                            exType == typeof(NoSuchElementException) ||
                            exType == typeof(InvalidOperationException) ||
                            exType == typeof(StaleElementReferenceException) ||
                            exType == typeof(ElementNotInteractableException))
                        {
                            return false;
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return true;
                });
            }
            catch (Exception ex)
            {
                Type exType = ex.GetType();
                if (exType == typeof(WebDriverTimeoutException))
                {
                    // element does not exist
                    elem = null;
                }
            }
            return elem;
        }
    }
}
