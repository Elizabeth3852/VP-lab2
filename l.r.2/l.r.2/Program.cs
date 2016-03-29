/*  Описать класс «поезд», содержащий следующие закрытые поля:
•	название пункта назначения;
•	номер поезда (может содержать буквы и цифры);
•	время отправления.
•	Предусмотреть свойства для получения состояния объекта.
•	Описать класс «вокзал», содержащий закрытый массив поездов. Обеспечить следующие возможности:
•	вывод информации о поезде по номеру с помощью индекса;
•	вывод информации о поездах, отправляющихся после введенного с клавиатуры времени;
•	перегруженную операцию сравнения, выполняющую сравнение времени отправления двух поездов;
•	вывод информации о поездах, отправляющихся в заданный пункт назначения.
    Информация должна быть отсортирована по времени отправления. 
    Написать программу, демонстрирующую все разработанные элементы классов. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l.r._2
{
    class Poezd: IComparable     // класс поезд
    {
        String nameStation; // поле пункта назначения
        int namber;         // поле номера поезда
        DateTime time;      // поле времени отправления
        public String NameStation      //свойство поля пункта назначения
        {
            get { return nameStation; }
            set { nameStation = value; }
        }
 
        public int Namber           //свойство поля номера поезда
        {
            get { return namber; }
            set 
            {
                if(value>0)
                    namber = value; 
            }
        }
 
        public DateTime Time        //свойство поля времени отправления
        {
            get { return time; }
            set { time = value; }
        }
 
        void SetTime()       //метод для ввода времени
        {
            while (true)
            {
                int hour = 0;
                int minute = 0;
                Console.Write("Введите время отправления поезда: "); // [hour.minute]
                string t = Console.ReadLine();
                string h = "";
                string m = "";
                int q;
                bool flag = false;
                for (int i = 0; i < t.Length; i++)
                {
                    if (!flag)
                    {
                        if (Int32.TryParse(Convert.ToString(t[i]), out q))
                        {
                            h = h + t[i];
                        }
                        else
                        {
                            flag = true;
                            continue;
                        }
                    }
                    if (flag)
                    {
                        if (Int32.TryParse(Convert.ToString(t[i]), out q))
                        {
                            m = m + t[i];
                        }
                    }
                }
                try
                {
                    hour = Int32.Parse(h);
                    minute = Int32.Parse(m);
                    Time = new DateTime(2016, 12, 12, hour, minute, 0);
                    return;
                }
                catch
                {
                    Console.WriteLine("Введен неверный формат времени");
                }
 
            }
        }
 
        public Poezd(string NameStation, int Namber)       //Перегруженный конструктор класса
        {
            this.NameStation = NameStation;
            this.Namber = Namber;
            SetTime ();
        }
        public Poezd(string NameStation, int Namber, DateTime Time)       //Перегруженный конструктор класса
        {
            this.NameStation = NameStation;
            this.Namber = Namber;
            this.Time = Time;
        }
 
        public int CompareTo(object input)  //реализования метода интерфейса IComparable для сортировки поездов по времени отправления
        {
            if (input is Poezd)
            {
                Poezd p1 = (Poezd)input;
                if (this.Time > p1.Time)
                    return 1;
                else if (this.Time < p1.Time)
                    return -1;
                else
                    return 0;
            }
            return 0;
        }
 
        public override string ToString()    //переопределение метода ToString()
        {
            string info = String.Format("Поезд №{0} следует в пункт назначения {1}. Время отправления: {2}.{3}!",
                Namber,NameStation,time.Hour,time.Minute);
            return info;
        }
        public void ShowInfo()      //вывод информации о поезде
        {
            Console.WriteLine(ToString());
        }
 
        public static bool operator >(Poezd p1, Poezd p2)   //Перегрузга оператора >
        {
            if (p1.Time > p2.Time)
                return true;
            return false;
        }
        public static bool operator <(Poezd p1, Poezd p2)   //Перегрузга оператора <
        {
            if (p1.Time < p2.Time)
                return true;
            return false;
        }
        public static bool operator ==(Poezd p1, Poezd p2)   //Перегрузга оператора ==
        {
            if (p1.Time == p2.Time)
                return true;
            return false;
        }
        public static bool operator !=(Poezd p1, Poezd p2)   //Перегрузга оператора !=
        {
            if (p1.Time != p2.Time)
                return true;
            return false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Vokzal vokzal = new Vokzal();
            int Kol = 4;
            for (int i = 0; i < Kol; i++)
            {
                vokzal.Add();
            }
            vokzal[0].ShowInfo();
            vokzal[1].ShowInfo();
            vokzal[2].ShowInfo();
            vokzal.ShowLastTime();
            vokzal.ShowEndPunct();
            Console.ReadKey();
        }
    }

    class Vokzal      //КЛАСС ВОКЗАЛ
    {
        List<Poezd> Poezda = new List<Poezd>();     //объявление коллекции поездов

        public Poezd this[int index]    //пользовательский индексатор для класса
        {
            get
            {
                return Poezda[index];
            }
            set
            {
                if (index >= 0 && value is Poezd)
                {
                    Poezda.Add(value);
                    Poezda.Sort();
                }
            }
        }

        public void Add()       //метод добавления поезда в коллекцию
        {
            int namber = 0;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Console.Write("Введите номер поезда: ");
                    namber = Convert.ToInt32(Console.ReadLine());
                    flag = !flag;
                }
                catch
                {
                    Console.WriteLine("Неверный формат номера");
                }
            }

            Console.Write("Введите конечный пункт назначения: ");
            string EndPunct = Console.ReadLine();
            Poezd poezd = new Poezd(EndPunct, namber);
            Poezda.Add(poezd);
            Poezda.Sort();
        }

    } 
    
}   