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
    class Day4
    {
        public static void Play()
        {
            IEnumerable<string> S = openFile(@"..\..\input4.txt");
            Console.WriteLine(Answer1(S));
            Console.WriteLine(Answer2(S));        
        }
        static IEnumerable<string> openFile(string path) => File.ReadLines(path);
        public static int Answer1(IEnumerable<string> list)
        {
            List<Log> LogList = new List<Log>(GetLog(list));
            var G = GuardInfo(LogList);
            
            int MaxGuard = getMax(G);
            int[] MaxTime = G[MaxGuard];
            return Array.IndexOf(MaxTime, MaxTime.Max()) * MaxGuard;
        }
        public static int Answer2(IEnumerable<string> list)
        {
            List<Log> LogList = new List<Log>(GetLog(list));
            var G = GuardInfo(LogList);
            KeyValuePair<int,int> maxuser=new KeyValuePair<int, int>(0,0);
            foreach (var x in G) {
                if (maxuser.Value<x.Value.Max()) {
                    maxuser = new KeyValuePair<int, int>(x.Key, x.Value.Max());
                }
            }
            return maxuser.Key* Array.IndexOf(G[maxuser.Key], maxuser.Value);
        }
        public static List<Log> GetLog(IEnumerable<string> file) {
            List<Log> lst = new List<Log>();
            foreach (string s in file) {
                lst.Add(new Log(DateTime.Parse(s.Substring(1, 16)), s.Substring(19)));
            }
            return lst.OrderBy(x => x.Date).ToList();
        }
        public static Dictionary<int, int[]> GuardInfo(List<Log> lst){
            Dictionary<int, int[]> SleepTime =new Dictionary<int,int[]>(); //Records Guard ID/Guard SleepTime
            Regex R = new Regex(@"\d+");
            MatchCollection r;
            int NowGuard=0;
            int startTime=0;

            foreach(Log s in lst) {
                //Console.WriteLine(s.Msg);
                r = R.Matches(s.Msg);
                if (r.Count != 0)
                {
                    NowGuard = Int32.Parse(r[0].Value);
                    if (!SleepTime.ContainsKey(NowGuard))
                    {
                        SleepTime.Add(NowGuard, new int[60]);
                    }
                }
                else if (s.Msg == "falls asleep")
                {
                    startTime = s.Date.Minute;
                }
                else {
                    for (int i = startTime; i < s.Date.Minute; i++) {
                        SleepTime[NowGuard][i]++;
                    }
                }
            }
            return SleepTime;
        }
        public static int getMax(Dictionary<int, int[]> dic) {
            Dictionary<int, int> sumList = new Dictionary<int,int>();
            foreach (var i in dic) {
                sumList.Add(i.Key,i.Value.Sum());
            }
            return sumList.FirstOrDefault(x=>x.Value==sumList.Values.Max()).Key;
        }
        public struct Log {
            public DateTime Date; 
            public string Msg;
            public Log(DateTime date, string msg) {
                Date = date;
                Msg = msg;
            }
        }
    }
}