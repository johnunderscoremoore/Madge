using Madge.Sensory_Input;
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

            //Instantiate custom UIAutomation functions
            var uiAutomator = new UIAutomation.UIAutomation_Functions();

            AutomationElement _resultTextBoxAutomationElement = uiAutomator.GetElementbyID("Login - WinSCP",Properties.Settings.Default.WinSCP_HostName_FieldID.ToString());

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
            //Ask user if they want to add a note, politely!
            Console.WriteLine("I think you might be trying to add a note. Would you like assistance? (y/n)");
            string respondIndicator = Console.ReadLine();

            var languageIntepreter = new Language_Interpreter();

            //check if response was affirmative. If not, exit (and eventually log the event to a database)
            if (languageIntepreter.CheckForAffirmative(respondIndicator) == false)
            {
                return "Looks like I was wrong. Sorry.";
            }
            
            //Prompt user for what they want to put into the notes, in a semi-uniform fashion.
            Console.WriteLine("What's the title of your note?");
            Console.WriteLine("");
            string noteText = "Title: " + Console.ReadLine();
            Console.WriteLine("Describe the note.");
            Console.WriteLine("");
            noteText += System.Environment.NewLine + "Description: " + Console.ReadLine();

            //Instantiate custom UIAutomation functions
            var uiAutomator = new UIAutomation.UIAutomation_Functions();

            //Get Automation Element for Advanced Settings button
            AutomationElement advancedButton = uiAutomator.GetElementbyID("Login - WinSCP", Properties.Settings.Default.WinSCP_AdvancedButton_FieldID.ToString());

            System.Windows.Point p = advancedButton.GetClickablePoint();
            //Click the button where it lays!
            SetCursorPosition(p.X, p.Y);
            MouseEvent(MouseEventFlags.LeftDown);
            MouseEvent(MouseEventFlags.LeftUp);
            
            //Move to Notes section and paste. This could be done using UIAutomation, but I'm not there yet...
            uiAutomator.sendKeysToApp("N");
            uiAutomator.sendKeysToApp("{TAB}");
            uiAutomator.sendKeysToApp(noteText);

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
