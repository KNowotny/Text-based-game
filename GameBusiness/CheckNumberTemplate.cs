using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBusiness
{
    public class CheckNumberTemplate
    {
        public static int CheckIntNumber(string number, int min, int max)
        {
            var parsePossible = int.TryParse(number, out int result);
            while (!parsePossible || result > max || result < min)
            {
                Console.Write("Zła liczba. Wprowadź poprawną: ");
                number = Console.ReadLine();
                parsePossible = int.TryParse(number, out result);
            }
            return result;
        }

        public static double CheckDoubleNumber(string number, double min, double max)
        {
            var parsePossible = double.TryParse(number, out double result);
            while (!parsePossible || result > max || result < min)
            {
                Console.Write("Zła liczba. Wprowadź poprawną: ");
                number = Console.ReadLine();
                parsePossible = double.TryParse(number, out result);
            }
            return result;
        }
    }
}
