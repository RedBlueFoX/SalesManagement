using System;
using Database;

namespace SalesManagement
{
    class Program
    {
        static void Main()
        {
            Random rnd = new Random();
            Console.WriteLine("Sales Manager Assistant");
            DataBase db = new DataBase();
            string[] tableArgs = new string[] { "firstTable", "64" };
            db.addTable(tableArgs);
            db.setFocusTable(1);
            string[] argsToAdd = new string[] { "int", "ID", "true", "true" };
            db.createColumn(argsToAdd);
            argsToAdd = new string[]{ "string", "Age", "false", "false"  };
            db.createColumn(argsToAdd);
            dynamic[] dataToAdd = new string[] { "", "20" };
            db.addEntity(dataToAdd);
            for(int i = 0; i < 10; i++)
            {
                dataToAdd = new string[] { "", Convert.ToString(rnd.Next(100))};
                db.addEntity(dataToAdd);
            }
            db.printTable();
        }
    }
}   
