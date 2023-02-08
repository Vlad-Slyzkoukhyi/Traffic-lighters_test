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
        TimeSpan endOfDay = new(23, 00, 00);
        int endHour = endOfDay.Hours;
        int endMinute = endOfDay.Minutes;
        int endSecond = endOfDay.Seconds;
        int endDayAtSeconds = endHour * 60 * 60 + endMinute * 60 + endSecond;
        return endDayAtSeconds;
    }
    public static int StartOfDay()
    {
        TimeSpan startOfDay = new(06, 00, 00);
        int startHour = startOfDay.Hours;
        int startMinute = startOfDay.Minutes;
        int startSecond = startOfDay.Seconds;
        int startDayAtSeconds = startHour * 60 * 60 + startMinute * 60 + startSecond;
        return startDayAtSeconds;
    }
}
