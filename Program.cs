using System;
using System.Threading;
using System.Windows.Forms;


namespace Spirograph_v1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(HandleThreadException);
            Application.Run(new FormMain());

        }   // Main()


        static void HandleThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"An error occurred: {e.Exception.Message}",
                             "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);

        }   // HandleThreadException()


    }   // class Program

}   // namespace Spirograph_v1
