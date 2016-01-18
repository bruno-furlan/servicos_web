using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FipeDownloader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
		/// mais um comentario
		/// comentário do ticket TI#2 - branch
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}