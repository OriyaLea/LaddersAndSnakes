using System;
using LaddersAndSnakes.Models;
using LaddersAndSnakes.Global;
using System.Collections.Generic;
using System.Text;


namespace LaddersAndSnakes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Actions actions = new Actions();
            actions.InitGame();
            actions.StartGame();
            Console.ReadLine();

        }
    }
}
