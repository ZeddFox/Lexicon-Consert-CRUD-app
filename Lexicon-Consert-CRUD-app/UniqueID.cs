using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Concert_CRUD_app
{
    internal static class UniqueID
    {
        static int currentID = 0;
        public static int GetNewID()
        {
            currentID++;
            return currentID;
        }

        public static void SetLowestID(int value)
        {
            currentID = value++;
        }
    }
}
