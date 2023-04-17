using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConnectFour
{

    internal class Program
    {
        static void Main(string[] args)
        {
            string command = "";
            while (command.Trim() != "exit")
            {
                Controller controller = new Controller();
                controller.Play();
                Console.WriteLine("Press Enter to play again or type 'exit' to quit.    ");
                command = Console.ReadLine();
            }
        }
    }
}

