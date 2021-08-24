using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace GeoMemories
{
    class lifelog
    {
    
        private Dictionary<int, Dictionary<string, string>> events = new Dictionary<int, Dictionary<string, string>>();
        private Dictionary<string, string> eventsInfo = new Dictionary<string, string>();

        public lifelog() { }

        public Dictionary<int, Dictionary<string,string>> Event
        {
            get { return events; }      // Accessor for events
            set { events = value; }     // Mutator for events
        }

        public Dictionary<string, string> EventInfo
        {
            get { return eventsInfo; }
            set { eventsInfo = value; }
        }

        public void loadLogs()
        {
            var filename = "lifelog-events.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var lifelogsFilepath = Path.Combine(currentDirectory, filename);

            XElement lifelogEvents = XElement.Load(lifelogsFilepath);

            var eventInfo = lifelogEvents.Elements().ToDictionary(x => x.Name.LocalName, x => x.Value);

            EventInfo = eventInfo;
        }

        public void displayDic()
        {
            Console.WriteLine(EventInfo);
        }


    }
}
