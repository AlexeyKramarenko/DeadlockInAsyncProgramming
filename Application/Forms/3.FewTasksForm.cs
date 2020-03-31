using Library;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application
{
    public partial class FewTasksForm : Form
    {

        private readonly Service _service;
        private readonly Task _task2;
        private readonly Task _task3;

        public FewTasksForm()
        {
            InitializeComponent();

            _service = new Service();
            _task2 = Task.Delay(5000);
            _task3 = Task.Delay(3000);
        }


        /*
            Trace:
                On UI thread: BEGINNING of EventHandler
                On UI thread: Beginning of DangerousMethodAsync
        */
        private void BLOCKING_UI_CALL_WITH_DEADLOCK(object sender, EventArgs e)
        {
            Logger.Write("BEGINNING of EventHandler"); // on UI thread

            Task.WaitAll(_service.DangerousMethodAsync(), _task2, _task3);

            Logger.Write("END of EventHandler"); // on UI thread
        }


        /*
            Trace:
                On UI thread: BEGINNING of EventHandler
                On UI thread: Beginning of SafeMethodAsync
                     On ThreadPool thread: Called Task.Delay (SafeMethodAsync)
                     On ThreadPool thread: Called Thread.Sleep (SafeMethodAsync)
                     On ThreadPool thread: End of SafeMethodAsync
                On UI thread: END of EventHandler
        */
        private void BLOCKING_UI_CALL(object sender, EventArgs e)
        {
            Logger.Write("BEGINNING of EventHandler"); // on UI thread

            Task.WaitAll(_service.SafeMethodAsync(), _task2, _task3);

            Logger.Write("END of EventHandler"); // on UI thread
        }


        /*
            Trace:
                On UI thread: BEGINNING of EventHandler
                On UI thread: Beginning of DangerousMethodAsync
                    On ThreadPool thread: Called Task.Delay (DangerousMethodAsync)
                On UI thread: Called Thread.Sleep (DangerousMethodAsync)
                On UI thread: End of DangerousMethodAsync
                On UI thread: END of EventHandler
        */
        private void NON_BLOCKING_UI_CALL(object sender, EventArgs e)
        {
            Logger.Write("BEGINNING of EventHandler"); // on UI thread

            Task.WhenAll(_service.DangerousMethodAsync(), _task2, _task3)
                .ContinueWith(_ =>
                {
                    label1.Text = "Done";

                }, TaskScheduler.FromCurrentSynchronizationContext());

            Logger.Write("END of EventHandler"); // on UI thread
        }


        /*
            Trace:
                On UI thread: BEGINNING of EventHandler
                On UI thread: Beginning of DangerousMethodAsync
                On UI thread: END of EventHandler
                    On ThreadPool thread: Called Task.Delay (DangerousMethodAsync)
                On UI thread: Called Thread.Sleep (DangerousMethodAsync)
                On UI thread: End of DangerousMethodAsync
        */
        private void CONTINUE_WITH(object sender, EventArgs e)
        {
            Logger.Write("BEGINNING of EventHandler"); // on UI thread

            Task.WhenAll(_service.DangerousMethodAsync(), _task2, _task3) // on ThreadPool threads
                .GetAwaiter()
                .OnCompleted(() =>
                {
                    label1.Text = "Done"; // on UI thread

                    Logger.Write("END of EventHandler"); // on UI thread
                });
        }


        /*
            Trace:
                On UI thread: BEGINNING of EventHandler
                On UI thread: Beginning of DangerousMethodAsync
                    On ThreadPool thread: Called Task.Delay (DangerousMethodAsync)
                On UI thread: Called Thread.Sleep (DangerousMethodAsync)
                On UI thread: End of DangerousMethodAsync
                On UI thread: END of EventHandler
        */
        private async void GET_AWAITER(object sender, EventArgs e)
        {
            Logger.Write("BEGINNING of EventHandler"); // on UI thread

            await Task.WhenAll(_service.DangerousMethodAsync(), _task2, _task3);

            Logger.Write("END of EventHandler"); // on UI thread
        }

    }
}
