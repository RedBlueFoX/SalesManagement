using System;
using Database;

namespace SalesManagement
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine("Sales Manager Assistant");
            DataBase db = new DataBase();
            string[] tableArgs = new string[] { "firstTable", "64" };
            db.addTable(tableArgs);
            db.setFocusTable(1);
            dynamic[] argsToAdd = new dynamic[] { "int", "true", "true" };
            db.addEntity(argsToAdd);
            
        }
    }
}   
