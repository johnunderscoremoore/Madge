using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madge.Sensory_Input
{
    class Event_Memory
    {
        public static Dictionary<string, string> eventMemory = new Dictionary<string, string>() { };

        /// <summary>
        /// Add event to memory feed
        /// </summary>
        /// <param name="eventName">Name of event that has occurred</param>
        /// <param name="eventValue">Value that needs to be stored for later retrieval</param>
        public static void addEventToMemory(string eventName, string eventValue)
        {
            //exit if key already found
            if (eventMemory.ContainsKey(eventName)) { return; }

            eventMemory.Add(eventName, eventValue);
        }
        /// <summary>
        /// Reset memory, preferrable after each process loop whe a fresh set of events needs to be monitored
        /// </summary>
        public static void resetMemory()
        {
            eventMemory.Clear();
        }
        public static bool eventSafetyFire(string eventName, string eventDependency)
        {

            //return true if empty string as this means there are no dependencies
            if (eventName == "") { return true; };

            //Check if event has already fired. If so, return false to signify not safe to fire
            if (eventMemory.ContainsKey(eventName) == true) { return false; }
            else {
                //If no dependency, return true for safe to fire
                if (eventDependency == "") { return true; }
                //Check if dependency and if it has already fired. 
                if (eventMemory.ContainsKey(eventDependency) && eventDependency != "") { return true; }
                //If these tests do not pass, return false to signify not safe to fire
                else { return false; }

            };
        }
    }
}
