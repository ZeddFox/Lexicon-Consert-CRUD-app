using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Consert_CRUD_app
{
    internal static class UniqueID
    {
        static int currentID = 0;
        public static int GetNewID()
        {
            int newID = currentID++;
            currentID = newID;

            return newID;
        }

        public static void SetLowestID(int value)
        {
            currentID = value;
        }
    }
}
