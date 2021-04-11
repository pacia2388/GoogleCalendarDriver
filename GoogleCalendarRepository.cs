using GoogleCalendarWebDriver.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCalendarWebDriver
{
    public class GoogleCalendarRepository
    {
        private readonly Func<string> _inputProvider;
        private readonly Action<string> _outputProvider;

        private readonly Dictionary<int, Tuple<string, Func<object>>> _calendarObject;

        public GoogleCalendarRepository(Func<string> inputProvider, Action<string> outputProvider)
        {
            _inputProvider = inputProvider;
            _outputProvider = outputProvider;

            _calendarObject = new Dictionary<int, Tuple<string, Func<object>>>
            {
                { 1, new Tuple<string,Func<object>>("Event", CreateCalendarEvent) },
                { 2, new Tuple<string, Func<object>>("Task", CreateCalendarTask) },
                { 3, new Tuple<string, Func<object>>("Reminder", CreateCalendarReminder) }
            };
        }

        public List<string> GetCalendarObjects()
        {
            return _calendarObject.Select(t => t.Key + ". " + t.Value.Item1).ToList();
        }

        public object CreateCalendarObject(int calendarObject)
        {
            return _calendarObject[calendarObject].Item2();
        }


        private GoogleEventModel CreateCalendarEvent()
        {
            GoogleEventModel googleEvent = new GoogleEventModel();

            googleEvent.Title = GetInput<string>("title");
            if (googleEvent.Title == null) return null;

            googleEvent.Description = GetInput<string>("description");
            if (googleEvent.Description == null) return null;

            googleEvent.EventStart = GetInput<DateTime?>("event start [yyyy-MM-dd HH:mm:ss] [e.g. 2021-01-01 13:00:00]");
            if (googleEvent.EventStart == null) return null;

            var isWholeDay = GetInput<bool?>("Is Whole Day? [yes or no]");
            if (isWholeDay == null) return null;

            if (isWholeDay.Value == true)
            {
                googleEvent.EventEnd = googleEvent.EventStart;
                googleEvent.IsWholeDay = true;
            }
            else
            {
                googleEvent.EventEnd = GetInput<DateTime?>("event end [yyyy-MM-dd HH:mm:ss] [e.g. 2021-01-01 13:00:00]");
                if (googleEvent.EventEnd == null) return null;
            }
            while (googleEvent.EventEnd < googleEvent.EventStart)
            {
                _outputProvider("Event end must be greater than or equal to event start.");
                googleEvent.EventEnd = GetInput<DateTime?>("event end [yyyy-MM-dd HH:mm:ss] [e.g. 2021-01-01 13:00:00]");
                if (googleEvent.EventEnd == null) return null;
            }

            googleEvent.Location = GetInput<string>("location");
            if (googleEvent.Location == null) return null;

            return googleEvent;
        }

        private GoogleTaskModel CreateCalendarTask()
        {
            GoogleTaskModel googleTask = new GoogleTaskModel();

            googleTask.Title = GetInput<string>("title");
            if (googleTask.Title == null) return null;

            googleTask.Description = GetInput<string>("description");
            if (googleTask.Description == null) return null;

            googleTask.DateTime = GetInput<DateTime?>("date [yyyy-MM-dd HH:mm:ss] [e.g. 2021-01-01 13:00:00]");
            if (googleTask.DateTime == null) return null;

            var isWholeDay = GetInput<bool?>("Is Whole Day? [yes or no]");
            if (isWholeDay == null) return null;

            googleTask.IsWholeDay = isWholeDay.Value;

            return googleTask;
        }

        private GoogleReminderModel CreateCalendarReminder()
        {
            GoogleReminderModel googleReminder = new GoogleReminderModel();

            googleReminder.Title = GetInput<string>("title");
            if (googleReminder.Title == null) return null;

            googleReminder.DateTime = GetInput<DateTime?>("date [yyyy-MM-dd HH:mm:ss] [e.g. 2021-01-01 13:00:00]");
            if (googleReminder.DateTime == null) return null;

            var isWholeDay = GetInput<bool?>("Is Whole Day? [yes or no]");
            if (isWholeDay == null) return null;

            googleReminder.IsWholeDay = isWholeDay.Value;

            return googleReminder;
        }

        private T GetInput<T>(string name)
        {
            object obj = null;
            while (obj == null)
            {
                _outputProvider($"Enter {name} (X to go back):");
                obj = _inputProvider();

                if ((string)obj == "X")
                {
                    obj = null;
                    break;
                }
                if (IsValidInput<T>(ref obj, out string msg) == false)
                {
                    _outputProvider(msg);
                }
                else
                {
                    break;
                }
            }
            return (T)obj;
        }

        private bool IsValidInput<T>(ref object obj, out string message)
        {
            message = "";
            try
            {
                var type = typeof(T);
                if (type == typeof(DateTime) || type == typeof(DateTime?))
                {
                    DateTime dt = DateTime.ParseExact((string)obj, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    obj = dt;
                }
                else if (type == typeof(bool?))
                {
                    if (obj.ToString().ToLower() == "yes")
                    {
                        obj = true;
                    }
                    else if (obj.ToString().ToLower() == "no")
                    {
                        obj = false;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    var input = (T)obj;
                }
                return true;
            }
            catch
            {
                obj = null;
                message = "Invalid input";
                return false;
            }
        }
    }
}
