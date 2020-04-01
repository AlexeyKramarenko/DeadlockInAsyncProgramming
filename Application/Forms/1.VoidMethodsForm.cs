using Library;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application
{
    public partial class VoidMethodsForm : Form
    {

        private readonly Service _service;
        public VoidMethodsForm()
        {
            InitializeComponent();

            _service = new Service();
        }


        /*
           Trace:
                On UI thread: Beginning of EventHandlerMethod
                On UI thread: Beginning of DangerousMethodAsync

              ---------DEADLOCK---------
        */
        private void BLOCKING_OF_UI_THREAD_WITH_DEADLOCK_DUE_TO_ConfigureAwaitTrue_IN_THE_LIBRARY_METHOD(object sender, EventArgs e)
        {
            Logger.Write("Beginning of EventHandlerMethod");   // on UI thread 

            _service
                .DangerousMethodAsync() // on UI and on ThreadPool threads
                .Wait(); // BLOCK UI thread till the end of execution of library method

            Logger.Write("End of EventHandlerMethod");  // on UI thread
        }


        /*
           Trace:
                On UI thread: Beginning of EventHandlerMethod
                On UI thread: Beginning of SafeMethodAsync
                     On ThreadPool thread: Called Task.Delay (SafeMethodAsync)
                     On ThreadPool thread: Called Thread.Sleep (SafeMethodAsync)
                     On ThreadPool thread: End of SafeMethodAsync
                On UI thread: End of EventHandlerMethod
        */
        private void BLOCKING_OF_UI_THREAD_BUT_WITHOUT_DEADLOCK_BECAUSE_OF_ConfigureAwaitFalse_IN_THE_LIBRARY_METHOD(object sender, EventArgs e)
        {
            Logger.Write("Beginning of EventHandlerMethod");   // on UI thread 

            _service
                .SafeMethodAsync() // on UI and on ThreadPool threads
                .Wait(); // BLOCK UI thread till the end of execution of library method 

            Logger.Write("End of EventHandlerMethod");  // on UI thread
        }


        /*  Trace:
                On UI thread: Beginning of EventHandlerMethod
                On UI thread: Beginning of SafeMethodAsync
                On UI thread: End of EventHandlerMethod
                     On ThreadPool thread: Called Task.Delay (SafeMethodAsync)
                     On ThreadPool thread: Called Thread.Sleep (SafeMethodAsync)
                     On ThreadPool thread: End of SafeMethodAsync
        */
        private void EXECUTION_IN_PARALLEL_DUE_TO_NOT_AWAITED_CALL(object sender, EventArgs e)
        {
            Logger.Write("Beginning of EventHandlerMethod");   // on UI thread 

            //Since SafeMethodAsync is not awaited execution of the current event handler continues before the call of SafeMethodAsync is completed.
            _service.SafeMethodAsync(); // on UI and on ThreadPool threads

            Logger.Write("End of EventHandlerMethod");  // on UI thread
        }


        /*
           Trace:
                On UI thread: Beginning of EventHandlerMethod
                On UI thread: Beginning of SafeMethodAsync
                     On ThreadPool thread: Called Task.Delay (SafeMethodAsync)
                     On ThreadPool thread: Called Thread.Sleep (SafeMethodAsync)
                     On ThreadPool thread: End of SafeMethodAsync
                On UI thread: End of EventHandlerMethod
        */
        private void ASYNCHRONOUS_EXECUTION_USING_GetAwaiterMethod(object sender, EventArgs e)
        {
            Logger.Write("Beginning of EventHandlerMethod");   // on UI thread 

            _service
                .SafeMethodAsync() // on UI and on ThreadPool threads
                .GetAwaiter()
                .OnCompleted(() =>
                {
                    Logger.Write("End of EventHandlerMethod");  // on UI thread           
                });
        }


        /*
        Trace:
            On UI thread: Beginning of EventHandlerMethod
            On UI thread: Beginning of SafeMethodAsync
                 On ThreadPool thread: Called Task.Delay (SafeMethodAsync)
                 On ThreadPool thread: Called Thread.Sleep (SafeMethodAsync)
                 On ThreadPool thread: End of SafeMethodAsync
                 On ThreadPool thread: End of EventHandlerMethod
       */
        private void ASYNCHRONOUS_EXECUTION_USING_ContinueWithMethod(object sender, EventArgs e)
        {
            Logger.Write("Beginning of EventHandlerMethod");   // on UI thread 

            _service
                .SafeMethodAsync() // on UI and on ThreadPool threads
                .ContinueWith(_ => { Logger.Write("End of EventHandlerMethod"); }, TaskScheduler.Default); // on ThreadPool thread owing to TaskScheduler.Default
        }


        /*
            Trace:
                On UI thread: Beginning of EventHandlerMethod
                On UI thread: Beginning of SafeMethodAsync
                     On ThreadPool thread: Called Task.Delay (SafeMethodAsync)
                     On ThreadPool thread: Called Thread.Sleep (SafeMethodAsync)
                     On ThreadPool thread: End of SafeMethodAsync
                On UI thread: End of EventHandlerMethod
        */
        private async void ASYNCHRONOUS_EXECUTION_USING_AWAIT_KEYWORD(object sender, EventArgs e)
        {
            Logger.Write("Beginning of EventHandlerMethod");   // on UI thread 

            await _service
                      .SafeMethodAsync() // on UI and on ThreadPool threads
                      .ConfigureAwait(true); // Continue next part of click method on UI thread

            Logger.Write("End of EventHandlerMethod");  // on UI thread
        }


        /*
            Trace:
                On UI thread: Beginning of EventHandlerMethod
                On UI thread: Beginning of SafeMethodAsync
                     On ThreadPool thread: Called Task.Delay (SafeMethodAsync)
                     On ThreadPool thread: Called Thread.Sleep (SafeMethodAsync)
                     On ThreadPool thread: End of SafeMethodAsync
                     On ThreadPool thread: End of EventHandlerMethod
        */
        private async void ASYNCHRONOUS_EXECUTION_USING_AWAIT_KEYWORD_WITHOUT_SWITCHING_OF_SynchronisationContext_AFTER_EXECUTION_OF_LIBRARY_METHOD(object sender, EventArgs e)
        {
            Logger.Write("Beginning of EventHandlerMethod");   // on UI thread 

            await _service
                      .SafeMethodAsync() // on UI and on ThreadPool threads
                      .ConfigureAwait(false); // Without switching of context from ThreadPool thread to UI thread. Continue next part of button1_Click method on LAST used thread in library method. In current case this is the ThreadPool thread. 

            Logger.Write("End of EventHandlerMethod");  // on ThreadPool thread
        }

    }
}
