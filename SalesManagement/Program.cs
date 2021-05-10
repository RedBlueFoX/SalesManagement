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
            argsToAdd = new string[]{ "int", "Age", "false", "false"  };
            db.createColumn(argsToAdd);
            argsToAdd = new string[] { "string", "Name", "false", "false" };
            db.createColumn(argsToAdd);
            dynamic[] dataToAdd = new dynamic[] { "", 20, "Andrew"};
            db.addEntity(dataToAdd);
            for(int i = 0; i < 10; i++)
            {
                dataToAdd = new dynamic[] { "", rnd.Next(100), "Not Andrew"};
                db.addEntity(dataToAdd);
            }
            db.printTable();
            db.deleteRow(4);
            db.printTable();
            db.truncateTable();
        }
    }
}   
