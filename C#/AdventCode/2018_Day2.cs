using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using System.Numerics;

namespace ConsoleApp4
{
    class Day2
    {
        public static void Play()
        {
            IEnumerable<string> Strs = openFile(@"..\..\input2.txt");
            Console.WriteLine(Answer1(Strs).ToString());
            Console.WriteLine(Answer2(Strs));        
        }
        static IEnumerable<string> openFile(string path) => File.ReadLines(path);
        public static int Answer1(IEnumerable<string> list) {
            int[] repSum = { 0, 0 };
            bool[] repeats;
            foreach (string s in list)
            {
                repeats=new bool[]{ false, false };
                foreach (char c in s) {
                    if (s.Count(chr => chr == c) == 2) {
                        repeats[0] = true;
                    }
                    else if (s.Count(chr => chr == c) == 3) {
                        repeats[1] = true;
                    }
                    if (repeats[0] == true && repeats[1] == true) break;
                }
                //add repsum if bool true
                repSum[0]+=(repeats[0] ? 1 : 0);
                repSum[1] += (repeats[1] ? 1 : 0);
            }
            return repSum[0]*repSum[1];
        }
        public static string Answer2(IEnumerable<string> list)
        {
            List<string> list2 = new List<string>();
            for (int i = 0; i < 26; i++) {
                list2.Clear();
                foreach (string str in list) {
                    list2.Add(str.Remove(i,1));
                }
                for (int j=0;j<list2.Count;j++)
                {
                    if (list2.IndexOf(list2[j])!=-1 && list2.IndexOf(list2[j]) != j) return list2[j];
                }
            }
            return "Nothing";
        }
    }
}
