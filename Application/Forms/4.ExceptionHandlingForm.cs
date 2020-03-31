using Library;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application
{
    public partial class ExceptionHandlingForm : Form
    {

        private readonly Service _service;
        public ExceptionHandlingForm()
        {
            InitializeComponent();

            _service = new Service();
        }


        private void continueWith_Click(object sender, EventArgs e)
        {
            Task<string> t1 = Task.Run(() => _service.LongRunningOperation("ContinueWith", 3000));

            t1.ContinueWith((t2) =>
            {
                if (t2.IsCompleted && !t2.IsFaulted && !t2.IsCanceled)
                {
                    label1.Text = t2.Result;
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }


        private async void asyncAwait_Click(object sender, EventArgs e)
        {
            try
            {
                string result = await Task.Run(() => _service.LongRunningOperation("AsyncOperation", 3000));

                label1.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
