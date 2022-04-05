using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccessSystem
{
    public static class DatabaseContent
    {
        static SQLiteConnection dbConnection;
        static SQLiteCommand command;

        public static void ConnectToDatabase()
        {
            try
            {
                dbConnection = new SQLiteConnection("Data Source=db_access.sqlite; FailIfMissing = True");
                dbConnection.Open();
            }

            catch (Exception)
            {
                string message = "Nepodařilo se nalést databázový soubor: 'db_access.sqlite' Chcete vytvořit novou prázdnou databázi? ";
                string title = "Chyba databáze";

                MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        CreateNewDatabase();
                        break;

                    case MessageBoxResult.No:
                        Environment.Exit(0);
                        break;

                    case MessageBoxResult.Cancel:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public static SQLiteDataReader GetValuesFromDB(string sqlCommand)
        {
            try
            {
                dbConnection = new SQLiteConnection("Data Source=db_access.sqlite; FailIfMissing = True");
                dbConnection.Open();
                command = new SQLiteCommand(sqlCommand, dbConnection);
                return command.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show("Během čtení z databáze došlo k chybě.");
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        public static bool InsertIntoDatabase(string sqlCommand)
        {
            try
            {
                dbConnection = new SQLiteConnection("Data Source=db_access.sqlite; FailIfMissing = True");
                dbConnection.Open();
                command = new SQLiteCommand(sqlCommand, dbConnection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Během zápisu do databáze došlo k chybě.");
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        private static void CreateNewDatabase()
        {
            try
            {
                SQLiteConnection.CreateFile("db_access.sqlite");
                dbConnection = new SQLiteConnection("Data Source=db_access.sqlite; FailIfMissing=True");
                dbConnection.Open();
                SQLiteCommand sqlCommand = new SQLiteCommand(dbConnection);
                CreateTablesInNewDatabase(sqlCommand);
                dbConnection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("Nastala neočekávaná chyba, nelze vytvořit soubor s databází zkontrolujte zda má aplikace dostatečná opraávnění");
                MessageBox.Show(e.ToString());
            }
        }

        private static void CreateTablesInNewDatabase(SQLiteCommand sqlCommand)
        {
            sqlCommand.CommandText = "CREATE TABLE `Groups` (`id` INTEGER PRIMARY KEY,`name` TEXT,`MOfromTime` INTEGER,`MOtoTime` INTEGER,`TUfromTime` INTEGER,`TUtoTime` INTEGER,`WEfromTime` INTEGER,`WEtoTime` INTEGER,`THfromTime` INTEGER,`THtoTime` INTEGER,`FRfromTime` INTEGER,`FRtoTime` INTEGER,`SAfromTime` INTEGER,`SAtoTime` INTEGER,`SUfromTime` INTEGER,`SUtoTime` INTEGER);";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "CREATE TABLE `Users` (`id` UINT32 PRIMARY KEY,`groupID` INTEGER,`firstName` TEXT,`surName` TEXT);";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "CREATE TABLE `Logs` (`logID` INTEGER PRIMARY KEY,`id` UINT32,`timestamp` DATETIME);";
            sqlCommand.ExecuteNonQuery();
        }

        public static void CloseDatabase()
        {
            dbConnection.Close();
        }
    }
}
