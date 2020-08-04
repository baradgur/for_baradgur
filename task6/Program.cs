using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task6
{
    class Program
    {
        static void Main(string[] args)
        {
            Command_handler c_handler = new Command_handler();
            Response_Handler r_handler = new Response_Handler();

            Console.WriteLine("Available commands:");
            Console.WriteLine(c_handler.ShowCommands());

            string result = "";
            while (result != "exit")
            {
                Console.Write("\nEnter command: ");
                var response = c_handler.Request(Console.ReadLine());
                result = r_handler.Process_response(response);
                Console.WriteLine(result);
            }
        }
    }
}
