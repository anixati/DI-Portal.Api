using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmDataExtractor
{


    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataTasks.Execute();
                Console.WriteLine($"Done");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"ERROR; {ex.Message}");
            }
            Console.ReadLine();
        }

        

    }


}
