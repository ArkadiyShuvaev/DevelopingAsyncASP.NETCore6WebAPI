using System.Diagnostics;

namespace Books.Legacy;

public class PageCalculator
{
    /// <summary>
    /// Full CPU load for 5 seconds
    /// </summary>
    public static int CalculateBookPages(int bookId)
    {
        var watch = new Stopwatch();
        watch.Start();
        while (true)
        {
            if (watch.ElapsedMilliseconds > 5000)
            {
                break;
            }
        }

        return 42;
    }
}
