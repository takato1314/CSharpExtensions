using System;
using System.Xml.Linq;

namespace Reevo.Unbroken.Extensions
{
    public static class XmlExtensions
    {
        public static string ToStringWithDeclaration(this XDocument doc)
        {
            return doc.Declaration + doc.ToString();
        }

        /// <summary>
        /// Attempts to parse string to XML
        /// </summary>
        /// <param name="doc">The string xml to be parsed</param>
        /// <param name="xml">The variable to store the output XDocument</param>
        /// <returns>returns true and its XDocument object if parsing is successful; else returns false and null</returns>
        public static bool TryParseXml(string doc, out XDocument xml)
        {
            var result = false;

            try
            {               
                xml = XDocument.Parse(doc);
                result = true;
            }
            catch (Exception)
            {
                xml = null;
            }

            return result;
        }
    }
}
