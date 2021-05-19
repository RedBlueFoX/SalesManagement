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
        public Table getTable(int index)
        {
            return this.contents[index];
        }
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
        public void modifyData(dynamic[] data, int index)
        {
            this.contents[tableIndex].modifyData(data, index);
        }
        public void modifyData(IDictionary<string, dynamic> data, int index)
        {
            this.contents[tableIndex].modifyData(data, index);
        }
        public void printTable()
        {
            this.contents[tableIndex].printTable(false);
        }
        public void printTable(string[] columns)
        {
            this.contents[tableIndex].printTable(false, columns);
        }
        public void deleteRow(int index)
        {
            this.contents[tableIndex].deleteRow(index);
        }
        
        public void truncateTable()
        {
            this.contents[tableIndex].truncateTable();
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
            for (int i = 0; i < rows.Count(); i++)
            {
                if (this.rows[i].getKey())
                {
                    for (int j = 1; j < this.rows[i].getContents()[j]; j++)
                    {
                        for (int l = j; l > 0 && this.rows[i].getContents()[l - 1] > this.rows[i].getContents()[l]; l--)
                        {

                            for (int y = 0; y < rows.Count(); y++)
                            {
                                dynamic tempData = this.rows[y].getContents();
                                dynamic temp = tempData[l];
                                tempData[l] = tempData[l - 1];
                                tempData[l - 1] = temp;
                                this.rows[y].setContents(tempData, j);
                            }
                        }
                    }
                    break;
                }
            }
        }
        public void sortTableByColumn(int columnNumber)
        {
            
        }
        public void createColumn(string[] args)
        {
            this.rows.Add(new Column(args));
        }
        public void addRow(dynamic[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                this.rows[i].addData(data[i]);
            }

            this.size++;
        }
        public void printTable(bool printAll)
        {

            if (!printAll)
            {
                char[] cell = new char[30];
                for (int i = 0; i < cell.Length; i++)
                {
                    cell[i] = ' ';
                }
                for (int i = 0; i < this.rows.Count; i++)
                {
                    cell = this.rows[i].getName().ToCharArray();
                    int letterCounter = 0;
                    while (letterCounter < 30)
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
                Console.Write("\n");
                for (int j = 0; j < this.size; j++)
                {
                    for (int k = 0; k < this.rows.Count; k++)
                    {
                        switch (this.rows[k].getDataType())
                        {
                            case "int":
                                if ((int)Math.Floor(Math.Log10(this.rows[k].getContents()[j])) + 1 <= 30)
                                {
                                    cell = Convert.ToString(this.rows[k].getContents()[j]).ToCharArray();
                                }
                                else
                                {
                                    cell = Convert.ToString(this.rows[k].getContents()[j]).Substring(0, 30).ToCharArray();
                                }
                                break;
                            case "float":
                                cell = this.rows[k].getContents()[j].ToCharArray();
                                break;
                            case "double":
                                cell = this.rows[k].getContents()[j].ToCharArray();
                                break;
                            case "char":
                                cell = this.rows[k].getContents()[j].ToCharArray();
                                break;
                            case "string":
                                if (this.rows[k].getContents()[j].Length > 30)
                                {
                                    cell = this.rows[k].getContents()[j].Substring(0, 30).ToCharArray();
                                }
                                else
                                {
                                    cell = this.rows[k].getContents()[j].ToCharArray();
                                }
                                break;
                            case "date":
                                cell = Convert.ToString(this.rows[k].getContents()[j]).ToCharArray();
                                break;
                            case "bool":
                                cell = Convert.ToString(this.rows[k].getContents()[j]).ToCharArray();
                                break;
                            default:
                                cell = Convert.ToString(this.rows[k].getContents()[j]).ToCharArray();
                                break;
                                
                        }
                        int letterCounter = 0;
                        while (letterCounter < 30)
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
                    Console.Write("\n");
                }


            }
            else
            {

            }
        }
        public void printTable(bool printAll, string[] columns)
        {
            char[] cell = new char[30];
            for (int i = 0; i < cell.Length; i++)
            {
                cell[i] = ' ';
            }
            foreach (string columnName in columns)
            {
                for (int i = 0; i < this.rows.Count; i++)
                {

                    if (columnName != this.rows[i].getName())
                    {
                        bool ifEqualToPrompt = columnName != this.rows[i].getName();
                        continue;
                    }

                    cell = this.rows[i].getName().ToCharArray();
                    int letterCounter = 0;
                    while (letterCounter < 30)
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
            Console.Write("\n");
            for (int j = 0; j < this.size; j++)
            {
                foreach (string columnName in columns)
                {
                    for (int k = 0; k < this.rows.Count; k++)
                    {

                        if (columnName != this.rows[k].getName())
                        {
                            continue;
                        }

                        switch (this.rows[k].getDataType())
                        {
                            case "int":
                                if ((int)Math.Floor(Math.Log10(this.rows[k].getContents()[j])) + 1 <= 30)
                                {
                                    cell = Convert.ToString(this.rows[k].getContents()[j]).ToCharArray();
                                }
                                else
                                {
                                    cell = Convert.ToString(this.rows[k].getContents()[j]).Substring(0, 30).ToCharArray();
                                }
                                break;
                            case "float":
                                cell = this.rows[k].getContents()[j].ToCharArray();
                                break;
                            case "double":
                                cell = this.rows[k].getContents()[j].ToCharArray();
                                break;
                            case "char":
                                cell = this.rows[k].getContents()[j].ToCharArray();
                                break;
                            case "string":
                                if (this.rows[k].getContents()[j].Length > 30)
                                {
                                    cell = this.rows[k].getContents()[j].Substring(0, 30).ToCharArray();
                                }
                                else
                                {
                                    cell = this.rows[k].getContents()[j].ToCharArray();
                                }
                                break;
                            case "date":
                                cell = Convert.ToString(this.rows[k].getContents()[j]).ToCharArray();
                                break;
                            case "bool":
                                cell = Convert.ToString(this.rows[k].getContents()[j]).ToCharArray();
                                break;
                            default:
                                cell = Convert.ToString(this.rows[k].getContents()[j]).ToCharArray();
                                break;

                        }
                        int letterCounter = 0;
                        while (letterCounter < 30)
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
                Console.Write("\n");
                
            }
        
        }
        public void deleteRow(int index)
        {
            for (int i = 0; i < this.rows.Count; i++)
            {
                this.rows[i].deleteData(index);
            }
            this.size--;
        }

        public void modifyData(dynamic[] data, int index)
        {
            for(int i = 0; i < data.Length; i++)
            {
                this.rows[i + 1].setContents(data[i], index);
            }
        }
        public void modifyData(IDictionary<string, dynamic> data, int index)
        {
            for(int i = 0; i < this.rows.Count(); i++)
            {
                foreach(KeyValuePair<string, dynamic> kvp in data)
                {
                    if(this.rows[i].getName() == kvp.Key)
                    {
                        this.rows[i].setContents(kvp.Value, index);
                    }
                } 
            }
        }

        public void truncateTable()
        {
            foreach (Column cl in this.rows)
            {
                cl.truncateColumn();
            }
            this.size = 0;
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
        public int getSize()
        {
            return this.size;
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
        public dynamic getContents(int index)
        {
            return this.data[index];
        }
        public void setContents(dynamic input, int index)
        {
            this.data[index] = input;

        }
        public void deleteData(int index)
        {
            this.data.RemoveAt(index);
            this.size--;
        }
        public void truncateColumn()
        {
            this.data.RemoveRange(0, this.data.Count);
            this.size = 0;
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
