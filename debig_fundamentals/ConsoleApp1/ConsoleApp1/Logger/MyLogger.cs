using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Logger
{
    internal class MyLogger
    {
        public byte Level { get; set; }
        public MyLogger()
        {
#if DEBUG
            Level = 0;
#else
            Level = 1;
#endif
        }

        public void Log(string message, byte level = 0)
        {
            if (level >= Level)
                Console.WriteLine(message);
        }
    }
}
