using System;
using Database;
using InputParser;
using System.Collections.Generic;
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
            dynamic[] modifiedData = new dynamic[] { 20, "Mark" };
            db.modifyData(modifiedData, 7);
            IDictionary<string, dynamic> modifiedData2 = new Dictionary<string, dynamic>();
            modifiedData2.Add("Age", 12);
            modifiedData2.Add("Name", "Josh");
            db.modifyData(modifiedData2, 6);
            db.printTable(new string[] { "ID", "Name" });
            db.truncateTable();
            db.printTable();
        }
    }
}   
