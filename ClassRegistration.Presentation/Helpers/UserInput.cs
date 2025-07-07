using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegistration.ConsoleApplication.Helpers
{
    public class UserInput
    {
        public string PromptUserInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        public int PromptUserInputInt(string message)
        {
            Console.WriteLine(message);
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Please enter a valid number: ");
            }
            return value;
        }
    }
}
