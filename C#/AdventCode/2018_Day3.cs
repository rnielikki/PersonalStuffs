using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
//using System.Numerics;

namespace ConsoleApp4
{
class Day3
{
    public static void Play()
    {
        IEnumerable<string> S = openFile(@"..\..\input3.txt");
        //Console.WriteLine(Answer1(S));
        Console.WriteLine(Answer1000(S)); //Using Array. Far far faster.
        Console.WriteLine(Answer2(S));
    }
    static IEnumerable<string> openFile(string path) => File.ReadLines(path);
    public static string Answer1(IEnumerable<string> list) {
        List<Obj> ObjList = CalcObj(list);
        HashSet<string> ColList = new HashSet<string>();
        for (int i = 0; i < ObjList.Count; i++)
        {
            foreach (Obj o in ObjList)
            {
                colCoord(ObjList[i], o, ref ColList);
            }
        }
        return ColList.Count.ToString();
    }
    public static int Answer1000(IEnumerable<string> list)
    {
            List<Obj> ObjList = CalcObj(list);
            int[,] arr=new int[1000,1000];
            int counter=0;
            foreach (Obj o in ObjList) {
                for(int x = o.posX; x < o.posX + o.areaX; x++)
                {
                    for (int y = o.posY; y < o.posY + o.areaY; y++) {
                        if (arr[x, y] == 0) arr[x, y] = o.id;
                        else if (arr[x, y] != -1) { counter++; arr[x, y] = -1; }
                    }
                }
            }
            return counter;
    }
        public static string Answer2(IEnumerable<string> list) {
        List<Obj> ObjList = CalcObj(list);
        List<string> ColList = new List<string>() { };
        bool collide;
        for (int i = 0; i < ObjList.Count; i++)
        {
            collide = false;
            foreach (Obj o in ObjList)
            {
                if (Collision(ObjList[i], o).id == -1) collide = true;
            }
            if (collide == false) return ObjList[i].id.ToString();
        }

        return "Nothing";
    }
    public static List<Obj> CalcObj(IEnumerable<string> Strs) {
        List<Obj> OList = new List<Obj>();
        MatchCollection str2;
        Regex regex = new Regex(@"\d+");
        foreach (string s in Strs) {
            str2 = regex.Matches(s);
            var arr = str2.Cast<Match>().ToArray();
            // I can use foreach but meh -_ ('__ ') __ -
            OList.Add(new Obj(Int32.Parse(arr[0].Value), Int32.Parse(arr[1].Value), Int32.Parse(arr[2].Value), Int32.Parse(arr[3].Value), Int32.Parse(arr[4].Value)));
        }
        return OList;
    }
    public static Obj Collision(Obj o1, Obj o2) {
        Obj collision = new Obj(-2, 0, 0, 0, 0 );//Collisions are -1
        //Box collision algorithm.
        if ((o1.posX + o1.areaX > o2.posX && o2.posX + o2.areaX > o1.posX && o1.posY + o1.areaY > o2.posY && o2.posY + o2.areaY > o1.posY) && (o1.id!=o2.id)) {
            collision.posX = Math.Max(o1.posX, o2.posX);
            collision.posY = Math.Max(o1.posY, o2.posY);
            collision.areaX = Math.Min(o1.posX + o1.areaX, o2.posX + o2.areaX);
            collision.areaY = Math.Min(o1.posY + o1.areaY, o2.posY + o2.areaY);
            collision.areaX -= collision.posX;
            collision.areaY -= collision.posY;
            collision.id = -1;
        }
        return collision;
    }
    public static int colCoord(Obj o1, Obj o2, ref HashSet<string> ColList){
        Obj col = Collision(o1, o2);
            if (col.id != -1)  return 0; //if no collision
        for (int i = col.posX; i < col.posX + col.areaX; i++) {
            for (int j = col.posY; j < col.posY + col.areaY; j++) {
                    //if(!ColList.Contains($"{i},{j}"))
                    ColList.Add($"{i},{j}");//Hashmap removes duplications.
            }
        }
        return 1;
    }
    public struct Obj {
        public int id;
        public int posX;
        public int posY;
        public int areaX;
        public int areaY;
        public Obj(int id, int posX, int posY, int areaX, int areaY){
            this.id=id;
            this.posX = posX;
            this.posY = posY;
            this.areaX = areaX;
            this.areaY = areaY;
        }
    }
}
}
 