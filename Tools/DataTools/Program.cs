using System;
using DataTools.Migrations;

namespace DataTools
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IMigrationTask task = new OptionSetTask();

                task.Execute();



                Console.WriteLine("Done!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
        }
    }
}
