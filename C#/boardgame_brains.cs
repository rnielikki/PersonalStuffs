using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Branins_csharp
{
    enum Obj{Nothing,Pagoda,YinYang};
    class Program
    {
        static readonly List<Field> f = new List<Field>() {
        new Field(new (int, int)[4] { (0, 6), (1, 7), (2, 5), (3, 4) }, new Obj[]{ Obj.Nothing, Obj.Nothing, Obj.Pagoda, Obj.Nothing },1),
        new Field(new (int, int)[4] { (0, 1), (2, 5), (3, 6), (4, 7) }, new Obj[] { Obj.Nothing, Obj.Nothing, Obj.Nothing, Obj.Nothing },2),
        new Field(new (int, int)[4] { (0, 2), (1, 4), (3, 5), (6, 7) }, new Obj[] { Obj.Nothing, Obj.Nothing, Obj.Nothing, Obj.Nothing },3),
        new Field(new (int, int)[4] { (0, 3), (1, 6), (2, 4), (5, 7) }, new Obj[] { Obj.Nothing, Obj.Nothing, Obj.Nothing, Obj.Nothing },4),
        new Field(new (int, int)[4] { (0, 5), (1, 2), (3, 6), (4, 7) }, new Obj[] { Obj.Nothing, Obj.Nothing, Obj.Nothing, Obj.Nothing },5),
        new Field(new (int, int)[4] { (0, 1), (2, 7), (3, 4), (5, 6) }, new Obj[] { Obj.Nothing, Obj.Nothing, Obj.Nothing, Obj.Nothing },6),
        new Field(new (int, int)[5] { (0, -1), (4, -1), (1, 7), (2 ,3), (5, 6) }, new Obj[] { Obj.YinYang, Obj.YinYang, Obj.Nothing, Obj.Nothing, Obj.Nothing },7)
        };
        static void Main(string[] args)
        {
            int[] arr1 = new int[3] { 1, 2, 3 };
            int[] arr2 = new int[3] { 1, 5, 4 };
            var x = arr1.Except(arr2);
            Console.WriteLine(String.Join(",",x));
            Console.WriteLine("---------------------");
            //Console.WriteLine(Calc.GetResult(new Journey(4,f[6]),Obj.Pagoda,f).Count);
            BlockSet B = new BlockSet(new Field[BlockSet.MAX_HEIGHT, BlockSet.MAX_WIDTH] { { null, f[0], null }, { null, f[1].Rotate(1), null } },new StartInfo(0,1,2));
            Console.WriteLine(B.Move());
        }
    }
    class BlockSet {
        public const byte MAX_WIDTH = 3;
        public const byte MAX_HEIGHT = 2;
        Field[,] Fields;
        StartInfo Start;
        public BlockSet(Field[,] FieldInfo,StartInfo Start){
            Fields = FieldInfo;
            this.Start = Start;
        }
        public Journey Move() {
            StartInfo SInfo = Start;
            Journey J = new Journey(SInfo.StartPoint,Fields[SInfo.BlockX,SInfo.BlockY]);
            Console.WriteLine($"{SInfo.BlockX},{SInfo.BlockY},{SInfo.StartPoint}");
            do
            {
                J = Fields[SInfo.BlockX, SInfo.BlockY].Output(J);
                SInfo.StartPoint = J.End.Pos;
                SInfo = Next(SInfo);
                if (!(SInfo.BlockX < MAX_HEIGHT && SInfo.BlockY < MAX_WIDTH && Fields[SInfo.BlockX, SInfo.BlockY] != null)) break;
                J.End.Pos = SInfo.StartPoint;
                J.Counter++;
                Console.WriteLine($"{SInfo.BlockX},{SInfo.BlockY},{SInfo.StartPoint}");
            } while (SInfo.BlockX<MAX_HEIGHT && SInfo.BlockY<MAX_WIDTH && Fields[SInfo.BlockX,SInfo.BlockY] != null);
            return J;
        }
        private StartInfo Next(StartInfo I) {
            Console.WriteLine(I.StartPoint);
            switch (I.StartPoint) {
                case 0:
                case 1:
                    return new StartInfo(I.BlockX - 1, I.BlockY, I.StartPoint + 5 - (I.StartPoint%2)*2);
                case 2:
                case 3:
                    return new StartInfo(I.BlockX, I.BlockY + 1, I.StartPoint + 5 - (I.StartPoint % 2) * 2);
                case 4:
                case 5:
                    return new StartInfo(I.BlockX + 1, I.BlockY, I.StartPoint - 3 - (I.StartPoint % 2) * 2);
                case 6:
                case 7:
                    return new StartInfo(I.BlockX, I.BlockY - 1, I.StartPoint - 3 - (I.StartPoint % 2) * 2);
            }
            return new StartInfo(-1,-1,-1);//Should not reach
        }
    }
    struct StartInfo {
        public int BlockX { get; }//Position of Block X
        public int BlockY { get; }//Position of Block Y
        public int StartPoint { get; set; }
        public StartInfo(int x,int y,int start){
            BlockX = x;
            BlockY = y;
            StartPoint = start;
        }
    }
   static class Calc {
        /*-------For One Field--------*/
        public static List<Field> GetResult(Journey start, Object Condition, List<Field> f)
        {
            List<Field> FList = new List<Field>();
            f[0].Rotate(2).Output(start);
            Info StartInfo = start.Start; //Note that Info is struct, not an object
            switch (Condition)
            {
                case int count: //
                    foreach (Field field in f)
                    {
                        for (byte i = 0; i < 4; i++)
                        {
                            start = new Journey(StartInfo.Pos, StartInfo.Block);
                            Journey Result = field.Rotate(i).Output(start);
                            if (Result.Counter == count)
                            {
                                FList.Add(field);
                            }
                        }
                    }
                    break;
                case Obj obj: //if found object
                    foreach (Field field in f)
                    {
                        for (byte i = 0; i < 4; i++)
                        {
                            start = new Journey(StartInfo.Pos, StartInfo.Block);
                            Journey Result = field.Rotate(i).Output(start);
                            if (Result.o.Count == 1 && Result.o[0] == obj)
                            {
                                FList.Add(field);
                            }
                        }
                        //Console.WriteLine(field.Rotate(0).Output(start).o[0]);
                    }
                    break;
                case int[] port: //int[2], start and end
                    foreach (Field field in f)
                    {
                        for (byte i = 0; i < 4; i++)
                        {
                            start = new Journey(StartInfo.Pos, StartInfo.Block);
                            Journey Result = field.Rotate(i).Output(start);
                            if (Result.End.Pos == port[1])
                            {
                                FList.Add(field);
                            }
                        }
                    }
                    break;
                case byte bridge: //bridge count
                    foreach (Field field in f)
                    {
                        for (byte i = 0; i < 4; i++)
                        {
                            start = new Journey(StartInfo.Pos, StartInfo.Block);
                            Journey Result = field.Rotate(i).Output(start);
                            if (Result.BridgeCount == bridge)
                            {
                                FList.Add(field);
                            }
                        }
                    }
                    break;
            }
            //if found return true /else return false
            return FList;

        }
    }
    class Field
    {
        public (int, int)[] Map;
        Obj[] Objs;
        bool[] Bridges;
        public short Name=0;
        public Field((int, int)[] connection, Obj[] objs,short Name)
        {
            if (connection.Length != objs.Length)
            {
                Console.WriteLine("Wrong usage");
                Environment.Exit(1);
            }
            Map = connection;
            Objs = objs;
            Bridges = MakeBridge(Map);
            this.Name = Name;
        }
        public Journey Output(Journey input)
        {
            int index = 0;
            foreach ((int, int) tp in Map)
            {
                if (tp.Item1 == input.End.Pos)
                {
                    input.End.Pos = tp.Item2;
                    if (Objs[index] != Obj.Nothing) {
                        input.o.Add(Objs[index]);
                    }
                    if (Bridges[index]) {
                        input.BridgeCount++;
                    }
                    return input;
                }
                else if (tp.Item2 == input.End.Pos)
                {
                    input.End.Pos = tp.Item1;
                    if (Objs[index] != Obj.Nothing)
                    {
                        input.o.Add(Objs[index]);
                    }
                    if (Bridges[index])
                    {
                        input.BridgeCount++;
                    }
                    return input;
                }
                index++;
            }
            return new Journey(-2,null);//Not found, should not reach
        }
        private bool[] MakeBridge((int, int)[] map)//Straight == bridge.
        {
            bool[] b = new bool[map.Length];
            int i = 0;
            foreach ((int, int) tp in map)
            {
                if ((Math.Abs(tp.Item1 - tp.Item2) == 5 && Math.Min(tp.Item1, tp.Item2) % 2 == 0) || (Math.Abs(tp.Item1 - tp.Item2) == 3 && Math.Min(tp.Item1, tp.Item2) % 2 == 1)) { b[i] = true; }
                i++;
            }
            return b;
        }
        public Field Rotate(byte rotationTimes){
            //no real obj properties/fields here, shallow copy works here
            int R = rotationTimes * 2;
            Field cloned=(Field)MemberwiseClone();
            int leng = Map.Length;
            for (int i=0;i<leng;i++) {
                cloned.Map[i].Item1 = (cloned.Map[i].Item1<0)?-1:(cloned.Map[i].Item1 + R) % 8;
                cloned.Map[i].Item2 = (cloned.Map[i].Item2 < 0) ? -1 : (cloned.Map[i].Item2 + R) % 8;
            }
            return cloned;
        }
    }
    class Journey {
        public Info Start=new Info();
        public Info End=new Info();
        //public int Pos { get; set; }
        public byte BridgeCount=0;
        public int Counter=1;
        public List<Obj> o=new List<Obj>();//Can be more than 1.
        public Journey(int position,Field field) {
            Start.Pos = position;
            End.Pos = position;
            Start.Block = field;
        }
        public override string ToString()
        {
            return $"{String.Join(", ", o)}, at {End.Pos}, bridges {BridgeCount}, Counter {Counter}";
        }
    }
    struct Info {
        public int Pos { get; set; }
        public Field Block { get; set; }
    }
}

