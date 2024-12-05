using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lexicon_Concert_CRUD_app
{
    internal class Menu
    {
        ConcertBuilder builder;
        string filePath;

        public Menu(ConcertBuilder concertBuilder, string filePath)
        {
            builder = concertBuilder;
            this.filePath = filePath;
        }

        public void Run()
        {
            Console.CursorVisible = false;
            bool running = true;

            while (running)
            {
                Console.WriteLine("Concert Builder running. Press the key corresponding to what you want to do.");
                Console.WriteLine("[ 1 ]: View Concerts");
                Console.WriteLine("[ 2 ]: Add Concert");
                Console.WriteLine("[ 3 ]: Change Concert");
                Console.WriteLine("[ 4 ]: Remove Concert");
                Console.WriteLine("[ 5 ]: Save to XML file");
                Console.WriteLine("[ ESC ]: Quit");

                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:

                        View();

                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:

                        Add();

                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:

                        Change();

                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:

                        Remove();

                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:

                        WriteToFile();

                        break;

                    case ConsoleKey.Escape:

                        running = false;

                        break;
                }
            }
        }

        string GetInputString(string inputDesired)
        {
            string stringOut = "";
            bool validOutput = false;

            while (!validOutput)
            {
                string read = Console.ReadLine();

                if (read != null || read != "")
                {
                    if (inputDesired == "Text")
                    {
                        stringOut = read;
                        validOutput = true;
                    }
                    else if (inputDesired == "Number")
                    {
                        int num;
                        if (int.TryParse(read, out num))
                        {
                            stringOut = read;
                            validOutput = true;
                        }
                        else
                        {
                            Console.WriteLine("Input was not recognized as a number. Please try again.");
                        }
                    }
                }
            }

            return stringOut;
        }

        void View()
        {
            Console.Clear();

            for (int i = 0; i < builder.Concerts.Count; i++)
            {
                Console.WriteLine(builder.Concerts[i].PrintOut());
            }
        }
        
        void Add()
        {
            Console.Clear();

            string location;
            int capacity;
            string performer;
            string date;

            Console.WriteLine("Please write where concert is taking place: ");
            location = GetInputString("Text");

            Console.WriteLine("Please write how many people it can hold: ");
            capacity = Convert.ToInt32(GetInputString("Number"));

            Console.WriteLine("Please write who is performing: ");
            performer = GetInputString("Text");

            Console.WriteLine("Please write the date: ");
            date = GetInputString("Text");

            builder.NewConcert(location, capacity, performer, date);

            Console.WriteLine("Successfully added new concert.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        void Change()
        {
            View();
            // ask which field to change: Location, Performer, etc. then ConcertBuilder.Change
            Console.WriteLine("Type in the ID of the concert you want to change, then press Enter");
            int id = Convert.ToInt32(GetInputString("Number"));

            string changeField = "";

            string newLocation = "";
            int newCapacity = 0;
            string newPerformer = "";
            string newDate = "";

            Console.WriteLine("Press the key corresponding to the field you would like to change");
            Console.WriteLine("[ 1 ]: Location");
            Console.WriteLine("[ 2 ]: Capacity");
            Console.WriteLine("[ 3 ]: Performer");
            Console.WriteLine("[ 4 ]: Date");

            ConsoleKey readKey = Console.ReadKey().Key;

            switch (readKey)
            {
                // Change Location
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    {
                        changeField = "Location";
                        Console.WriteLine("\nPlease write new Location: ");
                        newLocation = GetInputString("Text");
                    }
                    break;

                // Change Capacity
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    {
                        changeField = "Capacity";
                        Console.WriteLine("\nPlease write new Capacity: ");
                        newCapacity = Convert.ToInt32(GetInputString("Number"));
                    }
                    break;

                // Change Performer
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    {
                        changeField = "Performer";
                        Console.WriteLine("\nPlease write new Perfomer: ");
                        newPerformer = GetInputString("Text");
                    }
                    break;

                // Change Date
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    {
                        changeField = "Date";
                        Console.WriteLine("\nPlease write new Date: ");
                        newDate = GetInputString("Text");
                    }
                    break;
            }

            foreach (Concert curConcert in builder.Concerts)
            {
                if (id == curConcert.ID)
                {
                    switch (changeField)
                    {
                        case "Location":
                            {
                                curConcert.Location = newLocation;
                            }
                            break;

                        case "Capacity":
                            {
                                curConcert.Capacity = newCapacity;
                            }
                            break;

                        case "Performer":
                            {
                                curConcert.Performer = newPerformer;
                            }
                            break;

                        case "Date":
                            {
                                curConcert.Date = newDate;
                            }
                            break;
                    }
                }
            }
        }

        void Remove()
        {
            bool validOutput = false;
            while (!validOutput)
            {
                View();

                Console.WriteLine("Type in the ID of the concert you want to remove, then press Enter");
                int id = Convert.ToInt32(GetInputString("Number"));

                Console.WriteLine("Please write the ID again to confirm as deletion cannot be reverted. Confirm with Enter");
                int verify = Convert.ToInt32(GetInputString("Number"));

                if (id == verify)
                {
                    builder.RemoveConcert(id);
                    validOutput = true;
                }
                else
                {
                    Console.WriteLine("Numbers didn't match. Please try again.");
                }
            }
        }

        void WriteToFile()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to write to file? Press Enter to confirm or any other key to cancel");

            ConsoleKey keyRead = Console.ReadKey().Key;

            if (keyRead == ConsoleKey.Enter)
            {
                XmlDocument xmlDocument = builder.WriteToXML();
                xmlDocument.Save(filePath);

                Console.WriteLine("File written successfully. Press any key to return to menu.");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
