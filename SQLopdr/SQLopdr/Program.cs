using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace SQLopdr
{
    class Program
    {
        static void Main(string[] args)
        {
            LogIn();
            Console.Clear();
            InitializeDB();
            showdatabase();
            choosedatabase();
            choosetable();
            //Show each database + Choose one
            //Show each table + Choose one
            //Show each collum + Choose one
            //Show Collum information
            //extra();

        }
        public const String SERVER = "localhost";
        public const String DATABASE = "";
        public const String UID = "root";
        public const String PASSWORD = "123";

        public static MySqlConnection dbConn;

        public static void InitializeDB()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = SERVER;
            builder.UserID = UID;
            builder.Password = PASSWORD;
            builder.Database = DATABASE;

            String connString = builder.ToString();

            builder = null;

            dbConn = new MySqlConnection(connString);

            //Show if connected succefully to DB
            Console.WriteLine(connString);
            try
            {
                dbConn.Open();
                Console.WriteLine(dbConn);
                dbConn.Close();
            }
            catch (Exception X)
            {
                Console.WriteLine(X);
            }
            Console.Clear();
        }

        public static void showdatabase()
        {
            MySqlCommand command = dbConn.CreateCommand();
            command.CommandText = "SHOW DATABASES;";
            MySqlDataReader Reader;
            dbConn.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    for (int u = 0; u < Reader.FieldCount; u++)
                    {
                        row += Reader.GetValue(u).ToString() + ", ";
                        Console.WriteLine("{0}", row);
                    }
                }
            }
            dbConn.Close();
        }

        public static void choosedatabase()
        { // WELKE DATABASE WIL JE IN
            Console.WriteLine("Which database do you want to access:");
            string acDB = Console.ReadLine();

            dbConn.Open();
            dbConn.ChangeDatabase(acDB);

            Console.Clear();

            //SHOW TABLES IN SELECTED DB
            MySqlCommand comd = dbConn.CreateCommand();
            comd.CommandText = "SHOW TABLES";
            MySqlDataReader TobReader;
            TobReader = comd.ExecuteReader();
            while (TobReader.Read())
            {
                string raw = "";
                for (int q = 0; q < TobReader.FieldCount; q++)
                {
                    for (int y = 0; y < TobReader.FieldCount; y++)
                    {
                        raw += TobReader.GetValue(y).ToString() + ", ";
                        Console.WriteLine("{0}", raw);
                    }

                }
            }
            dbConn.Close();
        }

        public static void choosetable()
        { //WELKE TABLE WIL JE IN
            Console.WriteLine("Which table do you want to access:");
            string selectedTable = Console.ReadLine();
            Console.Clear();
            //SELECT ALLES FROM SELECTED TABLE
            MySqlCommand cemmand = dbConn.CreateCommand();
            cemmand.CommandText = "SELECT * FROM " + selectedTable + ";";
            
            MySqlDataReader Reader;
            dbConn.Open();
            Reader = cemmand.ExecuteReader();
            DataTable results = new DataTable();
            results.Load(Reader);
            PrintTable(results);
            dbConn.Close();
            Console.ReadLine();
        }

        public static void PrintTable(DataTable table)
        {
            Console.WriteLine("--- Table(" + table.TableName + ") ---");
            int zeilen = table.Rows.Count;
            int spalten = table.Columns.Count;
            // Header

            for (int i = 0; i < table.Columns.Count; i++)
            {
                string s = table.Columns[i].ToString();
                Console.Write(String.Format("{0,-20} | ", s));
            }

            Console.Write(Environment.NewLine);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                Console.Write("---------------------|-");
            }
            Console.Write(Environment.NewLine);
            // Data

            for (int i = 0; i < zeilen; i++)
            {
                DataRow row = table.Rows[i];
                //Console.WriteLine("{0} {1} ", row[0], row[1]);
                for (int j = 0; j < spalten; j++)
                {
                    string s = row[j].ToString();
                    if (s.Length > 20) s = s.Substring(0, 17) + "...";
                    Console.Write(String.Format("{0,-20} | ", s));
                }
                Console.Write(Environment.NewLine);
            }
            for (int i = 0; i < table.Columns.Count; i++)
            {
                Console.Write("---------------------|-");
            }
            Console.Write(Environment.NewLine);
            Console.WriteLine("Look for a different database: ");

            choosedatabase();
        }

        public static void LogIn()
        {
            Console.WriteLine("Welcome user");
            Console.WriteLine("Username:");
            string username = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Welcome user");
            Console.WriteLine("Password:");
            string password = Console.ReadLine();
            Console.Clear();

            if (username != UID)
            {
                Console.WriteLine("Wrong login information please try again");
                LogIn();
                Console.Clear();
            }
            else if (password != PASSWORD)
            {
                Console.WriteLine("Wrong login information please try again");
                LogIn();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Welcome {0} please press any key to connect to the database", username);
                Console.ReadLine();
            }
        }

        public static void extra()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Show DB entries");
            Console.WriteLine("2. Change DB entries");
            Console.WriteLine("3. Add DB entries");
            Console.WriteLine("4. Delete DB entries");
            Console.WriteLine("9. Exit");


            int Usernumber = int.Parse(Console.ReadLine());

            switch (Usernumber)
            {
                case 1: //Show
                    Console.Clear();
                    //Open Connection
                    dbConn.Open();
                    Console.WriteLine(dbConn);
                    //Make a command
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM namen");
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = dbConn;
                    //Empty string for information
                    string temp = "";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    //Store the information
                    Console.WriteLine("Namen:");
                    while (reader.Read())
                    {
                        temp += reader["namen"].ToString();
                        temp += "\n";
                    }
                    //Close Connection
                    dbConn.Close();
                    //Print Information
                    Console.WriteLine(temp);
                    Console.WriteLine("Press a button to go back");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear(); //Change 
                    dbConn.Open();
                    Console.WriteLine("Which name you'd like to change:");
                    string changeName = Console.ReadLine();
                    Console.WriteLine("To what do you like {0} to change?", changeName);
                    string changetoName = Console.ReadLine();
                    MySqlCommand changeCommand = dbConn.CreateCommand();
                    changeCommand.CommandText = "Update namen SET namen = ('" + changetoName + "') WHERE namen = ('" + changeName + "')";
                    changeCommand.ExecuteNonQuery();
                    dbConn.Close();
                    Console.Clear();
                    Console.WriteLine("{0} has succesfully been changed too {1}", changeName, changetoName);
                    Console.WriteLine("Press a any key to go back");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 3: //add
                    //open connection
                    Console.Clear();
                    dbConn.Open();
                    Console.WriteLine("New name you'd like to add:");
                    //Make a command
                    MySqlCommand command = dbConn.CreateCommand();
                    //String for input
                    string newName = Console.ReadLine();
                    command.CommandText = "Insert into namen (namen) values ('" + newName + "')";
                    //execute command close connection               
                    command.ExecuteNonQuery();
                    dbConn.Close();
                    //print information
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("{0} has been added", newName);
                    Console.WriteLine("Press any key to go back");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 4: //delete
                    Console.Clear();
                    dbConn.Open();
                    Console.WriteLine("New name you'd like to delete:");
                    MySqlCommand delCommand = dbConn.CreateCommand();
                    string delNamen = Console.ReadLine();
                    delCommand.CommandText = "DELETE FROM `namen` WHERE namen= ('" + delNamen + "')";
                    delCommand.ExecuteNonQuery();
                    dbConn.Close();
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("{0} has been deleted", delNamen);
                    Console.WriteLine("Press any key to go back");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 9:
                    Console.WriteLine();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please enter a correct number");
                    Console.ReadLine();
                    Console.Clear();
                    break;
            }
        }
    }
}


