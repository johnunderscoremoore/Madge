using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Automation;
using Madge.Automation_Library;

namespace Madge.Sensory_Input
{
    class Monitor
    {
        private static AutomationElement applicationElement;
        //private static AutomationElement fieldElement;
        string seti = Properties.Settings.Default.WinSCP_HostName_FieldID.ToString();
        public object monitorFields() {
            //Thi array should be a data table
            //unique key, window name (regex), elementID, text value to match, action to take if matched, class, function, unique key that must have fired prior (if applicable)
            string[,] fieldValueActions = { { "00000", "Login - WinSCP", Properties.Settings.Default.WinSCP_HostName_FieldID.ToString(), "test", "Madge.Automation_Library.WinSCP_Automation", "GetHostName", "" }, 
                { "00001", "Login - WinSCP", Properties.Settings.Default.WinSCP_HostName_FieldID.ToString(), "(alpha)", "Madge.Automation_Library.WinSCP_Automation", "GetHostName", "" }, 
                { "00002", "Login - WinSCP", Properties.Settings.Default.WinSCP_HostName_FieldID.ToString(), "(username)", "Madge.Automation_Library.WinSCP_Automation", "SetUsername", "" },
                { "00003", "Login - WinSCP", Properties.Settings.Default.WinSCP_HostName_FieldID.ToString(), "(add note)", "Madge.Automation_Library.WinSCP_Automation", "addNote", "" }
            };

            //Instantiate custom UIAutomation functions
            var uiAutomator = new UIAutomation.UIAutomation_Functions();

            //infinite loop
            bool x = true;
            do
            {
                //for each node, check fields
                for (int i = 0; i < fieldValueActions.GetLength(0); i += 1)
                {


                    //Get Automation Element for Advanced Settings button
                    AutomationElement fieldElement = uiAutomator.GetElementbyID(fieldValueActions[i, 1], fieldValueActions[i, 2]);


                    if (fieldElement == null)
                    {
                        //control not found, exit for
                        break;

                        throw new InvalidOperationException("Could not find result box");
                    }

                    //string fieldVal = fieldElement.Current.Name;
                    Match match = Regex.Match(fieldElement.Current.Name, @fieldValueActions[i, 3], RegexOptions.IgnoreCase);

                    // Here we check the Match instance.
                    if (match.Success)
                    {
                        //if there is an event dependency for this match, check if it has not fired. Otherwise, invoke and log.
                        //Event_Memory.eventSafetyFire will automatically return false if dependency is blank
                        if (Event_Memory.eventSafetyFire(fieldValueActions[i, 0], fieldValueActions[i, 6]) == true) {
                            //invoke automation call if there is a match on the field value
                            Invoke(fieldValueActions[i, 4], fieldValueActions[i, 5]);
                            Console.WriteLine("executing " + fieldValueActions[i, 4] + "." + fieldValueActions[i, 5]);
                            Event_Memory.addEventToMemory(fieldValueActions[i, 0], "");
                        }


                    }

                    //Console.WriteLine(fieldElement.Current.Name);

                }
            } while (x = true);


            return null;
        }
        /// <summary>
        /// Invoke a method from dynamic text. This can be used if Class and Method are stored as plain text and need to be called dynamically.
        /// </summary>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        public void Invoke (string className, string methodName)
        {

            //wrap parameters in object array
            object[] invokeParams = new object[] { "" };

            // Get a type from the string 
            Type type = Type.GetType(className);
            // Create an instance of that type
            Object obj = Activator.CreateInstance(type);
            // Retrieve the method you are looking for
            MethodInfo methodInfo = type.GetMethod(methodName);
            // Invoke the method on the instance we created above

            methodInfo.Invoke(obj, null);
        }
    }
}
