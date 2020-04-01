using Library;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application
{
    public partial class MethodsWithResultForm : Form
    {

        private readonly Service _service;
        public MethodsWithResultForm()
        {
            InitializeComponent();

            _service = new Service();
        }


        /*
             Trace:
                On UI thread: BEGINNING of EventHandler
                On UI thread: Beginning of GetText 
        */
        private void BLOCKING_UI_CALL_WITH_DEADLOCK(object sender, EventArgs e)
        {
            Logger.Write("BEGINNING of EventHandler"); // on UI thread

            var result = _service
                             .DangerousMethodAsync("https://www.google.com") // on UI and ThreadPool threads
                             .Result; // BLOCK UI thread till the end of getting result from library method

            label1.Text = result; // on UI thread

            Logger.Write("END of EventHandler");
        }


        /*
            Trace:
                On UI thread: BEGINNING of EventHandler
                On UI thread: Beginning of GetText
                     On ThreadPool thread: Called GetAsync()
                     On ThreadPool thread: Called Thread.Sleep
                     On ThreadPool thread: Called ReadAsStringAsync()
                     On ThreadPool thread: Called Thread.Sleep again
                On UI thread: END of EventHandler
        */
        private void BLOCKING_UI_CALL(object sender, EventArgs e)
        {
            Logger.Write("BEGINNING of EventHandler"); // on UI thread

            var result = _service
                             .SafeMethodAsync("https://www.google.com") // on UI and ThreadPool threads
                             .Result; // BLOCK UI thread till the end of getting result from library method

            label1.Text = result; // on UI thread

            Logger.Write("END of EventHandler"); // on UI thread
        }


        /*
         Trace:
            On UI thread: BEGINNING of EventHandler
            On UI thread: Beginning of GetText
                 On ThreadPool thread: Called GetAsync()
                 On ThreadPool thread: Called Thread.Sleep
                 On ThreadPool thread: Called ReadAsStringAsync()
                 On ThreadPool thread: Called Thread.Sleep again
            On UI thread: END of EventHandler
        */
        private void GET_AWAITER(object sender, EventArgs e)
        {
            Logger.Write("BEGINNING of EventHandler"); // on UI thread

            var awaiter = _service
                              .SafeMethodAsync("https://www.google.com") // on UI and ThreadPool threads
                              .GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                label1.Text = awaiter.GetResult(); // on UI thread

                Logger.Write("END of EventHandler"); // on UI thread
            });
        }


        /*
         Trace:
            On UI thread: BEGINNING of EventHandler
            On UI thread: Beginning of GetText
                 On ThreadPool thread: Called GetAsync()
                 On ThreadPool thread: Called Thread.Sleep
                 On ThreadPool thread: Called ReadAsStringAsync()
                 On ThreadPool thread: Called Thread.Sleep again
            On UI thread: End of EventHandlerMethod
             */
        private void CONTINUE_WITH(object sender, EventArgs e)
        {
            Logger.Write("Beginning of EventHandlerMethod");   // on UI thread 

            _service.SafeMethodAsync("https://www.google.com") // on UI and on ThreadPool threads
                    .ContinueWith((action) =>
                    {
                        label1.Text = action.Result; // on UI thread

                        Logger.Write("End of EventHandlerMethod");

                    }, TaskScheduler.FromCurrentSynchronizationContext());
        }


        /*
         Trace:
            On UI thread: BEGINNING of EventHandler
            On UI thread: Beginning of GetText
                 On ThreadPool thread: Called GetAsync()
                 On ThreadPool thread: Called Thread.Sleep
                 On ThreadPool thread: Called ReadAsStringAsync()
                 On ThreadPool thread: Called Thread.Sleep again
            On UI thread: End of EventHandlerMethod
        */
        private async void CONFIGURE_AWAIT(object sender, EventArgs e)
        {
            Logger.Write("Beginning of EventHandlerMethod");   // on UI thread 

            var result = await _service
                                   .SafeMethodAsync("https://www.google.com") // on UI and on ThreadPool threads
                                   .ConfigureAwait(true); // Continue next part of click method on UI thread 

            label1.Text = result; // on UI thread

            Logger.Write("End of EventHandlerMethod");
        }

    }
}
