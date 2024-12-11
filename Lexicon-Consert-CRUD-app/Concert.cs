using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Concert_CRUD_app
{
    public class Concert
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string Performer { get; set; }
        public string Date { get; set; }

        public Concert(int iD, string location, int capacity, string performer, string date)
        {
            ID = iD;
            Location = location;
            Capacity = capacity;
            Performer = performer;
            Date = date;
        }

        public string PrintOut()
        {
            string stringOut = "";

            stringOut += "ID: " + ID + "\n";
            stringOut += "Location: " + Location + "\n";
            stringOut += "Capacity: " + Capacity + "\n";
            stringOut += "Performer: " + Performer + "\n";
            stringOut += "Date: " + Date + "\n";

            return stringOut;
        }
    }
}