using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace GeoMemories
{
    class lifelog
    {

        private XNamespace SOAP = "http://www.w3.org/2001/12/soap-envelope";
        private XNamespace lle = "http://www.xyz.org/lifelogevents";


        private Dictionary<string, Dictionary<string, string>> events = new Dictionary<string, Dictionary<string, string>>();

        public lifelog() { }

        public Dictionary<string, Dictionary<string, string>> Event
        {
            get { return events; }      // Accessor for events
            set { events = value; }     // Mutator for events
        }

        public Dictionary<string, string> loadTweets(XElement el, ref string eventID)
        {
            Dictionary<string, string> eventInfo = new Dictionary<string, string>();

            eventID = el.Element(lle + "eventid").Value;

            var eventinfo =
            el.Descendants(lle + "tweet") //Select all "test" elements
            .SelectMany(x => x.Elements()) //Select all children of all "test" elements
            .Select(info => new { Tag = info.Name.LocalName, Att = info.Value })
            .ToList();

            Console.WriteLine(eventID);

            eventInfo.Add("type", "tweet");

            foreach (var obj in eventinfo)
            {
                Console.WriteLine(obj);
                eventInfo.Add(obj.Tag, obj.Att);
            }

            return eventInfo;
        }

        public Dictionary<string, string> loadFacebookStatusUpdate(XElement el, ref string eventID)
        {
            Dictionary<string, string> eventInfo = new Dictionary<string, string>();

            eventID = el.Element(lle + "eventid").Value;

            var eventinfo =
            el.Descendants(lle + "facebook-status-update") //Select all "test" elements
            .SelectMany(x => x.Elements()) //Select all children of all "test" elements
            .Select(info => new { Tag = info.Name.LocalName, Att = info.Value })
            .ToList();

            Console.WriteLine(eventID);

            eventInfo.Add("type", "facebook-status-update");

            foreach (var obj in eventinfo)
            {
                Console.WriteLine(obj);
                eventInfo.Add(obj.Tag, obj.Att);
            }

            return eventInfo;
        }

        public Dictionary<string, string> loadPhoto(XElement el, ref string eventID)
        {
            Dictionary<string, string> eventInfo = new Dictionary<string, string>();

            eventID = el.Element(lle + "eventid").Value;

            var eventinfo =
            el.Descendants(lle + "photo") //Select all "test" elements
            .SelectMany(x => x.Elements()) //Select all children of all "test" elements
            .Select(info => new { Tag = info.Name.LocalName, Att = info.Value })
            .ToList();

            Console.WriteLine(eventID);

            eventInfo.Add("type", "photo");

            foreach (var obj in eventinfo)
            {
                Console.WriteLine(obj);
                eventInfo.Add(obj.Tag, obj.Att);
            }

            return eventInfo;
        }

        public Dictionary<string, string> loadVideo(XElement el, ref string eventID)
        {
            Dictionary<string, string> eventInfo = new Dictionary<string, string>();

            eventID = el.Element(lle + "eventid").Value;

            var eventinfo =
            el.Descendants(lle + "video") //Select all "test" elements
            .SelectMany(x => x.Elements()) //Select all children of all "test" elements
            .Select(info => new { Tag = info.Name.LocalName, Att = info.Value })
            .ToList();

            Console.WriteLine(eventID);

            eventInfo.Add("type", "video");

            foreach (var obj in eventinfo)
            {
                Console.WriteLine(obj);
                eventInfo.Add(obj.Tag, obj.Att);
            }

            return eventInfo;
        }

        public Dictionary<string, string> loadTracklog(XElement el, ref string eventID)
        {
            Dictionary<string, string> eventInfo = new Dictionary<string, string>();

            eventID = el.Element(lle + "eventid").Value;

            var eventinfo =
            el.Descendants(lle + "tracklog") //Select all "test" elements
            .SelectMany(x => x.Elements()) //Select all children of all "test" elements
            .Select(info => new { Tag = info.Name.LocalName, Att = info.Value })
            .ToList();

            Console.WriteLine(eventID);

            eventInfo.Add("type", "tracklog");

            foreach (var obj in eventinfo)
            {
                Console.WriteLine(obj);
                eventInfo.Add(obj.Tag, obj.Att);
            }

            return eventInfo;
        }



        public void loadLogs()
        {
            var filename = "../../lifelog-events.xml";
            XDocument lifelogEvents = XDocument.Load(filename);

            IEnumerable<XElement> eventinfo =
               from el in lifelogEvents.Descendants(SOAP + "Body").Descendants(lle + "Event")
               select el;

            foreach (XElement el in eventinfo)
            {
                string eventID = "";
                Dictionary<string, string> eventInfo = new Dictionary<string, string>();

                if (el.Descendants(lle + "tweet").Any())
                    eventInfo = loadTweets(el, ref eventID);
                else if (el.Descendants(lle + "facebook-status-update").Any())
                    eventInfo = loadFacebookStatusUpdate(el, ref eventID);
                else if (el.Descendants(lle + "photo").Any())
                    eventInfo = loadPhoto(el, ref eventID);
                else if (el.Descendants(lle + "video").Any())
                    eventInfo = loadVideo(el, ref eventID);
                else if (el.Descendants(lle + "tracklog").Any())
                    eventInfo = loadTracklog(el, ref eventID);

                Event.Add(eventID, eventInfo);
            }
        }

        public void displayDic()
        {
            /*foreach (KeyValuePair<string, string> kvp in Event)
            {
                Console.WriteLine(string.Format("\nKey = {0}, Value = {1}", kvp.Key, kvp.Value));
            }*/
        }


    }
}
