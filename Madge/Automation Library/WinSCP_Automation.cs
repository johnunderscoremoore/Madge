using Madge.UIAutomation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Input;
using static Madge.UIAutomation.UIAutomation_Functions;

namespace Madge.Automation_Library
{
    class WinSCP_Automation
    {
        private AutomationElement _WinSCPAutomationElement;
        private Process _WinSCPProcess;
        private AutomationElement _resultTextBoxAutomationElement;
        private UIAutomation_Functions customUIAutoFunctions;

        public object GetHostName() 
        {
            //Uncomment if you need to start a new process
            //_WinSCPProcess = Process.Start("WinSCP.exe");

            int ct = 0;
            do
            {
            _WinSCPAutomationElement = AutomationElement.RootElement.FindFirst
            (TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty,
            "Login - WinSCP"));

                ++ct;
                Thread.Sleep(100);
            }
            while (_WinSCPAutomationElement == null && ct < 50);


            if (_WinSCPAutomationElement == null)
            {
                throw new InvalidOperationException("WinSCP must be running");
            }

            AutomationElement _resultTextBoxAutomationElement = _WinSCPAutomationElement.FindFirst(TreeScope.Descendants, new PropertyCondition
            (AutomationElement.AutomationIdProperty, Properties.Settings.Default.WinSCP_HostName_FieldID.ToString()));

            if (_resultTextBoxAutomationElement == null)
            {
                throw new InvalidOperationException("Could not find result box");
            }

            string fieldVal = _resultTextBoxAutomationElement.Current.Name;

            Console.WriteLine(fieldVal);

            return fieldVal;
        }
        /// <summary>
        /// Insert string into username field 
        /// </summary>
        /// <param name="username">user id</param>
        /// <returns></returns>
        public string SetUsername()
        {
            //Instantiate custom UIAutomation functions
            var uiAutomator = new UIAutomation.UIAutomation_Functions();

            //Get Automation Element for Username field
            AutomationElement usernameField = uiAutomator.GetElementbyID("Login - WinSCP", Properties.Settings.Default.WinSCP_Username_FieldID.ToString());

            //Insert username into field
            uiAutomator.InsertTextUsingUIAutomation(usernameField, "userID");
            
            return "Username field set to userID.";

        }
        public string addNote()
        {
            //Instantiate custom UIAutomation functions
            var uiAutomator = new UIAutomation.UIAutomation_Functions();

            //Get Automation Element for Username field
            AutomationElement advancedButton = uiAutomator.GetElementbyID("Login - WinSCP", Properties.Settings.Default.WinSCP_AdvancedButton_FieldID.ToString());

            System.Windows.Point p = advancedButton.GetClickablePoint();

            SetCursorPosition(p.X, p.Y);
            MouseEvent(MouseEventFlags.LeftDown);
            MouseEvent(MouseEventFlags.LeftUp);
            return "Clickety clack";
        }
        public AutomationElement GetFunctionButton(string functionName)
        {
            AutomationElement functionButton = _WinSCPAutomationElement.FindFirst
            (TreeScope.Descendants, new PropertyCondition
            (AutomationElement.NameProperty, functionName));

            if (functionButton == null)
            {
                throw new InvalidOperationException("No function button found with name: " +
                functionName);
            }

            return functionButton;
        }
        public class Functions
        {
            // Functions 
            public const string MemoryClear = "Memory clear";
            public const string Backspace = "Backspace";
            public const string MemoryRecall = "Memory recall";
            public const string ClearEntry = "Clear entry";
            public const string MemoryStore = "Memory store";
            public const string Clear = "Clear";
            public const string DecimalSeparator = "Decimal separator";
            public const string MemoryAdd = "Memory add";
            public const string MemoryRemove = "Memory subtract";
            public const string Equals = "Equals";
        }
    }


}
