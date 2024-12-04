using System.Xml;

namespace Lexicon_Consert_CRUD_app
{
    class Program()
    {
        static string programPath = "nullPath";

        static ConsertBuilder consertBuilder;
        static Menu menu;

         static void Main()
        {
            #region Load XML and start ConsertBuilder Class
            try
            {
                programPath = Environment.ProcessPath;
                if (programPath == null)
                {
                    Console.WriteLine("Path is invalid.");
                }
                else
                {
                    #region Path Trim
                    //Trim away exefile from path and add XML folder
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
                    programPath += "XML\\";
                    #endregion

                    //Debug cheat:
                    programPath = "C:\\Users\\zeddf\\source\\repos\\Lexicon-Consert-CRUD-app\\Lexicon-Consert-CRUD-app\\";

                    XmlDocument consertsXML = new XmlDocument();

                    consertsXML.Load(programPath + "Conserts.xml");

                    consertBuilder = new ConsertBuilder(consertsXML);
                }
            }
            catch
            {
                Console.WriteLine("Console application needs to be in the same folder as xml files.");
            }
            #endregion

            menu = new Menu(consertBuilder);
            menu.Run();
        }
    }
}