using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCalendarWebDriver
{
    public class OptionHelper
    {
        private readonly Func<string> _inputProvider;
        private readonly Action<string> _outputProvider;

        public OptionHelper(Func<string> inputProvider, Action<string> outputProvider)
        {
            _inputProvider = inputProvider;
            _outputProvider = outputProvider;
        }

        public int AskUserInput(string title, string question, params string[] options)
        {
            _outputProvider("================================================");
            _outputProvider(title);
            _outputProvider(question);
            foreach (var opt in options)
            {
                _outputProvider(opt);
            }

            Console.Write("Input:");

            var input = _inputProvider();
            var result = 0;

            // is not a number
            if (int.TryParse(input, out result) == false)
            {
                _outputProvider("Invalid input.");
                return 0;
            }

            if (result > 0 && result <= options.Length)
            {
                return result;
            }
            else
            {
                _outputProvider("Invalid input.");
                return 0;
            }
        }
    }
}
