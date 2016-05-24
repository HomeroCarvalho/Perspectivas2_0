using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace rotacao3DparaFiguras2D
{
    static class Program
    {
        /// <summary>
        /// The main entry PointF for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new rotacao3DparaFiguras2D.windowPerspectivas());
        } // Main()
    } // class Program
} // namespace
