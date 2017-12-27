using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeDrawingLibrary
{
    public class KeyGenerator
    {
        private List<string> keys;

        /// <summary>
        /// Initializes the list of strings.
        /// </summary>
        public KeyGenerator()
        {
            keys = new List<string>();
        }

        /// <summary>
        /// Generates 100 Guids, deletes symbols that accur too much.
        /// </summary>
        /// <returns>the generated keys</returns>
        public List<string> GenerateKeys()
        {
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    Guid g = Guid.NewGuid();
                    string GuidString = Convert.ToBase64String(g.ToByteArray());
                    // replaces common symbols that appear midst or at the end of the GuidString.
                    // Purely for visual reasons, for more cleaner looking serial keys.
                    GuidString = GuidString.Replace("=", "");
                    GuidString = GuidString.Replace("+", "");
                    GuidString = GuidString.Replace("/", "");

                    keys.Add(GuidString);

                }
            }
            catch (Exception e)
            {

                Debug.Write(e);
            }
            return keys;
        }
    }
}
