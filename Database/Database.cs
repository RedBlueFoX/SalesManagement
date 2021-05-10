using System;
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
            Console.WriteLine("Database Successfully Created");
            this.addTable(args);
           
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
        /*public Table getTable(int index)
        {   
            return this.contents[index];
        }*/
        public void setFocusTable(int index)
        {
            this.tableIndex = index;
            Console.WriteLine($"Table {this.contents[index].getName()} is Selected");
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
        public void createColumn(string[] args)
        {
            this.contents[tableIndex].createColumn(args);
        }
        public void addEntity(dynamic[] data)
        {
            contents[tableIndex].addRow(data);
            Console.WriteLine("Row Succesfully Added");
        }
        public void printTable()
        {
            this.contents[tableIndex].printTable(false);
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
            
            this.size++;
        }
        public void printTable(bool printAll)
        {

            if(!printAll)
            {
                char[] cell = new char[30];
                for(int i = 0; i < cell.Length; i++)
                {
                    cell[i] = ' ';
                }
                for (int i = 0; i < this.rows.Count; i++)
                {
                    for (int j = 0; j < this.rows.Count; j++)
                    {
                        switch (this.rows[j].getDataType())
                        {
                            case "int":
                                if ((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1 <= 30)
                                {
                                    cell = Convert.ToString((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1).ToCharArray();
                                }
                                else
                                {
                                    cell = Convert.ToString((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1).Substring(0, 30).ToCharArray();
                                }
                                break;
                            case "float":
                                cell = this.rows[j].getContents()[i].ToCharArray();
                                break;
                            case "double":
                                cell = this.rows[j].getContents()[i].ToCharArray();
                                break;
                            case "char":
                                cell = this.rows[j].getContents()[i].ToCharArray();
                                break;
                            case "string":
                                if (this.rows[j].getContents()[i].Length > 30)
                                {
                                    cell = this.rows[j].getContents()[i].Substring(0, 30).ToCharArray();
                                }
                                else
                                {
                                    cell = this.rows[j].getContents()[i].ToCharArray();
                                }
                                break;
                            case "date":
                                cell = Convert.ToString(this.rows[j].getContents()[i]).ToCharArray();
                                break;
                            case "bool":
                                cell = Convert.ToString(this.rows[j].getContents()[i]).ToCharArray();
                                break;
                            default:
                                cell = Convert.ToString(this.rows[j].getContents()[i]).ToCharArray();
                                break;
                                /*case "int":
                                    if ((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1 <= 30)
                                    {
                                        Console.Write(Convert.ToString((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1));
                                    } else
                                    {
                                        Console.Write(Convert.ToString((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1).Substring(0, 30));
                                    }
                                    break;
                                case "float":
                                    Console.Write(this.rows[j].getContents()[i].ToString32());
                                    break;
                                case "double":
                                    Console.Write(this.rows[j].getContents()[i].ToString32());
                                    break;
                                case "char":
                                    Console.Write(this.rows[j].getContents()[i]);
                                    break;
                                case "string":
                                    if (this.rows[j].getContents()[i].Length > 30)
                                    {
                                        Console.Write(this.rows[j].getContents()[i].Substring(0, 30));
                                    } else
                                    {
                                        Console.Write(this.rows[j].getContents()[i]);
                                    }
                                    break;
                                case "date":
                                    Console.Write(Convert.ToString(this.rows[j].getContents()[i]));
                                    break;
                                case "bool":
                                    Console.Write(Convert.ToString(this.rows[j].getContents()[i]));
                                    break;
                                default:
                                    Console.Write(Convert.ToString(this.rows[j].getContents()[i]));
                                    break;*/
                        }
                        int letterCounter = 0;
                        while (letterCounter < 10)
                        {
                            if (letterCounter < cell.Length)
                            {
                                Console.Write(cell[letterCounter]);
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                            letterCounter++;
                        }

                        Console.Write("|");
                    }
                }
                /*for (int i = 0; i < this.rows[i].getContents().Count; i++)
                {
                    for (int j = 0; j < this.rows.Count; j++)
                    {
                        switch (this.rows[j].getDataType())
                        {
                            case "int":
                                if ((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1 <= 30)
                                {
                                    cell = Convert.ToString((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1).ToCharArray();
                                }
                                else
                                {
                                    cell = Convert.ToString((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1).Substring(0, 30).ToCharArray();
                                }
                                break;
                            case "float":
                                cell = this.rows[j].getContents()[i].ToCharArray();
                                break;
                            case "double":
                                cell = this.rows[j].getContents()[i].ToCharArray();
                                break;
                            case "char":
                                cell = this.rows[j].getContents()[i].ToCharArray();
                                break;
                            case "string":
                                if (this.rows[j].getContents()[i].Length > 30)
                                {
                                    cell = this.rows[j].getContents()[i].Substring(0, 30).ToCharArray();
                                }
                                else
                                {
                                    cell = this.rows[j].getContents()[i].ToCharArray();
                                }
                                break;
                            case "date":
                                cell = Convert.ToString(this.rows[j].getContents()[i]).ToCharArray( );
                                break;
                            case "bool":
                                cell = Convert.ToString(this.rows[j].getContents()[i]).ToCharArray( );
                                break;
                            default:
                                cell = Convert.ToString(this.rows[j].getContents()[i]).ToCharArray( );
                                break;
                                *//*case "int":
                                    if ((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1 <= 30)
                                    {
                                        Console.Write(Convert.ToString((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1));
                                    } else
                                    {
                                        Console.Write(Convert.ToString((int)Math.Floor(Math.Log10(this.rows[j].getContents()[i])) + 1).Substring(0, 30));
                                    }
                                    break;
                                case "float":
                                    Console.Write(this.rows[j].getContents()[i].ToString32());
                                    break;
                                case "double":
                                    Console.Write(this.rows[j].getContents()[i].ToString32());
                                    break;
                                case "char":
                                    Console.Write(this.rows[j].getContents()[i]);
                                    break;
                                case "string":
                                    if (this.rows[j].getContents()[i].Length > 30)
                                    {
                                        Console.Write(this.rows[j].getContents()[i].Substring(0, 30));
                                    } else
                                    {
                                        Console.Write(this.rows[j].getContents()[i]);
                                    }
                                    break;
                                case "date":
                                    Console.Write(Convert.ToString(this.rows[j].getContents()[i]));
                                    break;
                                case "bool":
                                    Console.Write(Convert.ToString(this.rows[j].getContents()[i]));
                                    break;
                                default:
                                    Console.Write(Convert.ToString(this.rows[j].getContents()[i]));
                                    break;*//*
                        }
                        int letterCounter = 0;
                        while(letterCounter < 10)
                        {
                            if(letterCounter < cell.Length)
                            {
                                Console.Write(cell[letterCounter]);
                            } else
                            {
                                Console.Write(" ");
                            }
                            letterCounter++;
                        }
                        
                        Console.Write("|");
                    }
                }*/
            }
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
        private string dataType = "";
        private string name = "";
        private int size = 0;
        private dynamic data;
        public Column(string[] args)
        {
            dataType = args[0];
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
            Console.WriteLine($"Column {name} Successfully Created");
        }
        public void addData(dynamic data)
        {
            if (autoInc == true)
            {
                try
                {
                    this.data.Add(this.data[this.data.Count - 1] + 1);
                } catch
                {
                    this.data.Add(1);
                }
            }
            else
            {
                this.data.Add(data);
            }
            size++;
        }
        public string getDataType()
        {
            return this.dataType;
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
