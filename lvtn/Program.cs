﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace lvtn
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        public static string txtSoSanh;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
