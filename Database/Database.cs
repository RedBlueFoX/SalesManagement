using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Database
{
    public class DataBase
    {
        public DataBase(string[] args)
        {
            
        }
        private const int MAX_SIZE = 255;
        private int size = 0;
        private List<Table> contents = new List<Table>();
    }

    public class Table
    {
        public Table(string[] args)
        {

        }
        private bool isProtected = false;
        private int size = 0;
        private List<Column> rows = new List<Column>();
        public void sortTable()
        {
            for(int i = 0; i < rows.Count(); i++)
            {
                if (rows[i].getKey())
                {
                    for (int j = 1; i < this.rows[i].getContents()[j]; j++)
                    {
                        for (int l = j; l > 0 && this.rows[i].getContents()[l - 1] > this.rows[i].getContents()[l]; l--)
                        {
                            
                            for(int y = 0; y < rows.Count(); y++)
                            {
                                dynamic tempData = this.rows[y].getContents();
                                dynamic temp = tempData[l];
                                tempData[l] = tempData[l - 1];
                                tempData[l - 1] = temp;

                            }
                        }
                    }
                    break;
                }
            }
        }
    }

    public class Column
    {
        private bool autoInc = false;
        private bool key = false;
        private string name = "";
        private dynamic data;
         public Column(string[] args)
         {
            switch (args[0])
            {
                case "int":
                    data = new List<int>();
                    break;
                case "float":
                    data = new List<float>();
                    break;
                case "double":
                    data = new List<double>();
                    break;
                case "char":
                    data = new List<char>();
                    break;
                case "string":
                    data = new List<string>();
                    break;
                case "date":
                    data = new List<int>();
                    break;
                case "bool":
                    data = new List<bool>();
                    break;
                default:
                    data = new List<int>();
                    break;
            }
            this.name = args[1];

            if (bool.TryParse(args[2], out autoInc))
            { 
                
            }
            if (bool.TryParse(args[3], out key))
            {

            }
        }
        public bool getAI()
        {
            return this.autoInc;
        }
        public bool getKey()
        {
            return this.key;
        }
        public dynamic getContents()
        {
            return this.data;
        }
        public void setContents(dynamic input)
        {
            this.data = input;

        }

        /*public void sortData()
        {
            for(int i = 1; i < this.data.Length; i++)
            {
                for(int j = i; j > 0 && this.data[j-1] > this.data[j]; j--)
                {
                    dynamic temp = this.data[j];
                    this.data[j] = this.data[j - 1];
                    this.data[j - 1] = temp;
                }
            }
        }*/
        /*public List<int> getData()
        {

        }*/
    }
    /*public class Argument
    {
        public Argument()
        {
            
        }
        public Argument(string args)
        {
           
        }
    }
    public class DBArgument : Argument
    {
        public DBArgument()
        {

        }
    }
    public class TArgument : Argument
    {
        public TArgument()
        {

        }
    }
    public class EArgument : Argument
    {
        public EArgument()
        {

        }
    }*/




}
