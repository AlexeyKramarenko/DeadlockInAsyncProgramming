using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Library
{
    public class Service
    {

        private const int Interval = 2000;
        private static HttpClient _client = new HttpClient();

        public async Task<string> DangerousMethodAsync(string url)
        {
            Logger.Write("Beginning of GetText"); // on UI thread

            var request = await _client
                                    .GetAsync(url) // on ThreadPool thread
                                    .ConfigureAwait(true); // Continue rest part of the DangerousMethodAsync method on UI thread. If UI thread has been already BLOCKED than we have deadlock.

            Logger.Write("Called GetAsync()"); // on UI thread

            Thread.Sleep(Interval); // on UI thread

            Logger.Write("Called Thread.Sleep"); // on UI thread

            var text = await request
                                .Content
                                .ReadAsStringAsync() // on ThreadPool thread
                                .ConfigureAwait(true);  // on UI thread

            Logger.Write("Called ReadAsStringAsync()"); // on UI thread

            return text;
        }


        public async Task<string> SafeMethodAsync(string url)
        {
            Logger.Write("Beginning of GetText"); // on UI thread

            var request = await _client
                                  .GetAsync(url) // on ThreadPool thread
                                  .ConfigureAwait(false); // Continue rest part of the SafeMethodAsync method on the current ThreadPool thread

            Logger.Write("Called GetAsync()"); // on ThreadPool thread

            Thread.Sleep(Interval); // on ThreadPool thread

            Logger.Write("Called Thread.Sleep"); // on ThreadPool thread

            var text = await request
                                .Content
                                .ReadAsStringAsync() // on ThreadPool thread
                                .ConfigureAwait(true); // doesn't make sense

            Logger.Write("Called ReadAsStringAsync()"); // on ThreadPool thread

            Thread.Sleep(Interval); // on ThreadPool thread

            Logger.Write("Called Thread.Sleep again"); // on ThreadPool thread

            return text;
        }


        // Use of ConfigureAwait(true) can be dangerous if UI developer use some blocking operations 
        // such as call of .Wait(), .Result, .WaitAll() on async library methods because it leads to deadlock.        
        public async Task DangerousMethodAsync()
        {
            Logger.Write("Beginning of DangerousMethodAsync");  // on UI thread 

            await Task.Delay(Interval) // on ThreadPool tread
                      .ConfigureAwait(continueOnCapturedContext: true); // continue rest part of the DangerousMethodAsync method on UI thread. If UI thread has been already BLOCKED than we have deadlock.

            Logger.WriteIndicatingThatThreadBelongsToThreadPool("Called Task.Delay (DangerousMethodAsync)");

            Thread.Sleep(Interval); // on UI thread

            Logger.Write("Called Thread.Sleep (DangerousMethodAsync)");  // on UI thread

            Logger.Write("End of DangerousMethodAsync");  // on UI thread
        }


        // Use of ConfigureAwait(false) will not lead to deadlock even if UI developer calls blocking operations.
        public async Task SafeMethodAsync()
        {
            Logger.Write("Beginning of SafeMethodAsync");  // on UI thread 

            await Task.Delay(Interval) // on ThreadPool tread
                      .ConfigureAwait(continueOnCapturedContext: false); // continue rest part of the SafeMethodAsync method on the current ThreadPool thread 

            Logger.Write("Called Task.Delay (SafeMethodAsync)"); // on ThreadPool thread

            Thread.Sleep(Interval); // on ThreadPool thread

            Logger.Write("Called Thread.Sleep (SafeMethodAsync)");  // on ThreadPool thread

            Logger.Write("End of SafeMethodAsync");  // on ThreadPool thread
        }


        public string LongRunningOperation(string s, int sec)
        {
            Thread.Sleep(sec);
            return s + " Completed";
        }

    }
}
