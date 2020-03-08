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

        public void WaitForExit()
        {
            _process.WaitForExit();
        }
        
        public Task WaitForExitAsync()
        {
            if (_waitingTask == null)
            {
                _waitingTask = Task.Factory.StartNew(() =>
                {
                    _process.WaitForExit();
                });
            }
            
            return _waitingTask;
        }
    }
}