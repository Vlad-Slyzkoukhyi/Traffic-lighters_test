using System;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

MainCarTrafficLight firstCarMain = new("CarTrafficlight #1.1 Main road");
MainCarTrafficLight secondCarMain = new("CarTrafficlight #1.2 Main road");
SecondaryCarTrafficLight firstCarSecondary = new ("CarTrafficlight #2.1 Secondary road");
SecondaryCarTrafficLight secondCarSecondary = new ("CarTrafficlight #2.2 Secondary road");
HumanTrafficLight humanMain = new ("HumanTrafficlight #1.1");
HumanTrafficLight humanSecondary = new ("HumanTrafficlight #1.2");
TramTrafficLight tramMain = new ("TramTrafficlighter #1.1");
TramTrafficLight tramSecondary = new("TramTrafficlighter #1.2");

int start = TimeOfDay.StartOfDay();
int now = TimeOfDay.TimeNow();
int end = TimeOfDay.EndOfDay();

if (now > start && now < end)
{
    var task1 = firstCarMain.ModeDay(MainCarTrafficLight.State.Green);
    var task2 = secondCarMain.ModeDay(MainCarTrafficLight.State.Green);
    var task3 = firstCarSecondary.ModeDay(SecondaryCarTrafficLight.State.Red);
    var task4 = secondCarSecondary.ModeDay(SecondaryCarTrafficLight.State.Red);
    var task5 = humanMain.ModeDay(HumanTrafficLight.State.Red);
    var task6 = humanSecondary.ModeDay(HumanTrafficLight.State.Red);
    var task7 = tramMain.ModeDay(TramTrafficLight.State.Red);
    var task8 = tramSecondary.ModeDay(TramTrafficLight.State.Red);
    Task.WaitAll(task1, task2, task3, task4, task5, task6, task7, task8);
}
else if (now < start || now > end)
{
    var task9 = firstCarMain.ModeNight(MainCarTrafficLight.State.BlinkYellow);
    var task10 = secondCarMain.ModeNight(MainCarTrafficLight.State.BlinkYellow);
    var task11 = firstCarSecondary.ModeNight(SecondaryCarTrafficLight.State.BlinkYellow);
    var task12 = secondCarSecondary.ModeNight(SecondaryCarTrafficLight.State.BlinkYellow);
    Task.WaitAll(task9, task10, task11, task12);
} 


