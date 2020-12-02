using System;
using System.Windows.Forms;

namespace APIConsumer
{
    static class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try { Application.Run(new APIConsumerForm()); }
            catch (Exception e) { Logger.Error(e, "Some Exception"); }
            NLog.LogManager.Shutdown();
        }
    }
}
