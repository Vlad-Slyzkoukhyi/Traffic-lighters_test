using System;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Trafficlighters;
using static Trafficlighters.MainCarTrafficLight;

Console.WriteLine($"Application start at {DateTime.Now}");
Console.WriteLine("For stop application press <SPACEBAR>");

MainCarTrafficLight firstCarMain = new ("CarTrafficlight #1.1 Main road");
MainCarTrafficLight secondCarMain = new ("CarTrafficlight #1.2 Main road");
MainCarTrafficLight firstCarSecondary = new SecondaryCarTrafficLight("CarTrafficlight #2.1 Secondary road");
MainCarTrafficLight secondCarSecondary = new SecondaryCarTrafficLight("CarTrafficlight #2.2 Secondary road");
MainCarTrafficLight humanMain = new HumanTrafficLight("HumanTrafficlight #1.1");
MainCarTrafficLight humanSecondary = new HumanTrafficLight("HumanTrafficlight #1.2");
MainCarTrafficLight tramMain = new TramTrafficLight("TramTrafficlighter #1.1");
MainCarTrafficLight tramSecondary = new TramTrafficLight("TramTrafficlighter #1.2");

int start = TimeOfDay.StartOfDay();
int now = TimeOfDay.TimeNow();
int end = TimeOfDay.EndOfDay();

if (now > start && now < end)
{
    var task1 = firstCarMain.ModeDay(State.Green);
    var task2 = secondCarMain.ModeDay(State.Green);
    var task3 = firstCarSecondary.ModeDay(State.Red);
    var task4 = secondCarSecondary.ModeDay(State.Red);
    var task5 = humanMain.ModeDay(State.Red);
    var task6 = humanSecondary.ModeDay(State.Red);
    var task7 = tramMain.ModeDay(State.Red);
    var task8 = tramSecondary.ModeDay(State.Red);
}
else if (now < start || now > end)
{
    var task9 = firstCarMain.ModeNight(State.BlinkYellow);
    var task10 = secondCarMain.ModeNight(State.BlinkYellow);
    var task11 = firstCarSecondary.ModeNight(State.BlinkYellow);
    var task12 = secondCarSecondary.ModeNight(State.BlinkYellow);
}

while (Console.ReadKey().Key != ConsoleKey.Spacebar && now > start && now < end)
{  
    Console.ResetColor();
}

Console.WriteLine("App closed");

namespace Trafficlighters
{
    //Main class for realisation main road car trafffic lighter
    public class MainCarTrafficLight
    {
        public string Name { get; set; }

        public MainCarTrafficLight(string name)
        {
            Name = name;
        }
        //Value for timers
        public const int blinkTimer = 500;
        public const int redShineTimer = 22000;
        public const int greenShineTimer = 10000 - 3500; //-3500 ms for BlinkGreen
        public const int redYellowShineTimer = 2000;
         
        public bool redLamp = false;
        public bool yellowLamp = false;
        public bool greenLamp = false;
        public enum State
        {
            Red,
            Red2,
            RedYellow,
            Green,
            Yellow,
            BlinkGreen,
            BlinkYellow
        }
        //Day mode for main traffic lighter
        async public virtual Task ModeDay(State state)
        {
            switch (state)
            {
                case State.Red:
                    redLamp = true;
                    yellowLamp = false;
                    greenLamp = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{Name} shine {State.Red} about {(float)redShineTimer / 1000} seconds");
                    await Task.Delay(redShineTimer);
                    goto case State.RedYellow;
                case State.RedYellow:
                    redLamp = true;
                    yellowLamp = true;
                    greenLamp = false;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{Name} shine {State.RedYellow} about {(float)redYellowShineTimer / 1000} seconds");
                    await Task.Delay(redYellowShineTimer);
                    goto case State.Green;
                case State.Green:
                    redLamp = false;
                    yellowLamp = false;
                    greenLamp = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Name} shine {State.Green} about {(float)greenShineTimer / 1000} seconds");
                    await Task.Delay(greenShineTimer);
                    goto case State.BlinkGreen;
                case State.BlinkGreen:
                    redLamp = false;
                    yellowLamp = false;
                    for (int i = 0; i < 7; i++)
                    {
                        if (greenLamp == true)
                        {
                            greenLamp = false;
                            Console.ResetColor();
                            Console.WriteLine($"{Name} wasn't shine {State.Green} {(float)blinkTimer / 1000} second");
                            await Task.Delay(blinkTimer);
                        }
                        else
                        {
                            greenLamp = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{Name} shine {State.Green} {(float)blinkTimer / 1000} second");
                            await Task.Delay(blinkTimer);
                        }
                    }
                    goto case State.Yellow;
                case State.Yellow:
                    redLamp = false;
                    yellowLamp = true;
                    greenLamp = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{Name} shine {State.Yellow} about {(float)redYellowShineTimer / 1000} seconds");
                    await Task.Delay(redYellowShineTimer);
                    goto case State.Red;
            }
        }
        //Night mode for main traffic lighter
        async public virtual Task ModeNight(State state) 
        {
            redLamp = false;
            yellowLamp = false;
            greenLamp = false;
            switch (state)
            { 
                case State.BlinkYellow:
                if (yellowLamp == true)
                {
                    yellowLamp = false;
                    Console.ResetColor();
                    Console.WriteLine($"{Name} wasn't shine {State.Yellow} {(float)blinkTimer / 1000} second");
                    await Task.Delay(blinkTimer);
                }
                else
                {
                    yellowLamp = true;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{Name} shine {State.Yellow} {(float)blinkTimer / 1000} second");
                    await Task.Delay(blinkTimer);
                }
                goto case State.BlinkYellow;
            }
        }        
    }
    //Class for secondary road car traffic lighter. Inheritance from main class 
    public class SecondaryCarTrafficLight : MainCarTrafficLight
    {
        public SecondaryCarTrafficLight(string name) : base(name)
        { 
        }
  
        public const int red1ShineTimer = 10000;
        public const int red2ShineTimer = 12000;
        //Override metod create new logic for secondary car traffic lighter
        async public override Task ModeDay(State state)
        {
            switch (state)
            {
                case State.Red:
                    redLamp = true;
                    yellowLamp = false;
                    greenLamp = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{Name} shine {State.Red} about {(float)red1ShineTimer / 1000} seconds");
                    await Task.Delay(red1ShineTimer);
                    goto case State.RedYellow;
                case State.RedYellow:
                    redLamp = true;
                    yellowLamp = true;
                    greenLamp = false;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{Name} shine {State.RedYellow} about {(float)redYellowShineTimer / 1000} seconds");
                    await Task.Delay(redYellowShineTimer);
                    goto case State.Green;
                case State.Green:
                    redLamp = false;
                    yellowLamp = false;
                    greenLamp = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Name} shine {State.Green} about {(float)greenShineTimer / 1000} seconds");
                    await Task.Delay(greenShineTimer);
                    goto case State.BlinkGreen;
                case State.BlinkGreen:
                    redLamp = false;
                    yellowLamp = false;
                    for (int i = 0; i < 7; i++)
                    {
                        if (greenLamp == true)
                        {
                            greenLamp = false;
                            Console.ResetColor();
                            Console.WriteLine($"{Name} wasn't shine {State.Green} {(float)blinkTimer / 1000} second");
                            await Task.Delay(blinkTimer);
                        }
                        else
                        {
                            greenLamp = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{Name} shine {State.Green} {(float)blinkTimer / 1000} second");
                            await Task.Delay(blinkTimer);
                        }
                    }
                    goto case State.Yellow;
                case State.Yellow:
                    redLamp = false;
                    yellowLamp = true;
                    greenLamp = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{Name} shine {State.Yellow} about {(float)redYellowShineTimer / 1000} seconds");
                    await Task.Delay(redYellowShineTimer);
                    goto case State.Red2;
                case State.Red2:
                    redLamp= true;
                    yellowLamp = false;
                    greenLamp = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{Name} shine {State.Red} about {(float)red2ShineTimer / 1000} seconds");
                    await Task.Delay(red2ShineTimer);
                    goto case State.Red;
            }
        }
    }
    //Human traffic lighter which inheritance from main class
    public class HumanTrafficLight : MainCarTrafficLight
    {
        public HumanTrafficLight(string name) : base(name)
        {
        }
        public const int red1ShineTimer = 24000;
        public const int red2ShineTimer = 2000;
        //Work logic for humans and trams traffic lighters
        async public override Task ModeDay(State state)
        {
            switch (state)
            {
                case State.Red:
                    redLamp = true;
                    yellowLamp = false;
                    greenLamp = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{Name} shine {State.Red} about {(float)red1ShineTimer / 1000} seconds");
                    await Task.Delay(red1ShineTimer);
                    goto case State.Green;
                case State.Green:
                    redLamp = false;
                    yellowLamp = false;
                    greenLamp = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Name} shine {State.Green} about {(float)greenShineTimer / 1000} seconds");
                    await Task.Delay(greenShineTimer);
                    goto case State.BlinkGreen;
                case State.BlinkGreen:
                    redLamp = false;
                    yellowLamp = false;
                    for (int i = 0; i < 7; i++)
                    {
                        if (greenLamp == true)
                        {
                            greenLamp = false;
                            Console.ResetColor();
                            Console.WriteLine($"{Name} wasn't shine {State.Green} {(float)blinkTimer / 1000} second");
                            await Task.Delay(blinkTimer);
                        }
                        else
                        {
                            greenLamp = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{Name} shine {State.Green} {(float)blinkTimer / 1000} second");
                            await Task.Delay(blinkTimer);
                        }
                    }
                    goto case State.Red2;
                case State.Red2:
                    redLamp = true;
                    yellowLamp = false;
                    greenLamp = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{Name} shine {State.Red} about {(float)red2ShineTimer / 1000} seconds");
                    await Task.Delay(red2ShineTimer);
                    goto case State.Red;
            }
        }
    }
    //Class for tram traffic lighters. Inheritance from human traffic lighter.
    public class TramTrafficLight : HumanTrafficLight
    {
        public TramTrafficLight(string name) : base(name)
        {
        }         
    }

    //class for checking time
    public class TimeOfDay
    {
        public static int TimeNow()
        {
            TimeSpan timeNow = DateTime.Now.TimeOfDay;
            int hour = timeNow.Hours;
            int minute = timeNow.Minutes;
            int second = timeNow.Seconds;
            int secondsNow = hour * 60 * 60 + minute * 60 + second;
            return secondsNow;
        }
        public static int EndOfDay()
        {
            TimeSpan endOfDay = new (23, 00, 00);
            int endHour = endOfDay.Hours;
            int endMinute = endOfDay.Minutes;
            int endSecond = endOfDay.Seconds;
            int endDayAtSeconds = endHour * 60 * 60 + endMinute * 60 + endSecond;
            return endDayAtSeconds;
        }
        public static int StartOfDay()
        {
            TimeSpan startOfDay = new (06, 00, 00);
            int startHour = startOfDay.Hours;
            int startMinute = startOfDay.Minutes;
            int startSecond = startOfDay.Seconds;
            int startDayAtSeconds = startHour * 60 * 60 + startMinute * 60 + startSecond;
            return startDayAtSeconds;
        }        
    }
}
