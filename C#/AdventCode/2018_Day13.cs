using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using System.Numerics;

namespace ConsoleApp4
{
    class Day13
    {
        const double D90 = Math.PI / 2; //90degree
        public static void Play()
        {
            /*Vector2 v1 = new Vector2(0,0);
            Vector2 v2 = new Vector2(3,4);
            Console.WriteLine(Vector2.Distance(v1,v2));*/
            string[] Map = openFile(@"..\..\input13.txt");
            List<Car> Cars = ScanCar(Map);
            List<Car> ToRemove = new List<Car>();
            while (Cars.Count > 1)
            {
                Cars = Cars.OrderBy(c => c.y).ThenBy(c => c.x).ToList();
                foreach (Car car in Cars)
                {
                    car.Go(Map);
                    //Console.WriteLine($"{car.x}" + ", " + $"{car.y}" + ", " + $"{car.angle}"); ;
                    foreach (Car car2 in Cars)
                    {
                        if (car2 != car && car2.x == car.x && car2.y == car.y) {
                            Console.WriteLine($"{car2.x} and {car2.y} ----");
                            ToRemove.Add(car2);
                            ToRemove.Add(car);
                        }
                    }
                }
                foreach (Car del in ToRemove) {
                    if(Cars.Remove(del))
                    Console.WriteLine($"{Cars.Count} survived");
                }
                ToRemove.Clear();
            }
            Console.WriteLine($"{Cars[0].x} , {Cars[0].y}");
            //Console.WriteLine(Map[0]);

            //TODO: if("v><^".indexof(c)!=0){ new Car(x,y,c); CarList.Add(Car); }
        }
        static string[] openFile(string path) => File.ReadAllLines(path);//Remmeber, the poisition is [y][x]
        static List<Car> ScanCar(string[] map) {
            List<Car> Cars = new List<Car>();
            for (int y = 0; y < map.Length; y++) {
                for (int x = 0; x < map[y].Length; x++) {
                    if ("v<>^".IndexOf(map[y][x]) >-1) {
                        Cars.Add(new Car(x, y, map[y][x]));
                    }
                }
            }
            return Cars;
        }
        /*public map ReadMap(IEnumerable<string> Input){
        }*/
        public class Car {
            public int x; public int y; public int angle;
            int[] moveTo = { 0, 0 };//dunno is it better if less calculated math.
            int turn = -1; //left -1, straight 0, turn 1.
            public Car(int x, int y, char way) {
                this.x = x;
                this.y = y;
                angle = WayToNum(way);
                moveTo = ChangeMoveTo(angle);
            }
            private void CrossWay() {
                //Another way
                //swap x and y
                //after that if x==0 (or before that if y==0) change positive and negative
                //this way WayToNum() will be affected. Can be way faster I dunno
                angle = angle + turn;
                moveTo = ChangeMoveTo(angle);
                turn = Turn(turn);
            }
            private int[] ChangeMoveTo(int a){
                int[] returnValue = { (int)Math.Round(Math.Cos(a * D90)), (int)Math.Round(Math.Sin(a * D90)) }; //Math.Round to correct 0.99x error
                return returnValue;
            }
            private void TurnCorner(char theChar)
            {
                //either left or right
                if((moveTo[1]==0 && theChar == '/') || (moveTo[0] == 0 && theChar == '\\'))
                    angle = angle - 1;//if left
                else if ((moveTo[0] == 0 && theChar == '/') || (moveTo[1] == 0 && theChar == '\\'))
                    angle = angle + 1;//if right
                moveTo = ChangeMoveTo(angle);
            }
            public void Go(string[] Map) {
                if (Map[y][x] == '\\' || Map[y][x] == '/')
                {
                    TurnCorner(Map[y][x]);
                }
                else if (Map[y][x]=='+') {
                    CrossWay();
                }
                x = x + moveTo[0]; y = y + moveTo[1];
                //if there're crossway then CrossWay()
                //TODO: Turn on the corner. TurnCorner() if met \ or /

                //And very important thing: IF CRASH!
            }
            private int WayToNum(char way){
                switch (way) {
                    case 'v':
                        return 1;
                    case '>':
                        return 0;
                    case '<':
                        return 2;
                    case '^':
                        return 3;
                }
                return -1; //Shouldn't be. It's invalid.
            }
            private int Turn(int turn) => (turn + 2) % 3 - 1;
        }
    }
}
