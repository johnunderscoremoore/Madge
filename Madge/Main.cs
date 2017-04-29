using Madge.Automation_Library;
using Madge.Sensory_Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madge
{
    class Madge_Main
    {
        private static Monitor monitor;
        private static string userCommand;
        private static WinSCP_Automation winscpAuto;

        static void Main(string[] args)
        {
            //begin monitor execution
            monitor = new Monitor();

            monitor.monitorFields();

            Console.WriteLine("Finished");
            userCommand = Console.ReadLine();
        }
    }
}
