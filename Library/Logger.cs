using System;
using System.Threading;

namespace Library
{
    public static class Logger
    {

        public static void Write(string text)
        {
            var thread = Thread.CurrentThread.IsThreadPoolThread ? "     On ThreadPool thread" : "On UI thread";

            Console.WriteLine($"{thread}: {text}");
        }

        public static void WriteIndicatingThatThreadBelongsToThreadPool(string text)
        {
            Console.WriteLine($"On ThreadPool thread: {text}");
        }

    }
}
