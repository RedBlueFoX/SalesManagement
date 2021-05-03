﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace Database
{
    public class DataBase
    {
        public DataBase()
        {
            string[] args = new string[] { "index" };
            this.addTable(args);
            Console.WriteLine("Database Successfully Created");
        }
        
        private const int MAX_SIZE = 255;
        private int size = 0;
        private List<Table> contents = new List<Table>();
        private int tableIndex = 0;
        public void addTable(string[] args)
        {
            try
            {
                contents.Add(new Table(args));
                Console.WriteLine("Table Succesfully Created");
            }
            catch
            {
                Console.WriteLine("Table Creation Returned an Error");
            }
        }
        public Table getTable(string name)
        {
            for(int counter = 0; counter < contents.Count(); counter++)
            {
                if(this.contents[counter].getName() == name)
                {
                    return this.contents[counter];
                }
            }
            return null;
        }
        public Table getTable(int index)
        {
            return this.contents[index];
        }
        public void setFocusTable(int index)
        {
            this.tableIndex = index;
            Console.WriteLine($"Table {this.contents[counter].getName()} is Selected");
        }
        public void setFocusTable(string name)
        {
            for (int counter = 0; counter < contents.Count(); counter++)
            {
                if (this.contents[counter].getName() == name)
                {
                    this.tableIndex = counter;
                    Console.WriteLine($"Table {this.contents[counter].getName()} is Selected");
                }
            }
            
        }
        public void addEntity(dynamic[] data)
        {
            contents[tableIndex].addRow(data);
            Console.WriteLine("Row Succesfully Added");
        }

    }

    public class Table
    {
        public Table(string[] args)
        {
            this.name = args[0];
        }
        private bool isProtected = false;
        private int size = 0;
        private string name = "";
        private List<Column> rows = new List<Column>();
        public void sortTable()
        {
            for(int i = 0; i < rows.Count(); i++)
            {
                if (this.rows[i].getKey())
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
                                this.rows[y].setContents(tempData);
                            }
                        }
                    }
                    break;
                }
            }
        }
        public void createColumn(string[] args)
        {
            this.rows.Add(new Column(args));
        }
        public void addRow(dynamic[] data)
        {
            for(int i = 0; i < data.Length; i++)
            {
                this.rows[i].addData(data[i]);
            }
            Console.WriteLine("Row succesfully added");
            this.size++;
        }
        public string getName()
        {
            return this.name;
        }
    }

    public class Column
    {
        private bool autoInc = false;
        private bool key = false;
/*       private int keyReference;*/
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
            Console.WriteLine("Column Successfully Created");
        }
        public void addData(dynamic data)
        {
            this.data.Add(data);
        }
        public bool getAI()
        {
            return this.autoInc;
        }
        public string getName()
        {
            return this.name;
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
