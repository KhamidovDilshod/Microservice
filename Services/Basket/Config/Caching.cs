using System.Timers;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Config
{
    public class Caching
    {
        public static DistributedCacheEntryOptions FlushServer()
        {
            var currentTimeUTC = DateTime.UtcNow.ToString();
            byte[] encodedCurrentTimeUTC = System.Text.Encoding.UTF8.GetBytes(currentTimeUTC);
            return new DistributedCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromSeconds(20));
        }
        public static void Timer()
        {
            System.Timers.Timer timer = new System.Timers.Timer(3600);
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            timer.Start();
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("This will clear Redis in the future");
        }
    }
}