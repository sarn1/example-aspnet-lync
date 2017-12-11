using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        delegate void SayGreeting(string Name);

        public static void SayGoodbye(string name)
        {
            Console.WriteLine(string.Format("Later, {0}", name));
        }

        static void Main(string[] args)
        {
            // assigned SayGreeting to the delegate, this will be triggered only once.
            SayGreeting sayGreeting = delegate (string name)
            {
                Console.WriteLine(string.Format("Hello, {0}", name));
            };

            Console.WriteLine("Whats your name:");
            string input = Console.ReadLine();
            sayGreeting(input);
            Console.ReadLine(); // Hello

            sayGreeting = new SayGreeting(SayGoodbye);
            sayGreeting(input);
            Console.ReadKey(); // Later
        }
    }
}
