using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Bug
{
    internal class RandomException
    {
        private readonly Random rand;

        private double _probability = 0;

        public byte Probability 
        { 
            set { _probability = value / 100.0; } 
            get { return (byte)(_probability * 100.0); } 
        }  
        public RandomException(byte probability)
        {
            rand = new Random();
            Probability = probability; 
        }

        public void RandomlyThrowException()
        {
            double number = rand.NextDouble();

            if (number < _probability)
            {
                throw new Exception("Random Exception");
            }
        }
    }
}
