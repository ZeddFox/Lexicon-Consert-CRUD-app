using System;
using System.Security;
using System.Xml;
using System.Xml.Linq;

namespace Lexicon_Concert_CRUD_app
{
    public class Program()
    {
        static string programPath = "nullPath";
        static string fileName = "Concerts.xml";

        static ConcertBuilder concertBuilder;
        static Menu menu;

        static void Main()
        {
            #region Load XML and start ConcertBuilder Class
                programPath = Environment.ProcessPath;

                if (programPath == null)
                {
                    Console.WriteLine("Path is invalid.");
                }
                else
                {
                    #region Path Trim
                    //Trim away exefile from path
                    char[] pathChars = programPath.ToCharArray();
                    bool isCleanPath = false;
                    int trimPos = 0;
                    int curPos = pathChars.GetLength(0);

                    while (!isCleanPath)
                    {
                        if (pathChars[(curPos - 1) - trimPos] != '\\')
                        {
                            trimPos++;
                        }
                        else
                        {
                            programPath = programPath.Remove(programPath.Length - trimPos);
                            isCleanPath = true;
                        }
                    }
                #endregion

                XmlDocument concertsXML = new XmlDocument();

                try
                {
                    concertsXML.Load(programPath + fileName);
                }
                catch
                {
                    if (File.Exists(programPath + fileName))
                    {
                        Console.Clear();
                        Console.WriteLine("File exists but cannot be read due to an unknown error.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        XmlElement concertsElement = concertsXML.DocumentElement;

                        concertsElement = concertsXML.CreateElement("Concerts");

                        XmlElement concertElement = concertsXML.CreateElement("Concert");

                        XmlElement idElement = concertsXML.CreateElement("Id");
                        idElement.InnerText = "00";
                        concertElement.AppendChild(idElement);

                        XmlElement locationElement = concertsXML.CreateElement("Location");
                        locationElement.InnerText = "none";
                        concertElement.AppendChild(locationElement);

                        XmlElement capacityElement = concertsXML.CreateElement("Capacity");
                        capacityElement.InnerText = "00";
                        concertElement.AppendChild(capacityElement);

                        XmlElement performerElement = concertsXML.CreateElement("Performer");
                        performerElement.InnerText = "none";
                        concertElement.AppendChild(performerElement);

                        XmlElement dateElement = concertsXML.CreateElement("Date");
                        dateElement.InnerText = "none";
                        concertElement.AppendChild(dateElement);

                        concertsElement.AppendChild(concertElement);

                        concertsXML.AppendChild(concertsElement);

                        concertsXML.Save(programPath + fileName);

                        concertsXML.Load(programPath + fileName);
                    }
                }

                concertBuilder = new ConcertBuilder(concertsXML);

                    menu = new Menu(concertBuilder, programPath + fileName);
                    menu.Run();
                }
            #endregion
        }
    }
}