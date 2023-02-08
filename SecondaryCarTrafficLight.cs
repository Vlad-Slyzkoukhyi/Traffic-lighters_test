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
                await Task.Delay(red1ShineTimer);
                goto case State.RedYellow;
            case State.RedYellow:
                redLamp = true;
                yellowLamp = true;
                greenLamp = false;
                await Task.Delay(redYellowShineTimer);
                goto case State.Green;
            case State.Green:
                redLamp = false;
                yellowLamp = false;
                greenLamp = true;
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
                        await Task.Delay(blinkTimer);
                    }
                    else
                    {
                        greenLamp = true;
                        await Task.Delay(blinkTimer);
                    }
                }
                goto case State.Yellow;
            case State.Yellow:
                redLamp = false;
                yellowLamp = true;
                greenLamp = false;
                await Task.Delay(redYellowShineTimer);
                goto case State.Red2;
            case State.Red2:
                redLamp = true;
                yellowLamp = false;
                greenLamp = false;
                await Task.Delay(red2ShineTimer);
                goto case State.Red;
        }
    }
}