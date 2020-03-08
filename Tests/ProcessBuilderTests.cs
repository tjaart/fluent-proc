using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ProcessBuilderTests
    {
        [Fact]
        public void TestRunTaskWithNormalExitCode()
        {
            var exitCode = -100;
            var proc = ProcessBuilder.Create("ls")
                .HandleExit(code =>
                {
                    exitCode = code;
                })
                .Build();
            var runningProcess = proc.Run();

            runningProcess.WaitForExit();
            
            Assert.Equal(0, exitCode);

        }
        [Fact]
        public void TestRunTaskWithOutput()
        {
            var outputStr = new StringBuilder();
            var proc = ProcessBuilder.Create("ls")
                .Arg("/").Arg("-l")
                .HandleExit(code =>
                {
                    var exitCode = code;
                })
                .HandleOutput(s => { outputStr.AppendLine(s); })
                .Build();
            var runningProcess = proc.Run();

            runningProcess.WaitForExit();
            var final = outputStr.ToString();
            Assert.True(final.Length > 0);

        }
        
        [Fact]
        public void TestRunTaskWithArgument()
        {
            var exitCode = -100;
            
            var proc = ProcessBuilder.Create("ls")
                .Arg("/")
                .HandleExit(code =>
                {
                    exitCode = code;
                })
                .Build();
            var runningProcess = proc.Run();

            runningProcess.WaitForExit();
            
            Assert.Equal(0, exitCode);

        }

        
        [Fact]
        public async Task TestRunTaskWithAsyncWait()
        {
            var exitCode = -100;
            var proc = ProcessBuilder.Create("ls")
                .HandleExit(code =>
                {
                    exitCode = code;
                })
                .Build();
            var runningProcess = proc.Run();

            await runningProcess.WaitForExitAsync();
            
            Assert.Equal(0, exitCode);

        }

        
        
    }
}