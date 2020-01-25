using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Exercise1
{
/*
Classic producer/consumer problem.
As I mentioned in the interview, I'm not a threading expert, but I remembered Concurrent Collections... and
this website http://www.albahari.com/threading/part5.aspx#_BlockingCollectionT

I figured TPL DataFlow was overkill. Probably could have used async somewhere, and added graceful exit code,
but decided not worth it for the exercise.
*/
    internal class Program
    {
        static void Main()
        {
            BlockingCollection<int> queue = new BlockingCollection<int>(new ConcurrentQueue<int>());
            
            for (int i = 0; i < 5; i++)
            {
                // name the clerk
                var clerkId = (char)(65+i);
                Task.Factory.StartNew (() => ClerkWork(clerkId));
            }
    
            for (int shopperId = 1;; shopperId++)
            {
                Console.WriteLine($"{shopperId} getting in line");
                queue.Add(shopperId);
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
    
            void ClerkWork(char clerkId)
            {
                var random = new Random();
                Console.WriteLine($"{clerkId} ready for work");
                foreach (var shopperId in queue.GetConsumingEnumerable())
                {
                    var wait = TimeSpan.FromMilliseconds(random.Next(1000,5000));
                    Console.WriteLine($"{clerkId} helping shopper {shopperId}");
                    Thread.Sleep(wait);
                    Console.WriteLine($"{clerkId} done helping {shopperId}");
                }
            }
        }
    }
}