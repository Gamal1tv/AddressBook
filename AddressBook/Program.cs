using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        internal class Contact //created class "Contact"
        {
            //create string variables
            internal string FirstName;
            internal string LastName;
            internal string Phone;
            internal string Email;
            internal string Business;
            internal string Notes;
        }
           

        internal static void ShowSplashScreen()
        {
            Application.Run(new frmSplash()); //Runs Splash Screen 
        }
    }
}
