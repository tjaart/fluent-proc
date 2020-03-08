using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests
{
    public class ProcessRunner
    {
        public ProcessRunner(string processPath)
        {
            ProcessPath = processPath;
        }
        
        public Action<int>? ExitHandler { get; set; }
        public Action<string>? DataHandler { get; set; }
        public string ProcessPath { get; }
        public List<string> Args { get; set; } = new List<string>();

        public RunningProcess Run()
        {
            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false, 
                    FileName = ProcessPath
                }, EnableRaisingEvents = true
            };

            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += (sender, args) => { DataHandler(args.Data); };

            process.StartInfo.Arguments = string.Join(' ', Args);
            if (ExitHandler != null)
            {
                process.Exited += (sender, args) => { ExitHandler(process.ExitCode); };
            }
            
            process.Start();
            process.BeginOutputReadLine();
            return new RunningProcess(process);
        }
    }
}