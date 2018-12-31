using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
//using System.Text.RegularExpressions;
//using System.Numerics;

namespace ConsoleApp4
{
    class Day5
    {
        public static void Play()
        {
            string S = openFile(@"..\..\input5.txt");
            //Console.WriteLine(Answer1(S));
            Console.WriteLine(Answer2(S));        
        }
        static string openFile(string path) => File.ReadAllText(path);
        public static int Answer1(string str)
        {
            bool ifSame=false;
            while (!ifSame) {
                //str = Reaction(str,ref ifSame);
                str = Replection(str,ref ifSame); //Faster version
                //Console.WriteLine(str.Length); //if you want to see the beautiful progress :)
            }
            return str.Length;
        }
        public static int Answer2(string str)
        {
            List<int> lengths = new List<int>();
            string copy = str;
            for (int i = 97; i < 123; i++)
            {
                str = str.Replace(((char)i).ToString(),"");
                str = str.Replace(((char)(i-32)).ToString(), "");
                lengths.Add(Answer1(str));
                str = copy;
            }
            return lengths.Min();
        }
        public static string Replection(string str,ref bool ifSame) {
            int inputLang = str.Length;
            string match = "";
            for (int i = 97; i < 123; i++) {
                match=(((char)i).ToString()+((char)(i-32)).ToString());
                str=str.Replace(match,"");
                match = (((char)(i -32)).ToString() + ((char)i).ToString());
                str=str.Replace(match, "");
            }
            ifSame = (str.Length == inputLang) ? true : false;
            return str;
        }
        public static string Reaction(string str,ref bool ifSame){
            char buffer = '\0';
            int index = 0;
            string str2 = "";//Copy to str2
            foreach (char c in str)
            {
                if (buffer != '\0' && buffer == Reverse(c))
                {
                    str2 = str2.Remove(str2.Length - 1);
                    buffer = '\0';
                }
                else { str2 += c; buffer = c; }
                index++;
            }
            ifSame = (str.Length==str2.Length)?true:false;
            return str2;
        }
        public static string Remover(string str, char az)
        {
            int index = 0;
            string str2 = "";//Copy to str2
            foreach (char c in str)
            {
                if (!(c==az || c==Reverse(az)))
                {
                    str2 += c;
                }
                index++;
            }
            return str2;
        }
        public static char Reverse(char c) => ((int)c < 91) ? (char)(c + 32) : (char)(c - 32);
    }
}