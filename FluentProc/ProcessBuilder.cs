using System;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    public class ProcessBuilder
    {
        private readonly string _processPath;
        private Action<int>? _exitHandler;
        private readonly List<string> _args = new List<string>();
        private Action<string>? _dataHandler;
        

        private ProcessBuilder(string processPath)
        {
            _processPath = processPath;
        }

        public static ProcessBuilder Create(string processPath)
        {
            
            return new ProcessBuilder(processPath);
        }

        public ProcessBuilder HandleExit(Action<int> exitHandler)
        {
            _exitHandler = exitHandler;
            return this;
        }

        public ProcessBuilder Arg(in string argument)
        {
            
            if (_args.Contains(argument))
            {
                return this;
            }
            
            _args.Add( new ArgumentHelper(argument).SanitizeArgument());

            return this;
        }

       

        public ProcessBuilder HandleOutput(Action<string> handleDataAction)
        {
            _dataHandler = handleDataAction;
            return this;
        }
        
        public ProcessRunner Build()
        {
            return new ProcessRunner(_processPath)
            {
                ExitHandler = _exitHandler,
                
                DataHandler = _dataHandler,
                Args = _args,
            };
        }

    }
}