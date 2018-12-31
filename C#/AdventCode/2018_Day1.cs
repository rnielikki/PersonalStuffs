using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using System.Numerics;

namespace ConsoleApp4
{
    class Day1
    {
        public static void Play()
        {
            IEnumerable<string> Nums = openFile(@"..\..\input1.txt");;
            Console.WriteLine(Answer1(Nums).ToString());
            Console.WriteLine(Answer2(Nums).ToString());
            //Console.WriteLine(sum.ToString());
        }
        static IEnumerable<string> openFile(string path) => File.ReadLines(path);
        public static int Answer1(IEnumerable<string> list) {
            int sum = 0;
            foreach (string n in list)
            {
                if (Int32.TryParse(n, out int x))
                {
                    sum += x;
                }
            }
            return sum;
        }
        public static int Answer2(IEnumerable<string> list)
        {
            int sum = 0;
            HashSet<int> Sums = new HashSet<int>();
            while (true)
            {
                foreach (string n in list)
                {
                    if (Int32.TryParse(n, out int x))
                    {
                        sum += x;
                        if (Sums.Contains(sum))
                        {
                            return sum;
                        }
                        Sums.Add(sum);
                    }
                }
            }
        }
    }
}
