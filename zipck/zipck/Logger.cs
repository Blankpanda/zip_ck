using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zipck
{
    class Logger
    {

        private static string Filename = "log.text";

        public static void Write(string information)
        {
            // todo: order this in a list ?
            // todo: if the log file for this session doesn't exist create it using the DateTime
            Update(information);


        }
        
        private  static void Update(string information)
        {
            // todo: we want to create one per session
            WriteToTextFile(information);
        }

        private static void WriteToTextFile(string information)
        {
            using (System.IO.StreamWriter s = new System.IO.StreamWriter(Filename))
            {
                s.WriteLine(information);
            }
        }

    }
}
