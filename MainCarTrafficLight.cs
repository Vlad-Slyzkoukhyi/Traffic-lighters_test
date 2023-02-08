// Main class for realisation main road car trafffic lighter
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
                await Task.Delay(redShineTimer);
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
                    await Task.Delay(blinkTimer);
                }
                else
                {
                    yellowLamp = true;
                    await Task.Delay(blinkTimer);
                }
                goto case State.BlinkYellow;
        }
    }

}