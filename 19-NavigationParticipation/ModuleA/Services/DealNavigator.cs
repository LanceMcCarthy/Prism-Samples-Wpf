using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ModuleA.Services
{
    public class DealNavigator
    {
        public ObservableCollection<string> MessageTexts { get; set; } = new ObservableCollection<string>();

        public Task StartFillAsyncTask()
        {
            Debug.WriteLine($"Start Fill Async Task running on Thread: {Thread.CurrentThread.ManagedThreadId}");

            // simulating long running work on this thread
            Thread.Sleep(1500);

            for (int i = 0; i < 500; i++)
            {
                this.MessageTexts.Add($"Line:{i}");
            }

            return Task.CompletedTask;
        }
    }
}