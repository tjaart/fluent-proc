using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Tests
{
    public class RunningProcess
    {
        private readonly Process _process;
        private Task? _waitingTask;

        public RunningProcess(Process process)
        {
            _process = process;
        }

        public void WaitForExit(TimeSpan? timeOut = null)
        {
            if (timeOut == null)
            {
                timeOut = TimeSpan.FromSeconds(10);
            }

            Task.Delay(timeOut.Value).ContinueWith(task => { _process.Kill(); });

            _process.WaitForExit();
        }


        public Task WaitForExitAsync(TimeSpan? timeOut = null)
        {
            if (_waitingTask == null)
            {
                _waitingTask = Task.Factory.StartNew(() => { WaitForExit(timeOut); });
            }

            return _waitingTask;
        }
    }
}