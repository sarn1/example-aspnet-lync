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

        static void Main(string[] args)
        {
            // assigned SayGreeting to the delegate, this will be triggered only once.
            SayGreeting sayGreeting = delegate (string name)
            {
                Console.WriteLine(string.Format("Hello, {0}", name));
            };

            Func<string, string> conversate = delegate (string message)
             {
                 Console.WriteLine(message);
                 return Console.ReadLine();
             };


            Console.WriteLine("Whats your name:");
            string input = Console.ReadLine();
            sayGreeting(input); // Hello
            
            //use this for lambda
            //sayGreeting = (greeting) =>
            sayGreeting = delegate (string name)
            {
                Console.WriteLine(string.Format("Later, {0}", name));
            };
            Console.ReadLine();
            sayGreeting(input); //Later
            Console.ReadKey();

            // example using func
            conversate("TEST");
        }
    }
}
