using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Consert_CRUD_app
{
    internal class Menu
    {
        ConsertBuilder builder;

        public Menu(ConsertBuilder consertBuilder)
        {
            builder = consertBuilder;
        }

        public void Run()
        {
            Console.CursorVisible = false;
            bool running = true;

            while (running)
            {
                Console.WriteLine("Consert Builder running. Press the key corresponding to what you want to do.");
                Console.WriteLine("[ 1 ]: View Concerts");
                Console.WriteLine("[ 2 ]: Add Consert");
                Console.WriteLine("[ 3 ]: Change Consert");
                Console.WriteLine("[ 4 ]: Remove Consert");
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
                    case ConsoleKey.Escape:

                        running = false;

                        break;
                }
            }
        }

        void View()
        {
            Console.Clear();

            for (int i = 0; i < builder.Conserts.Count; i++)
            {
                Console.WriteLine(builder.Conserts[i].PrintOut());
            }
        }
        
        void Add()
        {
            Console.Clear();
            // Read input for all fields then create new Consert object and then use ConsertBuilder.NewConcert
        }

        void Change()
        {
            // ask which field to change: Location, Performer, etc. then ConsertBuilder.Change
        }

        void Remove()
        {
            // ConsertBuilder.RemoveConsert
        }
    }
}
