using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lexicon_Concert_CRUD_app
{
    public class ConcertBuilder
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

        public static void BruteWriteToXML(string filePath, string bruteLocation, int bruteCapacity, string brutePerformer, string bruteDate)
        {
            List<Concert> bruteConcerts = new List<Concert>();
            int bruteUniqueID = 0;

            #region Program.Main() Load file or create new if not exist
            XmlDocument bruteConcertsXML = new XmlDocument();

            try
            {
                bruteConcertsXML.Load(filePath);
            }
            catch
            {
                if (File.Exists(filePath))
                {
                    Console.Clear();
                    Console.WriteLine("File exists but cannot be read due to an unknown error.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    XmlElement bruteConcertsElement = bruteConcertsXML.DocumentElement;

                    bruteConcertsElement = bruteConcertsXML.CreateElement("Concerts");

                    XmlElement concertElement = bruteConcertsXML.CreateElement("Concert");

                    XmlElement idElement = bruteConcertsXML.CreateElement("Id");
                    idElement.InnerText = "00";
                    concertElement.AppendChild(idElement);

                    XmlElement locationElement = bruteConcertsXML.CreateElement("Location");
                    locationElement.InnerText = "none";
                    concertElement.AppendChild(locationElement);

                    XmlElement capacityElement = bruteConcertsXML.CreateElement("Capacity");
                    capacityElement.InnerText = "00";
                    concertElement.AppendChild(capacityElement);

                    XmlElement performerElement = bruteConcertsXML.CreateElement("Performer");
                    performerElement.InnerText = "none";
                    concertElement.AppendChild(performerElement);

                    XmlElement dateElement = bruteConcertsXML.CreateElement("Date");
                    dateElement.InnerText = "none";
                    concertElement.AppendChild(dateElement);

                    bruteConcertsElement.AppendChild(concertElement);

                    bruteConcertsXML.AppendChild(bruteConcertsElement);

                    bruteConcertsXML.Save(filePath);

                    bruteConcertsXML.Load(filePath);
                }
            }
            #endregion

            #region ReadFromXML()
            XmlNodeList concertsNodeList = bruteConcertsXML.DocumentElement.SelectNodes("/Concerts/Concert");

            foreach (XmlNode xmlNode in concertsNodeList)
            {
                int newId = int.Parse(xmlNode.SelectSingleNode("Id").InnerText);
                string newLocation = xmlNode.SelectSingleNode("Location").InnerText;
                int newCapacity = int.Parse(xmlNode.SelectSingleNode("Capacity").InnerText);
                string newPerformer = xmlNode.SelectSingleNode("Performer").InnerText;
                string newDate = xmlNode.SelectSingleNode("Date").InnerText;

                Concert newConcert = new Concert(newId, newLocation, newCapacity, newPerformer, newDate);
                bruteConcerts.Add(newConcert);
            }
            #endregion

            #region Set Lowest UniqueID
            int lowestID = 0;
            for (int i = 0; i < bruteConcerts.Count; i++)
            {
                if (bruteConcerts[i].ID >= lowestID)
                {
                    lowestID = bruteConcerts[i].ID;
                }
            }
            bruteUniqueID = lowestID += 1;
            #endregion

            #region NewConsert()
            int newBruteId = bruteUniqueID;

            bruteConcerts.Add(new Concert(newBruteId, bruteLocation, bruteCapacity, brutePerformer, bruteDate));
            #endregion

            #region WriteToXML()
            XmlDocument outXML = new XmlDocument();

            XmlElement concertsElement = outXML.DocumentElement;

            concertsElement = outXML.CreateElement("Concerts");

            foreach (Concert concert in bruteConcerts)
            {
                XmlElement concertElement = outXML.CreateElement("Concert");

                XmlElement idElement = outXML.CreateElement("Id");
                idElement.InnerText = concert.ID.ToString();
                concertElement.AppendChild(idElement);

                XmlElement locationElement = outXML.CreateElement("Location");
                locationElement.InnerText = concert.Location;
                concertElement.AppendChild(locationElement);

                XmlElement capacityElement = outXML.CreateElement("Capacity");
                capacityElement.InnerText = concert.Capacity.ToString();
                concertElement.AppendChild(capacityElement);

                XmlElement performerElement = outXML.CreateElement("Performer");
                performerElement.InnerText = concert.Performer.ToString();
                concertElement.AppendChild(performerElement);

                XmlElement dateElement = outXML.CreateElement("Date");
                dateElement.InnerText = concert.Date;
                concertElement.AppendChild(dateElement);

                concertsElement.AppendChild(concertElement);
            }

            outXML.AppendChild(concertsElement);

            outXML.Save(filePath);
            #endregion
        }
    }
}