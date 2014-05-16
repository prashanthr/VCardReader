using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCardReader
{
    class Enums
    {
        public sealed class VCardVersion
        {
            public const string TwoPointOne = "2.1";
            public const string ThreePointZero = "3.0";
            public const string FourPointZero = "4.0";
        }

        public sealed class VCardElements
        {
            public const string ADR = "ADR;";
            public const string AGENT = "AGENT";
            public const string ANNIVERSARY = "ANNIVERSARY";
            public const string BDAY = "BDAY";
            public const string BEGIN = "BEGIN:VCARD";            
            public const string C = "C:";
            public const string CALADRURI = "CALADRURI";
            public const string CALURI = "CALURI";
            public const string CATEGORIES = "CATEGORIES";
            public const string CLASS = "CLASS";
            public const string CLIENTPIDMAP = "CLIENTPIDMAP";
            public const string EMAIL = "EMAIL;";
            public const string END = "END:VCARD";
            public const string FBURL = "FBURL";
            public const string FN = "FN:";            
            public const string GENDER = "GENDER";
            public const string GEO = "GEO";
            public const string IMPP = "IMPP";
            public const string KEY = "KEY";
            public const string KIND = "KIND";
            public const string LABEL = "LABEL;";
            public const string LANG = "LANG";
            public const string N = "N:";
            public const string ORG = "ORG:";
            public const string PHOTO = "PHOTO;";
            public const string REV = "REV:";
            public const string TEL = "TEL;";
            public const string TITLE = "TITLE";
            public const string VERSION = "VERSION";
        }

        public sealed class VCardMetaData
        {
            public const string KeyValueSeperator = ":";
            public const string ValueJoinDelimiter = ";";

        }

        public sealed class File
        {
            public const string OutputName = "output";
            public const string CsvFormat = "csv";
            public const string CsvDelimiter = ",";
        }
    }
}
