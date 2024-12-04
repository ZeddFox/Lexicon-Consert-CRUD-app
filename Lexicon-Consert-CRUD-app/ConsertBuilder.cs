using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lexicon_Consert_CRUD_app
{
    internal class ConsertBuilder
    {
        XmlDocument ConsertsXML { get; set; }

        public List<Consert> Conserts { get; private set; }

        public ConsertBuilder(XmlDocument consertsXML)
        {
            ConsertsXML = consertsXML;
            Conserts = new List<Consert>();

            ReadFromXML();

            int lowestID = 0;
            for (int i = 0; i < Conserts.Count; i++)
            {
                if (Conserts[i].ID > lowestID)
                {
                    lowestID = Conserts[i].ID;
                }
            }
            UniqueID.SetLowestID(lowestID);
        }

        void ReadFromXML()
        {
            XmlNodeList concertsNodeList = ConsertsXML.DocumentElement.SelectNodes("/Conserts/Consert");

            foreach (XmlNode xmlNode in concertsNodeList)
            {
                int id = int.Parse(xmlNode.SelectSingleNode("Id").InnerText);
                string location = xmlNode.SelectSingleNode("Location").InnerText;
                int capacity = int.Parse(xmlNode.SelectSingleNode("Capacity").InnerText);
                string performer = xmlNode.SelectSingleNode("Performer").InnerText;
                string date = xmlNode.SelectSingleNode("Date").InnerText;

                Consert newConsert = new Consert(id, location, capacity, performer, date);
                Conserts.Add(newConsert);
            }
        }

        void WriteToXML()
        {

        }

        void NewConsert()
        {

        }

        void ChangeConsert()
        {

        }

        void RemoveConsert()
        {

        }
    }
}