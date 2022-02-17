using System.Collections.Generic;

namespace ReleaseXmlToHtmlConverter
{
    internal class ProgramArguments
    {
        readonly IReadOnlyList<string> _inputArguments;
        string? _path;
        IEnumerable<string>? _customerIds;

        internal ProgramArguments(IReadOnlyList<string> args)
        {
            _inputArguments = args;
        }

        internal string? Path => _path ??= GetPathArgument();
        internal IEnumerable<string>? CustomerIds => _customerIds ??= GetCustomerIdsArgument();

        internal bool AreValid { get; private set; }
        internal string? ErrorMessage { get; private set; }

        internal void ValidateAndSetParameters()
        {
            if (_inputArguments.Count == 4 &&
                !(_inputArguments[0] == Constants.CustomerIdsConsoleArgumentKey
                  || _inputArguments[2] == Constants.CustomerIdsConsoleArgumentKey))
            {
                ErrorMessage = "Customer IDs argument has not correct format.";
                AreValid = false;
                return;
            }

            if (string.IsNullOrEmpty(Path))
            {
                ErrorMessage = "Path argument has not correct format.";
                AreValid = false;
            }
            else
                AreValid = true;
        }

        IEnumerable<string>? GetCustomerIdsArgument()
        {
            if (_inputArguments.Count != 4)
                return null;

            string customerIdsArgumentValue;

            if (_inputArguments[0] == Constants.CustomerIdsConsoleArgumentKey)
                customerIdsArgumentValue = _inputArguments[1];
            else if (_inputArguments[2] == Constants.CustomerIdsConsoleArgumentKey)
                customerIdsArgumentValue = _inputArguments[3];
            else
                return null;

            return customerIdsArgumentValue.Split(',');
        }

        string? GetPathArgument()
        {
            return _inputArguments.Count switch
            {
                2 when _inputArguments[0] == Constants.PathConsoleArgumentKey => _inputArguments[1],
                4 when _inputArguments[0] == Constants.PathConsoleArgumentKey => _inputArguments[1],
                4 when _inputArguments[2] == Constants.PathConsoleArgumentKey => _inputArguments[3],
                _ => null
            };
        }
    }
}