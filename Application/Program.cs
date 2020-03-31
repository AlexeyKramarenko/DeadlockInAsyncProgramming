using System;

namespace Application
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new VoidMethodsForm());
            //System.Windows.Forms.Application.Run(new MethodsWithResultForm());
            //System.Windows.Forms.Application.Run(new FewTasksForm());
            //System.Windows.Forms.Application.Run(new ExceptionHandlingForm());
        }
    }
}
