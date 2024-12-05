using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lexicon_Concert_CRUD_app
{
    internal class ConcertBuilder
    {
        XmlDocument ConcertsXML { get; set; }

        public List<Concert> Concerts { get; private set; }

        public ConcertBuilder(XmlDocument concertsXML)
        {
            ConcertsXML = concertsXML;
            Concerts = new List<Concert>();

            ReadFromXML();

            int lowestID = 0;
            for (int i = 0; i < Concerts.Count; i++)
            {
                if (Concerts[i].ID > lowestID)
                {
                    lowestID = Concerts[i].ID;
                }
            }
            UniqueID.SetLowestID(lowestID);
        }

        void ReadFromXML()
        {
            XmlNodeList concertsNodeList = ConcertsXML.DocumentElement.SelectNodes("/Concerts/Concert");

            foreach (XmlNode xmlNode in concertsNodeList)
            {
                int id = int.Parse(xmlNode.SelectSingleNode("Id").InnerText);
                string location = xmlNode.SelectSingleNode("Location").InnerText;
                int capacity = int.Parse(xmlNode.SelectSingleNode("Capacity").InnerText);
                string performer = xmlNode.SelectSingleNode("Performer").InnerText;
                string date = xmlNode.SelectSingleNode("Date").InnerText;

                Concert newConcert = new Concert(id, location, capacity, performer, date);
                Concerts.Add(newConcert);
            }
        }

        public XmlDocument WriteToXML()
        {
            ConcertsXML = new XmlDocument();

            XmlElement concertsElement = ConcertsXML.DocumentElement;

            concertsElement = ConcertsXML.CreateElement("Concerts");

            foreach (Concert concert in Concerts)
            {
                XmlElement concertElement = ConcertsXML.CreateElement("Concert");

                XmlElement idElement = ConcertsXML.CreateElement("Id");
                idElement.InnerText = concert.ID.ToString();
                concertElement.AppendChild(idElement);

                XmlElement locationElement = ConcertsXML.CreateElement("Location");
                locationElement.InnerText = concert.Location;
                concertElement.AppendChild(locationElement);

                XmlElement capacityElement = ConcertsXML.CreateElement("Capacity");
                capacityElement.InnerText = concert.Capacity.ToString();
                concertElement.AppendChild(capacityElement);

                XmlElement performerElement = ConcertsXML.CreateElement("Performer");
                performerElement.InnerText = concert.Performer.ToString();
                concertElement.AppendChild(performerElement);

                XmlElement dateElement = ConcertsXML.CreateElement("Date");
                dateElement.InnerText = concert.Date;
                concertElement.AppendChild(dateElement);

                concertsElement.AppendChild(concertElement);
            }

            ConcertsXML.AppendChild(concertsElement);

            return ConcertsXML;
        }

        public void NewConcert(string location, int capacity, string performer, string date)
        {
            int Id = UniqueID.GetNewID();

            Concerts.Add(new Concert(Id, location, capacity, performer, date));   
        }

        public void RemoveConcert(int id)
        {
            int position = id;
            for (int i = 0; i < Concerts.Count; i++)
            {
                if (Concerts[i].ID == id)
                {
                    position = i; break;
                }
            }

            Concerts.RemoveAt(position);
        }
    }
}