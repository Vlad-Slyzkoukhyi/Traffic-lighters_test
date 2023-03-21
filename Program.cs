using Traffic_lighters;
using static Traffic_lighters.Modes;

Console.WriteLine($"Application start {DateTime.Now}");

Modes mode = new ();

mode.CheckStatus();

await mode.Work_Mode(WorkMode.First);


